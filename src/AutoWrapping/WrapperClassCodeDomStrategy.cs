using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public class WrapperClassCodeDomStrategy : ICodeGeneratorStrategy
    {
        public string GenerateCode(Type typeToGenerate, IList<Type> typesToTranslate, IList<TypeTranslationInfo> specialTypeTranslation)
        {
            var tree = CSharpSyntaxTree.ParseText(@"using Sitecore;");

            var compilation = CSharpCompilation.Create("Sitecore.Kernel")
                .AddReferences(new MetadataFileReference(typeof(Sitecore.Context).Assembly.Location))
                .AddSyntaxTrees(tree);

            return "";
        }
    }
}
