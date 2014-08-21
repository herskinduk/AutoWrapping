using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        //public override SyntaxNode VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        //{
        //    if (node.IsKind(SyntaxKind.StaticKeyword))
        //    {
        //        return null;
        //    }      
        //    return base.VisitAccessorDeclaration(node);
        //}

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.AttributeLists.Count() > 0)
            {
                return null;
            }
            else if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)))
            {
                // TODO: Investigate if this is optimal solution...
                return base.VisitMethodDeclaration(
                    node.Update(
                        node.AttributeLists,
                        SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                        node.ReturnType,
                        node.ExplicitInterfaceSpecifier,
                        node.Identifier,
                        node.TypeParameterList,
                        node.ParameterList,
                        node.ConstraintClauses,
                        node.Body,
                        node.SemicolonToken
                    ));
            }
            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)))
            {
                // TODO: Investigate if this is optimal solution...
                return base.VisitPropertyDeclaration(
                    node.Update(
                        node.AttributeLists,
                        SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                        node.Type,
                        node.ExplicitInterfaceSpecifier,
                        node.Identifier,
                        node.AccessorList,
                        node.Initializer,
                        node.SemicolonToken
                    ));
            }
            return base.VisitPropertyDeclaration(node);
        }

        public override Microsoft.CodeAnalysis.SyntaxNode VisitQualifiedName(Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax node)
        {
            var specialType = _typeTranslation.FirstOrDefault(tt =>  "global::" + ExtractRoslynSymbol(tt.ActualType).ToDisplayString() == node.ToFullString());

            if (specialType != null)
            {
                if (specialType.TranslatedType.Contains("."))
                {
                    var translatedTypeNameLeft = specialType.TranslatedType.Substring(0, specialType.TranslatedType.LastIndexOf("."));
                    var translatedTypeNameRight = specialType.TranslatedType.Substring(specialType.TranslatedType.LastIndexOf("."));

                    return base.VisitQualifiedName(SyntaxFactory.QualifiedName(SyntaxFactory.IdentifierName(translatedTypeNameLeft), SyntaxFactory.IdentifierName(translatedTypeNameRight)));
                }
                else
                {
                    return SyntaxFactory.IdentifierName(specialType.TranslatedType);
                }
            }

            return base.VisitQualifiedName(node);
        }

        private INamedTypeSymbol ExtractRoslynSymbol(Type type)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Format("using {0};", type.Namespace));

            var compilation = CSharpCompilation.Create(type.Assembly.FullName)
                .AddReferences(new MetadataFileReference(type.Assembly.Location))
                .AddSyntaxTrees(tree);

            var root = (CompilationUnitSyntax)tree.GetRoot();
            var model = compilation.GetSemanticModel(tree);
            var nameInfo = model.GetSymbolInfo(root.Usings.FirstOrDefault().Name);
            var namespaceSymbol = (INamespaceSymbol)nameInfo.Symbol;

            return namespaceSymbol.GetTypeMembers(type.Name).FirstOrDefault();
        }

        public MethodDeclarationSyntax CSharpCodeGenerator { get; set; }
    }
}
