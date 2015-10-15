using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoWrapping;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.IO;

namespace AutoWrapping.Tests.Integration
{
    public class SitecoreIntegrationTests
    {
        [Fact]
        public void GenerateCode_WithSitecoreClasses_Compiles()
        {
            var configuration = ConfigurationFactory();
            var codeGenerator = new AutoWrapperCodeGenerator(configuration);

            var generatedCode = codeGenerator.Generate();
            var generatedAssembly = Compile(new [] { generatedCode });

            Assert.NotNull(generatedAssembly);
        }

        private AutoWrapperConfiguration ConfigurationFactory()
        {
            return new AutoWrapperConfiguration()
            {
                StaticClasses = new []
                {
                    typeof(Sitecore.Context),
                    typeof(Sitecore.Diagnostics.Log),
                    typeof(Sitecore.Configuration.Factory),
                    typeof(Sitecore.Configuration.Settings)

                },
                InstanceClasses = new []
                {
                    typeof(Sitecore.Data.Items.Item),
                    typeof(Sitecore.Data.Database),
                    typeof(Sitecore.Data.Fields.Field),
		            typeof(Sitecore.Data.ID)
                },
                SpecialTypes = new []
                {
                    // Traslation for instance classes
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.Items.Item), 
                        TranslatedType = "Sitecore.Data.Items.IItem", 
                        ForwardTranslationExpression = "new Sitecore.Data.Items.ItemWrapper(target)",
                        ReverseTranslationExpression = "target.InnerWrappedObject as Sitecore.Data.Items.Item"
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.Database), 
                        TranslatedType = "Sitecore.Data.IDatabase", 
                        ForwardTranslationExpression = "new Sitecore.Data.DatabaseWrapper(target)",
                        ReverseTranslationExpression = "target.InnerWrappedObject as Sitecore.Data.Database"
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.Fields.Field), 
                        TranslatedType = "Sitecore.Data.Fields.IField", 
                        ForwardTranslationExpression = "new Sitecore.Data.Fields.FieldWrapper(target)",
                        ReverseTranslationExpression = "target.InnerWrappedObject as Sitecore.Data.Fields.Field"
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.ID), 
                        TranslatedType = "Sitecore.Data.IID", 
                        ForwardTranslationExpression = "new Sitecore.Data.IDWrapper(target)",
                        ReverseTranslationExpression = "target.InnerWrappedObject as Sitecore.Data.ID"
                    },

                    // Translation for lists
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Collections.FieldCollection), 
                        TranslatedType = "System.Collections.Generic.IEnumerable<Sitecore.Data.Fields.IField>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Fields.FieldWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Collections.ChildList), 
                        TranslatedType = "System.Collections.Generic.IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.Items.Item[]), 
                        TranslatedType = "System.Collections.Generic.IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(IEnumerable<Sitecore.Data.Items.Item>), 
                        TranslatedType = "System.Collections.Generic.IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(List<Sitecore.Data.Database>), 
                        TranslatedType = "System.Collections.Generic.IEnumerable<Sitecore.Data.IDatabase>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.DatabaseWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(System.Collections.Generic.IComparer<Sitecore.Data.Items.Item>), 
                        TranslatedType = "System.Collections.Generic.IComparer<Sitecore.Data.Items.Item>", 
                        ForwardTranslationExpression = "null",
                        ReverseTranslationExpression = "throw new NotImplementedException()"
                    }
                }
            };
        }

        // Borrowed from: http://mhusseini.wordpress.com/2014/05/23/roslyn-compile-c-expressions-without-using-the-scripting-api/
        private static Assembly Compile(params string[] sources)
        {
            var assemblyFileName = "gen" + Guid.NewGuid().ToString().Replace("-", "") + ".dll";
            var compilation = CSharpCompilation.Create(assemblyFileName,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,warningLevel: 1),
                syntaxTrees: from source in sources
                             select CSharpSyntaxTree.ParseText(source),
                references: new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(AutoWrapping.IAutoWrapped).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Sitecore.Context).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Configuration.AppSettingsReader).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Configuration.Provider.ProviderBase).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Xml.ConformanceLevel).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Web.ApplicationShutdownReason).Assembly.Location)
                });

            EmitResult emitResult;

            using (var ms = new MemoryStream())
            {
                emitResult = compilation.Emit(ms);

                if (emitResult.Success)
                {
                    var assembly = Assembly.Load(ms.GetBuffer());
                    return assembly;
                }
            }

            var message = string.Join("\r\n", emitResult.Diagnostics);
            throw new ApplicationException(message);
        }
    }
}
