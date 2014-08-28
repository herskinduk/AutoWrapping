using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public class RoslynTypeRewriter : CSharpSyntaxRewriter
    {
        private readonly IEnumerable<TypeTranslationInfo> _typeTranslation;

        public RoslynTypeRewriter(IEnumerable<TypeTranslationInfo> typeTranslation)
        {
            _typeTranslation = typeTranslation;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // Not the correct way of getting rid of compiler generated getter/setter
            if (node.AttributeLists.Any(attrList => 
                attrList.Attributes.Any(attr => 
                    attr.Name.ToFullString().Contains("System.Runtime.CompilerServices.CompilerGeneratedAttribute"))))
            {
                return null;
            }

            return base.VisitMethodDeclaration(
                node.Update(
                    node.AttributeLists,
                    SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                    UpdateReturnType(node.ReturnType),
                    node.ExplicitInterfaceSpecifier,
                    node.Identifier,
                    UpdateTypeParameterList(node.TypeParameterList),
                    UpdateParameterList(node.ParameterList),
                    node.ConstraintClauses,
                    UpdateMethodBody(node.Body, node.ReturnType, node.ParameterList),
                    node.SemicolonToken
                ));
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return base.VisitPropertyDeclaration(
                node.WithType(UpdateType(node.Type))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(UpdateAccessorList(node.AccessorList, node.Type)));
        }

        private BlockSyntax UpdateMethodBody(BlockSyntax blockSyntax, TypeSyntax returnTypeSyntax, ParameterListSyntax parameterListSyntax)
        {
            if (blockSyntax != null)
            {
                var specialType = _typeTranslation.FirstOrDefault(tt => ExtractRoslynSymbol(tt.ActualType).ToDisplayString() == returnTypeSyntax.ToFullString().Replace("global::", ""));
            
                if (specialType != null)
                {
                    blockSyntax = SyntaxFactory.Block(
                        blockSyntax.Statements.Select(s => s.IsKind(SyntaxKind.ReturnStatement) ?
                           WrapReturnStatement(s as ReturnStatementSyntax, specialType) :
                           s
                    ).ToArray());
                }


                var parameterIdentifierNodes = blockSyntax.DescendantNodes()
                    .OfType<IdentifierNameSyntax>()
                    .Where(i => parameterListSyntax.Parameters
                        .Any(p => p.Identifier.ToFullString() == i.ToFullString()));
                blockSyntax = blockSyntax.ReplaceNodes(parameterIdentifierNodes, (originalNode, rewrittenNode) => WrapForwardedIdentifier(rewrittenNode, parameterListSyntax));
            }

            return blockSyntax;
        }


        private StatementSyntax WrapReturnStatement(ReturnStatementSyntax returnStatement, TypeTranslationInfo specialType)
        {
            var wrappedExpression = SyntaxFactory.ParseExpression(specialType.ForwardTranslationExpression);
            var targetIdentifierNodes = wrappedExpression.DescendantNodes().OfType<IdentifierNameSyntax>().Where(i => i.ToFullString() == "target");
            wrappedExpression = wrappedExpression.ReplaceNodes(targetIdentifierNodes, (originalNode, rewrittenNode) => returnStatement.Expression);

            return returnStatement.Update(returnStatement.ReturnKeyword, wrappedExpression, returnStatement.SemicolonToken);
        }

        private SyntaxNode WrapForwardedIdentifier(IdentifierNameSyntax identifier, ParameterListSyntax parameterListSyntax)
        {
            var parameter = parameterListSyntax.Parameters.FirstOrDefault(p => p.Identifier.ToFullString() == identifier.ToFullString());
            var specialType = _typeTranslation.FirstOrDefault(tt => ExtractRoslynSymbol(tt.ActualType).ToDisplayString() == parameter.Type.ToFullString().Replace("global::", ""));
            
            if (specialType != null)
            {
                var wrappedExpression = SyntaxFactory.ParseExpression(specialType.ReverseTranslationExpression);
                var targetIdentifierNodes = wrappedExpression.DescendantNodes().OfType<IdentifierNameSyntax>().Where(i => i.ToFullString() == "target");
                wrappedExpression = wrappedExpression.ReplaceNodes(targetIdentifierNodes, (originalNode, rewrittenNode) => identifier);

                return wrappedExpression;
            }

            return identifier;
        }

        private ParameterListSyntax UpdateParameterList(ParameterListSyntax parameterListSyntax)
        {
            return SyntaxFactory.ParameterList(
                SyntaxFactory.SeparatedList(
                    parameterListSyntax.Parameters.Select(p => p.WithType(UpdateType(p.Type)))));                
        }

        // These (generic type params) should not be changed?
        private TypeParameterListSyntax UpdateTypeParameterList(TypeParameterListSyntax typeParameterListSyntax)
        { 
            return typeParameterListSyntax;
        }

        private TypeSyntax UpdateReturnType(TypeSyntax typeSyntax)
        {
            return UpdateType(typeSyntax);
        }

        private AccessorListSyntax UpdateAccessorList(AccessorListSyntax accessorListSyntax, TypeSyntax type)
        {
            var specialType = _typeTranslation.FirstOrDefault(tt => ExtractRoslynSymbol(tt.ActualType).ToDisplayString() == type.ToFullString().Replace("global::", ""));
            
            if (specialType != null)
            {
                return accessorListSyntax.WithAccessors(
                    SyntaxFactory.List<AccessorDeclarationSyntax>(
                        accessorListSyntax.Accessors.Select(a => 
                            a.WithBody(UpdateMethodBody(
                                a.Body, 
                                type,
                                SyntaxFactory.ParameterList(
                                    SyntaxFactory.SeparatedList<ParameterSyntax>( new [] { SyntaxFactory.Parameter(SyntaxFactory.ParseToken("value")).WithType(type) }))))))
                    );
            }

            return accessorListSyntax;
        }

        private TypeSyntax UpdateType(TypeSyntax typeSyntax)
        {
            // TODO: figure out a better way to handle global::
            var specialType = _typeTranslation.FirstOrDefault(tt => ExtractRoslynSymbol(tt.ActualType).ToDisplayString() == typeSyntax.ToFullString().Replace("global::",""));
            
            if (specialType != null)
            {
                return SyntaxFactory.ParseTypeName(specialType.TranslatedType);
            }

            return typeSyntax;
        }

        private ITypeSymbol ExtractRoslynSymbol(Type type)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Format("using {0};", type.Namespace));

            var compilation = CSharpCompilation.Create(type.Assembly.FullName)
                .AddReferences(new MetadataFileReference(type.Assembly.Location))
                .AddSyntaxTrees(tree);

            var root = (CompilationUnitSyntax)tree.GetRoot();
            var model = compilation.GetSemanticModel(tree);
            var nameInfo = model.GetSymbolInfo(root.Usings.FirstOrDefault().Name);
            var namespaceSymbol = (INamespaceSymbol)nameInfo.Symbol;

            if (type.IsGenericType)
            {
                return namespaceSymbol.GetTypeMembers(BaseTypeName(type))
                    .FirstOrDefault()
                        .Construct(type.GenericTypeArguments.Select(gta => ExtractRoslynSymbol(gta)).ToArray());
            }

            if(type.IsArray)
            {
                return CodeGenerationSymbolFactory.CreateArrayTypeSymbol(namespaceSymbol.GetTypeMembers(BaseTypeName(type)).FirstOrDefault());
            }

            return namespaceSymbol.GetTypeMembers(BaseTypeName(type)).FirstOrDefault();
        }

        static string PrettyTypeName(Type t)
        {
            if (t.IsGenericType)
            {
                return string.Format(
                    "{0}<{1}>",
                    t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture)),
                    string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName)));
            }

            return t.Name;
        }

        static string BaseTypeName(Type t)
        {
            if (t.IsGenericType)
            {
                return string.Format(
                    "{0}",
                    t.Name.Substring(0, t.Name.IndexOf("`", StringComparison.InvariantCulture)));
            }
            else if (t.IsArray)
            {
                return t.Name.Substring(0, t.Name.LastIndexOf("[", StringComparison.InvariantCulture));
            }

            return t.Name;
        }


        private TypeSyntax CreateSimpleTypeSyntax(INamedTypeSymbol symbol)
        {
            switch (symbol.SpecialType)
            {
                case SpecialType.System_Object:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword));

                case SpecialType.System_Void:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

                case SpecialType.System_Boolean:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword));

                case SpecialType.System_Char:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.CharKeyword));

                case SpecialType.System_SByte:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.SByteKeyword));

                case SpecialType.System_Byte:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ByteKeyword));

                case SpecialType.System_Int16:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ShortKeyword));

                case SpecialType.System_UInt16:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.UShortKeyword));

                case SpecialType.System_Int32:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword));

                case SpecialType.System_UInt32:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.UIntKeyword));

                case SpecialType.System_Int64:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.LongKeyword));

                case SpecialType.System_UInt64:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ULongKeyword));

                case SpecialType.System_Decimal:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.DecimalKeyword));

                case SpecialType.System_Single:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.FloatKeyword));

                case SpecialType.System_Double:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.DoubleKeyword));

                case SpecialType.System_String:
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword));
            }
            if ((symbol.Name == string.Empty) || symbol.IsAnonymousType)
            {
                return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword));
            }
            if (symbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
            {
                ITypeSymbol symbol2 = symbol.TypeArguments.First<ITypeSymbol>();
                if (symbol2.TypeKind != TypeKind.PointerType)
                {
                    // Deveation
                    //return this.AddInformationTo<NullableTypeSyntax>(SyntaxFactory.NullableType(CreateSimpleTypeSyntax(symbol2 as INamedTypeSymbol)), symbol);
                }
            }
            if (symbol.TypeParameters.Length == 0)
            {
                if ((symbol.TypeKind == TypeKind.Error) && (symbol.Name == "var"))
                {
                    return SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword));
                }
                return symbol.Name.ToIdentifierName();
            }
            //Deveation
            IEnumerable<TypeSyntax> nodes = symbol.IsUnboundGenericType ? 
                ((IEnumerable<TypeSyntax>)Enumerable.Repeat<OmittedTypeArgumentSyntax>(SyntaxFactory.OmittedTypeArgument(), symbol.TypeArguments.Length)) :
                System.Linq.ImmutableArrayExtensions.Select<ITypeSymbol, TypeSyntax>(symbol.TypeArguments, (Func<ITypeSymbol, TypeSyntax>)(t => CreateSimpleTypeSyntax(t as INamedTypeSymbol)));
            return SyntaxFactory.GenericName(symbol.ToDisplayString().ToIdentifierToken(false), SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(nodes)));
        }

        //private TTypeSyntax AddInformationTo<TTypeSyntax>(TTypeSyntax syntax, ISymbol symbol) where TTypeSyntax : TypeSyntax
        //{
        //    SyntaxTrivia[] trivia = new SyntaxTrivia[] { SyntaxFactory.ElasticMarker };
        //    SyntaxTrivia[] triviaArray2 = new SyntaxTrivia[] { SyntaxFactory.ElasticMarker };
        //    syntax = syntax.WithPrependedLeadingTrivia<TTypeSyntax>(trivia).WithAppendedTrailingTrivia<TTypeSyntax>(triviaArray2);
        //    SyntaxAnnotation[] annotations = new SyntaxAnnotation[] { SymbolAnnotation.Create(symbol) };
        //    syntax = syntax.WithAdditionalAnnotations<TTypeSyntax>(annotations);
        //    return syntax;
        //}
    }
}
