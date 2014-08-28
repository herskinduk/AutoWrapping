using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoWrapping;

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

            //Assert
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
                        TranslatedType = "IEnumerable<Sitecore.Data.Fields.IField>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Fields.FieldWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Collections.ChildList), 
                        TranslatedType = "IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(Sitecore.Data.Items.Item[]), 
                        TranslatedType = "IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(IEnumerable<Sitecore.Data.Items.Item>), 
                        TranslatedType = "IEnumerable<Sitecore.Data.Items.IItem>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.Items.ItemWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(List<Sitecore.Data.Database>), 
                        TranslatedType = "IEnumerable<Sitecore.Data.IDatabase>", 
                        ForwardTranslationExpression = "target.Select(x => new Sitecore.Data.DatabaseWrapper(x)).AsEnumerable()",
                        ReverseTranslationExpression = "target" //TODO FIX
                    },
                    new TypeTranslationInfo() { 
                        ActualType = typeof(System.Collections.Generic.IComparer<Sitecore.Data.Items.Item>), 
                        TranslatedType = "System.Collections.Generic.IComparer<Sitecore.Data.Items.Item>", 
                        ForwardTranslationExpression = "target",
                        ReverseTranslationExpression = "target"
                    }
                }
            };
        }
    }
}
