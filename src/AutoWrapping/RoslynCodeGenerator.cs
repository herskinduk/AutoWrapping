using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoWrapping
{
    public class RoslynCodeGenerator
    {

        #region Visitors
        private class StaticMethodBlockRewriter : CSharpSyntaxRewriter
        {
            private readonly IMethodSymbol _symbol;

            public StaticMethodBlockRewriter(IMethodSymbol symbol)
            {
                _symbol = symbol;
            }

            public override SyntaxNode VisitBlock(BlockSyntax node)
            {
                if (!node.Statements.Any() && node.Parent is MethodDeclarationSyntax)
                {

                    if (((node.Parent as MethodDeclarationSyntax).ReturnType is PredefinedTypeSyntax) &&
                        ((node.Parent as MethodDeclarationSyntax).ReturnType as PredefinedTypeSyntax).Keyword.IsKind(SyntaxKind.VoidKeyword))
                    {
                        return SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                            new[]
                            {
                                SyntaxFactory.ExpressionStatement(CreateInstanceInvocationStatement("global::"+_symbol.ReceiverType.ToDisplayString()))
                            }));
                    }
                    else
                    {
                        return SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                            new[]
                            {
                                SyntaxFactory.ReturnStatement(CreateInstanceInvocationStatement("global::"+_symbol.ReceiverType.ToDisplayString()))
                            }));

                    }
                }

                return base.VisitBlock(node);
            }

            private ExpressionSyntax CreateInstanceInvocationStatement(string identifierName)
            {
                return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        kind: SyntaxKind.SimpleMemberAccessExpression,
                        expression: SyntaxFactory.IdentifierName(identifierName),
                        name: _symbol.IsGenericMethod ?
                            SyntaxFactory.GenericName(
                                SyntaxFactory.Identifier(_symbol.Name),
                                SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(
                                    _symbol.TypeArguments.Select(ta => SyntaxFactory.ParseTypeName(ta.Name))))) as SimpleNameSyntax :
                            SyntaxFactory.IdentifierName(_symbol.Name) as SimpleNameSyntax),
                    CreateArgumentList());
            }

            private ArgumentListSyntax CreateArgumentList()
            {
                return SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList<ArgumentSyntax>(
                        _symbol.Parameters.Select(p => p.RefKind == RefKind.Out
                            ? SyntaxFactory.Argument(SyntaxFactory.IdentifierName(p.Name)).WithRefOrOutKeyword(SyntaxFactory.Token(SyntaxKind.OutKeyword)) :
                            SyntaxFactory.Argument(SyntaxFactory.IdentifierName(p.Name)))));
            }

            
        }

        private class StaticPropertyBlockRewriter : CSharpSyntaxRewriter
        {
            private readonly IPropertySymbol _symbol;

            public StaticPropertyBlockRewriter(IPropertySymbol symbol)
            {
                _symbol = symbol;
            }

            public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
            {
                return base.VisitPropertyDeclaration(
                    node.WithAccessorList(
                            SyntaxFactory.AccessorList(
                            SyntaxFactory.List<AccessorDeclarationSyntax>(
                                new[]
                                {
                                    SyntaxFactory
                                        .AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                        .WithBody(SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                                            new[]
                                            {
                                                SyntaxFactory.ReturnStatement(
                                                    SyntaxFactory.MemberAccessExpression(
                                                        kind: SyntaxKind.SimpleMemberAccessExpression,
                                                        expression: SyntaxFactory.IdentifierName("global::"+_symbol.ContainingType.ToDisplayString()),
                                                        name: SyntaxFactory.IdentifierName(_symbol.Name))
                                                )
                                            }
                                        )
                                    )),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                        .WithBody(SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                                            new[]
                                            {
                                                SyntaxFactory.ParseStatement("global::"+_symbol.ContainingType.ToDisplayString()+"."+_symbol.Name+" = value;")
                                            }
                                        )
                                    ))
                                }.Where(ad => 
                                    (_symbol.GetMethod != null && ad.IsKind(SyntaxKind.GetAccessorDeclaration)) ||
                                    (_symbol.SetMethod != null && ad.IsKind(SyntaxKind.SetAccessorDeclaration)))
                            )
                        )));
            }

        }

        private class InstanceMethodBlockRewriter : CSharpSyntaxRewriter
        {
            private readonly IMethodSymbol _symbol;

            public InstanceMethodBlockRewriter(IMethodSymbol symbol)
            {
                _symbol = symbol;
            }

            public override SyntaxNode VisitBlock(BlockSyntax node)
            {
                if (!node.Statements.Any() && node.Parent is MethodDeclarationSyntax)
                {

                    if (((node.Parent as MethodDeclarationSyntax).ReturnType is PredefinedTypeSyntax) &&
                        ((node.Parent as MethodDeclarationSyntax).ReturnType as PredefinedTypeSyntax).Keyword.IsKind(SyntaxKind.VoidKeyword))
                    {
                        return SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                            new[]
                            {
                                SyntaxFactory.ExpressionStatement(CreateInstanceInvocationStatement())
                            }
                        ));
                    }
                    else
                    {
                        return SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                            new[]
                            {
                                SyntaxFactory.ReturnStatement(CreateInstanceInvocationStatement())
                            }
                        ));
                    }
                }

                return base.VisitBlock(node);
            }

            private ExpressionSyntax CreateInstanceInvocationStatement()
            {
                return SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        kind: SyntaxKind.SimpleMemberAccessExpression,
                        expression: SyntaxFactory.IdentifierName("_innerWrappedObject"),
                        name: _symbol.IsGenericMethod ? 
                            SyntaxFactory.GenericName(
                                SyntaxFactory.Identifier(_symbol.Name), 
                                SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(
                                    _symbol.TypeArguments.Select(ta => SyntaxFactory.ParseTypeName(ta.Name))))) as SimpleNameSyntax :
                            SyntaxFactory.IdentifierName(_symbol.Name) as SimpleNameSyntax),
                    CreateArgumentList());
            }

            private ArgumentListSyntax CreateArgumentList()
            {
                return SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList<ArgumentSyntax>(
                        _symbol.Parameters.Select(p => p.RefKind == RefKind.Out
                            ? SyntaxFactory.Argument(SyntaxFactory.IdentifierName(p.Name)).WithRefOrOutKeyword(SyntaxFactory.Token(SyntaxKind.OutKeyword)) :
                            SyntaxFactory.Argument(SyntaxFactory.IdentifierName(p.Name)))));
            }
        }


        private class InstancePropertyBlockRewriter : CSharpSyntaxRewriter
        {
            private readonly IPropertySymbol _symbol;

            public InstancePropertyBlockRewriter(IPropertySymbol symbol)
            {
                _symbol = symbol;
            }

            public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
            {
                return base.VisitPropertyDeclaration(
                    node.WithAccessorList(
                            SyntaxFactory.AccessorList(
                            SyntaxFactory.List<AccessorDeclarationSyntax>(
                                new[]
                                {
                                    SyntaxFactory
                                        .AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                        .WithBody(SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                                            new[]
                                            {
                                                SyntaxFactory.ParseStatement("return _innerWrappedObject."+_symbol.Name + ";")
                                            }
                                        )
                                    )),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                        .WithBody(SyntaxFactory.Block(SyntaxFactory.List<StatementSyntax>(
                                            new[]
                                            {
                                                SyntaxFactory.ParseStatement("_innerWrappedObject."+_symbol.Name+" = value;")
                                            }
                                        )
                                    ))
                                }.Where(ad =>
                                    (_symbol.GetMethod != null && ad.IsKind(SyntaxKind.GetAccessorDeclaration)) ||
                                    (_symbol.SetMethod != null && ad.IsKind(SyntaxKind.SetAccessorDeclaration)))
                            )
                        )));
            }

        }
        #endregion

        private readonly IEnumerable<TypeTranslationInfo> _specialTypes;
        private Workspace _workspace = MSBuildWorkspace.Create();
        private readonly RoslynTypeRewriter _typeUpdater;

        // TODO: Inject naming strategies for namespaces, interfaces, innerobject etc.
        // TODO: Inject strategies for stripping/keeping attributes on members
        public RoslynCodeGenerator(IEnumerable<TypeTranslationInfo> specialTypes)
        {
            _specialTypes = specialTypes;
            _typeUpdater = new RoslynTypeRewriter(_specialTypes); // TODO: Refactor to DI
        }



        #region Class Generation

        public string GenerateClassForStaticMembers(Type type)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(type.Namespace))
                .AddMembers(new MemberDeclarationSyntax[] { CreateClass(type, true) });

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new[] { ns });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }

        public string GenerateClassForInstanceMembers(Type type)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(type.Namespace))
                .AddMembers(new MemberDeclarationSyntax[] { CreateClass(type, false) });

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new[] { ns });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }


        private ClassDeclarationSyntax CreateClass(Type type, bool isStatic)
        {
            var typeSymbol = ExtractRoslynSymbol(type) as ITypeSymbol;

            var instanceClassSyntax = SyntaxFactory.ParseCompilationUnit(@"
public class TargetClassIdentifier : TargetInterfaceIdentifier
{
    private TargetType _innerWrappedObject;

    public object InnerWrappedObject
    {
        get { return _innerWrappedObject; }
        set { _innerWrappedObject = value as TargetType; }
    }

    public TargetClassIdentifier(object innerWrappedObject)
    {
        _innerWrappedObject = innerWrappedObject as TargetType;
    }
}
");
            
            var staticClassSyntax = SyntaxFactory.ParseCompilationUnit(@"
public class TargetClassIdentifier : TargetInterfaceIdentifier
{
    public TargetClassIdentifier()
    {
    }
}
");


            var classDeclaration = (isStatic ? staticClassSyntax : instanceClassSyntax).ChildNodes().OfType<ClassDeclarationSyntax>().First();
            classDeclaration = classDeclaration
                .ReplaceNodes<ClassDeclarationSyntax, IdentifierNameSyntax>(
                    classDeclaration.DescendantNodesAndSelf().OfType<IdentifierNameSyntax>().Where(ins => ins.Identifier.Text == "TargetType"),
                    (original, rewritten) => SyntaxFactory.ParseName(type.FullName));
            classDeclaration = classDeclaration
                .ReplaceTokens<ClassDeclarationSyntax>(
                    classDeclaration
                        .DescendantNodesAndTokensAndSelf()
                        .Where(nodeOrToken => nodeOrToken.IsToken)
                        .Select(nodeOrToken => nodeOrToken.AsToken())
                        .Where(st => st.Text == "TargetClassIdentifier"),
                    (original, rewritten) => SyntaxFactory.ParseToken(type.Name + "Wrapper"));

            classDeclaration = classDeclaration
                .ReplaceTokens<ClassDeclarationSyntax>(
                    classDeclaration
                        .DescendantNodesAndTokensAndSelf()
                        .Where(nodeOrToken => nodeOrToken.IsToken)
                        .Select(nodeOrToken => nodeOrToken.AsToken())
                        .Where(st => st.Text == "TargetInterfaceIdentifier"),
                        (original, rewritten) => SyntaxFactory.ParseToken("I" + type.Name));

            var methods = CreateMethodDeclarations(typeSymbol, isStatic, CodeGenerationDestination.ClassType);
            if (methods.Any())
                classDeclaration = classDeclaration.AddMembers(methods);

            var properties = CreatePropertyDeclarations(typeSymbol, isStatic, CodeGenerationDestination.ClassType);
            if (properties.Any())
                classDeclaration = classDeclaration.AddMembers(properties);

            return classDeclaration;
        }

        #endregion


        #region Interface generation

        public string GenerateInterfaceForStaticMembers(Type type)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(type.Namespace))
                .AddMembers(new MemberDeclarationSyntax[] { CreateInterface(type, true) });

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new[] { ns });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }

        public string GenerateInterfaceForInstanceMembers(Type type)
        {
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(type.Namespace))
                .AddMembers(new MemberDeclarationSyntax[] { CreateInterface(type, false) });

            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new[] { ns });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }

        private InterfaceDeclarationSyntax CreateInterface(Type type, bool isStatic)
        {
            var baselist = isStatic ?
                new[] { SyntaxFactory.ParseTypeName("global::AutoWrapping.IAutoWrapped") } :
                new[] { SyntaxFactory.ParseTypeName("global::AutoWrapping.IAutoWrappedInstance") };

            var typeSymbol = ExtractRoslynSymbol(type);

            var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration("I" + type.Name)
                .AddModifiers(new[] { SyntaxFactory.Token(SyntaxKind.PublicKeyword) })
                .AddBaseListTypes(baselist);

            if (CreateMethodDeclarations(typeSymbol, isStatic, CodeGenerationDestination.InterfaceType).Any())
                interfaceDeclaration = interfaceDeclaration.AddMembers(CreateMethodDeclarations(typeSymbol, isStatic, CodeGenerationDestination.InterfaceType));

            if (CreatePropertyDeclarations(typeSymbol, isStatic, CodeGenerationDestination.InterfaceType).Any())
                interfaceDeclaration = interfaceDeclaration.AddMembers(CreatePropertyDeclarations(typeSymbol, isStatic, CodeGenerationDestination.InterfaceType));

            return interfaceDeclaration;
        }

        #endregion

        private MemberDeclarationSyntax[] CreateMethodDeclarations(ITypeSymbol typeSymbol, bool isStatic, CodeGenerationDestination destination)
        {
            var methods = typeSymbol.GetMembers().Where(member => 
                member.Kind == SymbolKind.Method && 
                member.DeclaredAccessibility == Accessibility.Public &&
                member.IsStatic == isStatic &&
                !member.GetAttributes().Any(attr => attr.AttributeClass.Name == "ObsoleteAttribute"));
            var list = new List<MemberDeclarationSyntax>();

            foreach (var method in methods.Select(m => m as IMethodSymbol))
            {
                if (method.Name == ".ctor") continue;

                var methodRewriter = isStatic ? 
                    new StaticMethodBlockRewriter(method) as CSharpSyntaxRewriter : 
                    new InstanceMethodBlockRewriter(method) as CSharpSyntaxRewriter;

                var syntax = CodeGenerator.CreateMethodDeclaration(method, _workspace, destination);
                
                if (destination == CodeGenerationDestination.ClassType)
                {
                    syntax = methodRewriter.Visit(syntax);
                }

                syntax = _typeUpdater.Visit(syntax as SyntaxNode);

                if (destination == CodeGenerationDestination.ClassType && syntax != null)
                {
                    syntax = (syntax as MethodDeclarationSyntax).WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
                }

                if (syntax != null)
                    list.Add(syntax as MemberDeclarationSyntax);
            }
            
            return list.ToArray();
        }

        private MemberDeclarationSyntax[] CreatePropertyDeclarations(ITypeSymbol typeSymbol, bool isStatic, CodeGenerationDestination destination)
        {
            var properties = typeSymbol.GetMembers().Where(member => 
                member.Kind == SymbolKind.Property &&
                member.DeclaredAccessibility == Accessibility.Public &&
                member.IsStatic == isStatic &&
                !member.GetAttributes().Any(attr => attr.AttributeClass.Name == "ObsoleteAttribute"));
            var list = new List<MemberDeclarationSyntax>();

            foreach (var property in properties.Select(m => m as IPropertySymbol))
            {
                var propertyRewriter = isStatic ? 
                    new StaticPropertyBlockRewriter(property) as CSharpSyntaxRewriter : 
                    new InstancePropertyBlockRewriter(property) as CSharpSyntaxRewriter;
                
                var syntax = CodeGenerator.CreatePropertyDeclaration(property, _workspace, destination, CodeGenerationOptions.Default);

                if (destination == CodeGenerationDestination.ClassType)
                {
                    syntax = propertyRewriter.Visit(syntax);
                }
                syntax = _typeUpdater.Visit(syntax);

                if (destination == CodeGenerationDestination.ClassType && syntax != null)
                {
                    syntax = (syntax as PropertyDeclarationSyntax).WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
                }
                list.Add(syntax as MemberDeclarationSyntax);
            }

            return list.ToArray();
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

    }
}
