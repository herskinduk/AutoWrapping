using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using RoslynDom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoWrapping
{
    public class RoslynTypeExplorer
    {
        public string Explore(Type type)
        {
            var tree = CSharpSyntaxTree.ParseText(string.Format("using {0};", type.Namespace));

            var compilation = CSharpCompilation.Create(type.Assembly.FullName)
                .AddReferences(new MetadataFileReference(type.Assembly.Location))
                .AddSyntaxTrees(tree);

            var root = (CompilationUnitSyntax)tree.GetRoot();
            var model = compilation.GetSemanticModel(tree);
            var nameInfo = model.GetSymbolInfo(root.Usings.FirstOrDefault().Name);
            var namespaceSymbol = (INamespaceSymbol)nameInfo.Symbol;

            var typeSymbol = namespaceSymbol.GetTypeMembers(type.Name).FirstOrDefault();



            var properties = typeSymbol.GetMembers().OfType<IPropertySymbol>();
            var methods = typeSymbol.GetMembers().OfType<IMethodSymbol>().Where(m => m.IsGenericMethod);

            var workspace = MSBuildWorkspace.Create();
            workspace.OpenSolutionAsync(@"C:\Projects\AutoWrapping\src\AutoWrapping\AutoWrapping.sln");

            var syntax = Microsoft.CodeAnalysis.CodeGeneration.CodeGenerator.CreateMethodDeclaration(methods.Last(), workspace);
            return "";
        }

        public string BuildInterface()
        {
            var compilation = CSharpCompilation.Create("test")
                .AddReferences(
                    new MetadataFileReference(typeof(Enumerable).Assembly.Location),
                    new MetadataFileReference(typeof(List<int>).Assembly.Location))
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(""));


            var tSource = "TSource";
            var enumerableName = "System.Collections.Generic.IEnumerable<" + tSource + ">";
            var tFiltered = "TFiltered";


            var compUnit = SyntaxFactory.CompilationUnit(
                externs: default(SyntaxList<ExternAliasDirectiveSyntax>),
                attributeLists: default(SyntaxList<AttributeListSyntax>),
                usings: 
                    SyntaxFactory.List<UsingDirectiveSyntax>((new [] { "System", "System.Collections", "System.Linq" }).Select(ns => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(ns)))),
                members:
                    SyntaxFactory.List<MemberDeclarationSyntax>( new [] 
                    {
                        SyntaxFactory.NamespaceDeclaration(
                            externs: default(SyntaxList<ExternAliasDirectiveSyntax>),
                            usings: default(SyntaxList<UsingDirectiveSyntax>),
                            name: SyntaxFactory.ParseName("CodeGenerator"), 
                            members: SyntaxFactory.List<MemberDeclarationSyntax>( new []
                            {
                                SyntaxFactory.ClassDeclaration(
                                    baseList: default(BaseListSyntax),
                                    attributeLists: default(SyntaxList<AttributeListSyntax>),
                                    constraintClauses: default(SyntaxList<TypeParameterConstraintClauseSyntax>),
                                    typeParameterList: default(TypeParameterListSyntax),
                                    modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword)), 
                                    identifier: SyntaxFactory.Identifier("EnumerableExtensions"),
                                    members: SyntaxFactory.List<MemberDeclarationSyntax>(
                                        compilation.GetTypeByMetadataName(typeof(Enumerable).FullName).GetMembers().OfType<IMethodSymbol>()
                                            .Where(m => m.Parameters.Any(p => p.Type.Name == "Func") && m.Parameters.First().Type.ToDisplayString() == enumerableName && !m.Parameters.Any(p => p.ToDisplayString().Contains("Nullable")))
                                            .Select(oldMethod => SyntaxFactory.MethodDeclaration(
                                                attributeLists: default(SyntaxList<AttributeListSyntax>),
                                                modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword)),
                                                returnType: SyntaxFactory.ParseTypeName(oldMethod.ReturnType.ToDisplayString().Replace(oldMethod.TypeParameters.First().Name, tFiltered)),
                                                explicitInterfaceSpecifier: default(ExplicitInterfaceSpecifierSyntax),
                                                identifier: SyntaxFactory.Identifier(oldMethod.Name),
                                                typeParameterList: SyntaxFactory.TypeParameterList(parameters: SyntaxFactory.SeparatedList(oldMethod.TypeParameters.Select(tp => SyntaxFactory.TypeParameter(identifier: SyntaxFactory.Identifier(tp.Name))).Concat(new[] { SyntaxFactory.TypeParameter(identifier: SyntaxFactory.Identifier(tFiltered)) }),
                                                    Enumerable.Repeat(SyntaxFactory.Token(SyntaxKind.CommaToken), oldMethod.TypeParameters.Count()))),
                                                parameterList: SyntaxFactory.ParameterList(parameters: SyntaxFactory.SeparatedList(
                                                    new[] { SyntaxFactory.Parameter(
                                                        attributeLists: default(SyntaxList<AttributeListSyntax>),
                                                        @default: default(EqualsValueClauseSyntax),
                                                        modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ThisKeyword)), 
                                                        type: SyntaxFactory.ParseTypeName(oldMethod.Parameters.First().ToDisplayString()), 
                                                        identifier: SyntaxFactory.Identifier(oldMethod.Parameters.First().Name)) }
                                                    .Concat(oldMethod.Parameters.AsEnumerable().Skip(1).Select(p => SyntaxFactory.Parameter(
                                                        attributeLists: default(SyntaxList<AttributeListSyntax>),
                                                        @default: default(EqualsValueClauseSyntax),
                                                        modifiers: SyntaxFactory.TokenList(p.CustomModifiers.Select(m => SyntaxFactory.ParseToken(m.Modifier.Name))),
                                                        type: SyntaxFactory.ParseTypeName(p.Type.ToDisplayString() == enumerableName ? p.Type.ToDisplayString() : p.Type.ToDisplayString().Replace(tSource, tFiltered)),
                                                        identifier: SyntaxFactory.Identifier(p.Name)))
                                                    ), Enumerable.Repeat(SyntaxFactory.Token(SyntaxKind.CommaToken), oldMethod.Parameters.Count() - 1))),
                                                constraintClauses: 
                                                    SyntaxFactory.List( new []
                                                    {
                                                        SyntaxFactory.TypeParameterConstraintClause(
                                                            name: SyntaxFactory.IdentifierName(tFiltered), 
                                                            constraints: SyntaxFactory.SeparatedList<TypeParameterConstraintSyntax>( new [] {
                                                                SyntaxFactory.TypeConstraint(SyntaxFactory.ParseTypeName(oldMethod.TypeParameters.First().Name)) 
                                                            }))
                                                    }),
                                                body: SyntaxFactory.Block(
                                                    statements: SyntaxFactory.List<StatementSyntax>(new [] 
                                                    {
                                                        SyntaxFactory.ReturnStatement(
                                                            expression: SyntaxFactory.InvocationExpression(
                                                                SyntaxFactory.MemberAccessExpression(
                                                                kind: SyntaxKind.SimpleMemberAccessExpression, 
                                                                expression: SyntaxFactory.IdentifierName(typeof(Enumerable).Name), 
                                                                name: SyntaxFactory.IdentifierName(oldMethod.Name)),
                                                                argumentList: SyntaxFactory.ArgumentList(
                                                                    SyntaxFactory.SeparatedList(new ArgumentSyntax[] { SyntaxFactory.Argument(expression: SyntaxFactory.InvocationExpression(
                                                                            SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName(oldMethod.Parameters.First().Name), name: SyntaxFactory.GenericName(SyntaxFactory.Identifier("OfType"), 
                                                                                SyntaxFactory.TypeArgumentList(arguments: SyntaxFactory.SeparatedList<TypeSyntax>(new[] {SyntaxFactory.ParseTypeName(tFiltered)}, new SyntaxToken [] {})))), 
                                                                            SyntaxFactory.ArgumentList())) 
                                                                        }.Concat(oldMethod.Parameters.AsEnumerable().Skip(1).Select(p => SyntaxFactory.Argument(expression: SyntaxFactory.IdentifierName(p.Name)))),
                                                                    Enumerable.Repeat(SyntaxFactory.Token(SyntaxKind.CommaToken), Math.Max(0, oldMethod.Parameters.Count() - 1))
                                                                ))
                                                    )) } )))))) 
                                })
                            )
                        })                    
                );


            var workspace = MSBuildWorkspace.Create();
            
            //workspace.

            var project = workspace.CurrentSolution.AddProject("Project", "Project.dll", LanguageNames.CSharp);

            var document = project.AddDocument("Document","");

            SyntaxNode formattedNode = Formatter.Format( compUnit, new CustomWorkspace());
            StringBuilder sb = new StringBuilder();
            using ( StringWriter writer = new StringWriter( sb ) ) {
                formattedNode.WriteTo( writer );
            }

                //var syntaxTree = document.GetSyntaxTreeAsync().Result;

                //var options = workspace.GetOptions();
                //if (changedOptionSet != null)
                //{
                //    foreach (var entry in changedOptionSet)
                //    {
                //        options = options.WithChangedOption(entry.Key, entry.Value);
                //    }
                //}

                //var root = syntaxTree.GetRoot();
                //AssertFormat(workspace, expected, root, spans, options, document.GetTextAsync().Result);

                //// format with node and transform
                //AssertFormatWithTransformation(workspace, expected, root, spans, options, treeCompare);

                //var options = workspace.GetOptions().WithChangedOption(new Microsoft.CodeAnalysis.Options.OptionKey();
                //options.
                
                //var output = Formatter.Format(compUnit, workspace, options).ToFullString();
            
            //var output = compUnit.Format().GetFullText();
            return "";
        }


        public string ExploreRoslynDOM(Type type)
        {
            var root = RDomFactory.GetRootFromString("");

            
            
            return "";
        }
    }
}
