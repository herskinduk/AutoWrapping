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
        public RoslynCodeGenerator SutFactory()
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
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.True(result.Contains("interface "));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_InterfaceNameIsCapitalIFollowedByClassName()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.True(result.Contains("interface IString"));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_InterfaceDeclaredPublic()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.True(result.StartsWith("public interface IString"));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethods_AddsMethodsToSyntaxTree()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(StaticClassWithMethods));

            Assert.True(result.Contains("String MethodB(String input);"));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethodAndGenericParameter_AddsMethodsToSyntaxTree()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticGenericMethod));

            Assert.True(result.Contains("T Method(T input);"));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethodWithGenericParameter_TransfersGenericContraint()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticGenericMethodAndGenericConstraints));

            Assert.True(result.Contains(" where T : class, ICloneable"));
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticProperty_MustAddPropertyToInterface()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForStaticMembers(typeof(ClassWithStaticProperty));

            Assert.True(result.Contains("String Property { get; set; }"));
        }

        [Fact]
        public void GenerateInterfaceForInstanceMembers_WithClassHavingStaticProperty_MustNotAddPropertyToInterface()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithStaticProperty));

            Assert.False(result.Contains("String Property { get; set; }"));
        }

        [Fact]
        public void GenerateInterfaceForInstanceMembers_WithClassHavingInstanceProperty_MustAddPropertyToInterface()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceProperty));

            Assert.True(result.Contains("String Property { get; set; }"));
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingPropertyOfSpecialType_MustTranslatePropertyType()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstancePropertyOfSpecialType));

            Assert.True(result.Contains("TranslatedSpecialType Property { get; set; }"));
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodReturningSpecialType_MustTranslateReturnType()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodReturningSpecialType));

            Assert.True(result.Contains("TranslatedSpecialType Method();"));
        }

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfSpecialType_MustTranslateParameterType()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfSpecialType));

            Assert.True(result.Contains("Void Method(TranslatedSpecialType input);"));
        }

        //[Fact]
        //public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfSpecialType_MustTranslateParameterType()
        //{
        //    var sut = SutFactory();

        //    var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfSpecialType));

        //    Assert.True(result.Contains("Void Method(TranslatedSpecialType input);"));
        //}

        //[Fact]
        //public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfSpecialType_MustTranslateParameterType()
        //{
        //    var sut = SutFactory();

        //    var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfSpecialType));

        //    Assert.True(result.Contains("Void Method(TranslatedSpecialType input);"));
        //}

        [Fact]
        public void GenerateInterfaces_WithClassHavingMethodAcceptingParameterOfGenericSpecialType_MustTranslateGenericType()
        {
            var sut = SutFactory();

            var result = sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingParameterOfGenericSpecialType));

            Assert.True(result.Contains("Void Method(List<TranslatedSpecialType> input);"));
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
