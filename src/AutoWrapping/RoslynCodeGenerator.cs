using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoWrapping
{
    public class RoslynCodeGenerator
    {
        private readonly IEnumerable<TypeTranslationInfo> _specialTypes;

        public RoslynCodeGenerator(IEnumerable<TypeTranslationInfo> specialTypes)
        {
            _specialTypes = specialTypes;
        }

        public string GenerateInterfaceForStaticMembers(Type type)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new MemberDeclarationSyntax[] { CreateInterface(type, BindingFlags.Static) });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }

        public string GenerateInterfaceForInstanceMembers(Type type)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(new MemberDeclarationSyntax[] { CreateInterface(type, BindingFlags.Instance) });

            return Formatter.Format(compilationUnit, new CustomWorkspace()).ToFullString();
        }

        private InterfaceDeclarationSyntax CreateInterface(Type type, BindingFlags bindingFlag)
        {
            var interfaceDeclaration = SyntaxFactory.InterfaceDeclaration("I" + type.Name)
                .AddModifiers(new[] { SyntaxFactory.Token(SyntaxKind.PublicKeyword) });

            if (CreateStaticMethods(type, bindingFlag).Any())
                interfaceDeclaration = interfaceDeclaration.AddMembers(CreateStaticMethods(type, bindingFlag));

            if (CreateStaticProperties(type, bindingFlag).Any())
                interfaceDeclaration = interfaceDeclaration.AddMembers(CreateStaticProperties(type, bindingFlag));

            return interfaceDeclaration;
        }

        private MemberDeclarationSyntax[] CreateStaticMethods(Type type, BindingFlags bindingFlag)
        {
            var list = new List<MemberDeclarationSyntax>();
            foreach (var method in type.GetMethods(bindingFlag | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName))
            {
                list.Add(SyntaxFactory.MethodDeclaration(
                    attributeLists: default(SyntaxList<AttributeListSyntax>),
                    modifiers: default(SyntaxTokenList),
                    returnType: TranslateType(method.ReturnType),
                    explicitInterfaceSpecifier: default(ExplicitInterfaceSpecifierSyntax),
                    identifier: SyntaxFactory.Identifier(method.Name),
                    typeParameterList: default(TypeParameterListSyntax),
                    parameterList: CreateMethodParameters(method),
                    constraintClauses: CreateConstraintClauses(method),
                    body: default(BlockSyntax)
                ).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }
            return list.ToArray();
        }

        private MemberDeclarationSyntax[] CreateStaticProperties(Type type, BindingFlags bindingFlag)
        {
            var list = new List<MemberDeclarationSyntax>();
            foreach (var property in type.GetProperties(bindingFlag | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName))
            {
                list.Add(SyntaxFactory.PropertyDeclaration(
                    attributeLists: default(SyntaxList<AttributeListSyntax>),
                    modifiers: default(SyntaxTokenList),
                    type: TranslateType(property.PropertyType),
                    explicitInterfaceSpecifier: default(ExplicitInterfaceSpecifierSyntax),
                    identifier: SyntaxFactory.Identifier(property.Name),
                    accessorList: CreatePropertyAccessorList(property),
                    initializer: default(EqualsValueClauseSyntax)
                ).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            }
            return list.ToArray();
        }

        private TypeSyntax TranslateType(Type type)
        {
            var name = GetFullName(type);

            if (_specialTypes.Where(st => st.ActualType == type).Any())
            {
                name = _specialTypes.FirstOrDefault(st => st.ActualType == type).TranslatedType;
            }

            return SyntaxFactory.ParseTypeName(name);
        }

        private string TranslateGenericType(Type type)
        {
            var sb = new StringBuilder();
            sb.Append(type.Name.Substring(0, type.Name.LastIndexOf("`")));
            if (type.IsGenericType)
            {
                sb.Append(type.GetGenericArguments().Aggregate("<",
                    (aggregate, genType) => (aggregate == "<" ? "" : ",") + TranslateGenericType(genType)));
                sb.Append(">");
            }
            return sb.ToString();
        }

        static string GetFullName(Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            StringBuilder sb = new StringBuilder();

            sb.Append(t.Name.Substring(0, t.Name.LastIndexOf("`")));
            sb.Append(t.GetGenericArguments().Aggregate("<",

                delegate(string aggregate, Type type)
                {
                    return aggregate + (aggregate == "<" ? "" : ",") + GetFullName(type);
                }
                ));
            sb.Append(">");

            return sb.ToString();
        }


        private AccessorListSyntax CreatePropertyAccessorList(PropertyInfo property)
        {
            var list = new List<AccessorDeclarationSyntax>();

            if (property.GetGetMethod() != null)
                list.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            if (property.GetSetMethod() != null)
                list.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));

            return SyntaxFactory.AccessorList(SyntaxFactory.List<AccessorDeclarationSyntax>(list));
        }

        private SyntaxList<TypeParameterConstraintClauseSyntax> CreateConstraintClauses(MethodInfo method)
        {
            var list = new List<TypeParameterConstraintClauseSyntax>();

            list.AddRange(method.GetGenericArguments()
                    .Where(ga => CreateConstraintList(ga).Any())
                    .Select(ga => SyntaxFactory.TypeParameterConstraintClause(
                        name: SyntaxFactory.IdentifierName(ga.Name),
                        constraints: SyntaxFactory.SeparatedList<TypeParameterConstraintSyntax>(CreateConstraintList(ga))
            )));

            return list.Any() ? new SyntaxList<TypeParameterConstraintClauseSyntax>().AddRange(list) : default(SyntaxList<TypeParameterConstraintClauseSyntax>);
        }

        private List<TypeParameterConstraintSyntax> CreateConstraintList(Type ga)
        {
            var list = new List<TypeParameterConstraintSyntax>();

            if (ga.IsGenericParameter && (ga.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0) 
                list.Add(SyntaxFactory.ConstructorConstraint());                                
            if (ga.IsGenericParameter && (ga.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0) 
                list.Add(SyntaxFactory.ClassOrStructConstraint(SyntaxKind.ClassConstraint));
            if (ga.GetGenericParameterConstraints().Any())
                list.AddRange(ga.GetGenericParameterConstraints()
                    .Select(gpc => SyntaxFactory.TypeConstraint(TranslateType(gpc))));

            return list;
        }

        private ParameterListSyntax CreateMethodParameters(MethodInfo method)
        {
            var list = SyntaxFactory.SeparatedList<ParameterSyntax>()
                .AddRange(method.GetParameters().Select(p => SyntaxFactory.Parameter(
                    attributeLists: default(SyntaxList<AttributeListSyntax>),
                    modifiers: default(SyntaxTokenList),
                    type: TranslateType(p.ParameterType),
                    identifier: SyntaxFactory.Identifier(p.Name),
                    @default: default(EqualsValueClauseSyntax))));

            return SyntaxFactory.ParameterList(list);
        }
    }
}
