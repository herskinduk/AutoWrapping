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

        // The two translations represent the most common translations
        public RoslynCodeGenerator CreateRoslynCodeGenerator()
        {
            return new RoslynCodeGenerator(
                new List<TypeTranslationInfo>() { 
                    new TypeTranslationInfo() { 
                        ActualType = typeof(SpecialType), 
                        TranslatedType = "TranslatedSpecialType", 
                        ForwardTranslationExpression = "new TranslatedSpecialType(target)",
                        ReverseTranslationExpression = "target.InnerWrappedObject"
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(List<SpecialType>), 
                        TranslatedType = "global::System.Collections.Generic.List<TranslatedSpecialType>", 
                        ForwardTranslationExpression = "target.Select(t => new TranslatedSpecialType(t)).ToList()",
                        ReverseTranslationExpression = "target.Select(t => t.InnerWrapperObject).ToList()"
                    }
                }
            );
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
        public void GenerateInterfaceForStaticMembers_WithAnyClass_MustNotAddCtor()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.DoesNotMatch("\\spublic String()\\s{", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithAnyClass_InterfaceDeclaredPublic()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.Contains("public interface IString", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethods_AddsMethodsToSyntaxTree()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(StaticClassWithMethods));

            Assert.Contains("string MethodB(string input);", result);
        }

        [Fact]
        public void GenerateInterfaceForStaticMembers_WithClassHavingStaticMethods_DoesNotUsePublicKeywordOnMethods()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(StaticClassWithMethods));

            Assert.DoesNotContain("public string MethodB(string input);", result);
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
        public void GenerateClass_WithClassHavingInstanceMethodAcceptingOutParameter_MustAddOutKeywordForwarededParameters()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethodAcceptingOutParameter));

            Assert.Contains("_innerWrappedObject.Method(out input)", result);
        }

        [Fact]
        public void GenerateClass_WithClassHavingStaticMethodAcceptingOutParameter_MustAddOutKeywordForwarededParameters()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticMethodAcceptingOutParameter));

            Assert.Contains("ClassWithStaticMethodAcceptingOutParameter.Method(out input)", result);
        }


        [Fact]
        public void GenerateClass_WithClassHavingMethodReturningSpecialType_MustTranslateReturnExpression()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethodReturningSpecialType));

            Assert.Contains("return new TranslatedSpecialType(_innerWrappedObject.Method());", result);
        }

        [Fact]
        public void GenerateClass_WithClassHavingGetPropertyOfSpecialType_MustTranslateReturnExpression()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstancePropertyOfSpecialType));

            Assert.Contains("return new TranslatedSpecialType(_innerWrappedObject.Property);", result);
        }

        [Fact]
        public void GenerateClass_WithClassHavingSetPropertyOfSpecialType_MustTranslateReturnExpression()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstancePropertyOfSpecialType));

            Assert.Contains("_innerWrappedObject.Property = value.InnerWrappedObject;", result);
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

            Assert.Contains("_innerWrappedObject.Method(input);", result);
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

        [Fact]
        public void GenerateClassForStaticMembers_WithClassHavingObsoleteProperty_MustNotAddProperty()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithObsoleteStaticProperty));

            Assert.DoesNotContain("public string Property", result);
        }

        [Fact]
        public void GenerateInterfaceForInstanceMembers_WithClassHavingObsoleteMethod_MustNotAddMethod()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(ClassWithObsoleteInstanceMethod));

            Assert.DoesNotContain("string Method()", result);
        }

        [Fact]
        public void GenerateInterface_WithAnyClass_EnclosesInterfaceInNameSpace()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(String));

            Assert.Matches(new System.Text.RegularExpressions.Regex("namespace.+\\{.*interface.*\\}", System.Text.RegularExpressions.RegexOptions.Singleline), result);
        }

        [Fact]
        public void GenerateClass_WithAnyClass_EnclosesClassInNameSpace()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(String));

            Assert.Matches(new System.Text.RegularExpressions.Regex("namespace.+\\{.*class.*\\}", System.Text.RegularExpressions.RegexOptions.Singleline), result);
        }

        [Fact]
        public void GenerateClass_ClassWithInstanceMehtodReturningVoid_ShouldNotHaveReturnStatment()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(ClassWithInstanceMethodReturningVoid));

            Assert.DoesNotContain("return _innerWrappedObject.Method();", result);
        }

        [Fact]
        public void GenerateClass_ClassWithStaticMehtodReturningVoid_ShouldNotHaveReturnStatment()
        {
            var result = _sut.GenerateClassForStaticMembers(typeof(ClassWithStaticMethodReturningVoid));

            Assert.DoesNotContain("return ", result);
        }

        [Fact]
        public void GenerateClass_ClassWithInstanceMembers_MustHaveCtorAcceptingInnerObject()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(String));

            Assert.Contains("public StringWrapper(object innerWrappedObject)", result);
        }

        [Fact]
        public void GenerateClass_WithAnyClass_MustHaveGeneratedInterfaceInBaseList()
        {
            var result = _sut.GenerateClassForInstanceMembers(typeof(String));

            Assert.Contains("public class StringWrapper : IString", result);
        }

        [Fact]
        public void GenerateInterface_WithInstanceClass_MustHaveIAutoWrappedInstanceInterfaceInBaseList()
        {
            var result = _sut.GenerateInterfaceForInstanceMembers(typeof(String));

            Assert.Contains("public interface IString : global::AutoWrapping.IAutoWrappedInstance", result);
        }

        [Fact]
        public void GenerateInterface_WithStaticClass_MustHaveIAutoWrappedInterfaceInBaseList()
        {
            var result = _sut.GenerateInterfaceForStaticMembers(typeof(String));

            Assert.Contains("public interface IString : global::AutoWrapping.IAutoWrapped", result);
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

    public class ClassWithInstanceMethodReturningVoid
    {
        public void Method(string input) { throw new NotImplementedException(); }
    }

    public class ClassWithStaticMethodReturningVoid
    {
        public static void Method(string input) { throw new NotImplementedException(); }
    }

    public class ClassWithInstancePropertyOfSpecialType
    {
        public SpecialType Property { get; set; }
    }

    public class ClassWithInstanceMethodReturningSpecialType
    {
        public SpecialType Method() { throw new NotImplementedException(); }
    }

    public class ClassWithObsoleteInstanceMethod
    {
        [Obsolete("This is obsolete")]
        public string Method() { throw new NotImplementedException(); }
    }

    public class ClassWithInstanceMethodAcceptingParameterOfSpecialType
    {
        public void Method(SpecialType input) { throw new NotImplementedException(); }
    }


    public class ClassWithInstanceMethodAcceptingOutParameter
    {
        public void Method(out string input) { throw new NotImplementedException(); }
    }

    public class ClassWithStaticMethodAcceptingOutParameter
    {
        public static void Method(out string input) { throw new NotImplementedException(); }
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

    public class ClassWithObsoleteStaticProperty
    {
        [Obsolete("This is obsolete")]
        public static string Property { get; set; }
    }


    public class SpecialType
    {

    }

}
