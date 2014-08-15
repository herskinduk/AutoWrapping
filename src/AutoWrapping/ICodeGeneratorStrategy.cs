using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoWrapping
{
    public interface ICodeGeneratorStrategy
    {
        string GenerateCode(Type typeToGenerate, IList<Type> typesToTranslate, IList<TypeTranslationInfo> specialTypeTranslation);
    }
}
