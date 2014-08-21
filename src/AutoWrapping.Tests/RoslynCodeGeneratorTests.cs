using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoWrapping;

namespace AutoWrapping.Tests
{
    public class RoslynCodeGeneratorTests
    {
        private readonly RoslynCodeGenerator _sut;

        public RoslynCodeGeneratorTests()
        {
            _sut = CreateRoslynCodeGenerator();
        }

        public RoslynCodeGenerator CreateRoslynCodeGenerator()
        {
            return new RoslynCodeGenerator(
                new List<TypeTranslationInfo> { 
                    new TypeTranslationInfo() { 
                        ActualType = typeof(SpecialType), 
                        TranslatedType = "TranslatedSpecialType", 
                        TranslationExpression = "new TranslatedSpecialType({0})" } });
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_ReturnsStringWithInterfaceDeclaration()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.Contains("interface ", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_InterfaceNameIsCapitalIFollowedByClassName()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.Contains("interface IString", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_InterfaceDeclaredPublic()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.StartsWith("public interface IString", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethods_AddsMethodsToSyntaxTree()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(StaticClassWithMethods));

            Assert.Contains("string MethodB(string input);", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethodAndGenericParameter_AddsMethodsToSyntaxTree()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticGenericMethod));

            Assert.Contains("T Method<T>(T input);", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethodWithGenericParameter_TransfersGenericContraint()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticGenericMethodAndGenericConstraints));

            Assert.Contains(" where T : class, global::System.ICloneable", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticProperty_MustAddPropertyToInterface()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticProperty));

            Assert.Contains("string Property { get; set; }", result);
        }

        [Fact]
        public void GenerateInterfaceForInstanceMembers_WithClassHavingStaticProperty_MustNotAddPropertyToInterface()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithStaticProperty));

            Assert.DoesNotContain("String Property { get; set; }", result);
        }

        [Fact]
        public void GenerateInterfaceForInstanceMembers_WithClassHavingInstanceProperty_MustAddPropertyToInterface()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceProperty));

            Assert.Contains("string Property { get; set; }", result);
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingPropertyOfSpecialType_MustTranslatePropertyType()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstancePropertyOfSpecialType));

            Assert.Contains("TranslatedSpecialType Property { get; set; }", result);
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodReturningSpecialType_MustTranslateReturnType()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodReturningSpecialType));

            Assert.Contains("TranslatedSpecialType Method();", result);
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfSpecialType_MustTranslateParameterType()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfSpecialType));

            Assert.Contains("void Method(TranslatedSpecialType input);", result);
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfGenericSpecialType_MustTranslateGenericType()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfGenericSpecialType));

            Assert.Contains("void Method(global::System.Collections.Generic.List<TranslatedSpecialType> input);", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithAnyClass_DeclaresPublicClass()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(String));

            Assert.Contains("public class ", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithAnyClass_ClassNameIsClassNameFollowedByWrapper()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(String));

            Assert.Contains("class StringWrapper", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticMethods_AddsMethodDeclaration()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(StaticClassWithMethods));

            Assert.Contains("string MethodB(string input)", result);
        }

        [Fact]
        public void GenerateClassForInstanceMembers_WithClassHavingInstanceMethods_AddsMethodDeclaration()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethod));

            Assert.Contains("public void Method(string input)", result);
        }

        [Fact]
        public void GenerateClassForInstanceMembers_WithClassHavingInstanceMethods_MethodBodyInvokesMethodOnInnerWrappedObject()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethod));

            Assert.Contains("return _innerWrappedObject.Method(input);", result);
        }


        [Fact]
        public void GenerateClassForInstanceMembers_WithClassHavingInstanceProperty_PropertyGetterForwardsToPropertyOnInnerWrappedObject()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceProperty));

            Assert.Matches("public string Property\\s+{\\s+get\\s+{\\s+return _innerWrappedObject.Property;", result);
        }

        [Fact]
        public void GenerateClassForInstanceMembers_WithClassHavingInstanceMethods_AddsMethodDeclarationWithTranslatedType()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfSpecialType));

            Assert.Contains("public void Method(TranslatedSpecialType input)", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticMethods_MethodDeclarationIsNotStatic()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(StaticClassWithMethods));

            Assert.DoesNotContain("static string MethodB", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticMethods_MethodBodyCallsStaticOnOriginType()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(StaticClassWithMethods));

            Assert.Contains("return global::AutoWrapping.Tests.StaticClassWithMethods.MethodB(input);", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticMethodHavingGenericConstraints_MethodCallsHasSameConstraints()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticGenericMethodAndGenericConstraints));

            Assert.Contains("return global::AutoWrapping.Tests.ClassWithStaticGenericMethodAndGenericConstraints.Method<T>(input);", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticProperty_MustAddPropertyToWrapperClass()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticProperty));

            Assert.Matches("public string Property\\s+{\\s+get\\s+{\\s+return global::AutoWrapping.Tests.ClassWithStaticProperty.Property;", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticProperty_MustAddProperty()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticProperty));

            Assert.Contains("public string Property", result);
        }

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingStaticProperty_MustNotAddGetAndSetMethods()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticProperty));

            Assert.DoesNotContain("get_Property", result);
        }
    }

    public static class StaticClassWithMethods
    {
        public static void MethodA()
        {
        }
        public static string MethodB(string input)
        {
            throw new NotImplementedException();
        }
    }

    public static class ClassWithStaticGenericMethod
    {
        public static T Method<T>(T input)
        {
            throw new NotImplementedException();
        }
    }

    public static class ClassWithStaticProperty
    {
        public static string Property { get; set; }
    }

    public class ClassWithInstanceProperty
    {
        public string Property { get; set; }
    }

    public class ClassWithInstanceMethod
    {
        public void Method(string input) { throw new NotImplementedException(); }
    }

    public class ClassWithInstancePropertyOfSpecialType
    {
        public SpecialType Property { get; set; }
    }

    public class ClassWithInstanceMethodReturningSpecialType
    {
        public SpecialType Method() { throw new NotImplementedException(); }
    }

    public class ClassWithInstanceMethodAcceptingParameterOfSpecialType
    {
        public void Method(SpecialType input) { throw new NotImplementedException(); }
    }

    public class ClassWithInstanceMethodAcceptingParameterOfGenericSpecialType
    {
        public void Method(List<SpecialType> input) { throw new NotImplementedException(); }
    }

    public static class ClassWithStaticGenericMethodAndGenericConstraints
    {
        public static T Method<T>(T input) where T : class, ICloneable
        {
            throw new NotImplementedException();
        }
    }

    public class SpecialType
    {

    }

}
