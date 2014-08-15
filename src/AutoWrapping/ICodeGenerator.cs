using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public interface ICodeGenerator<TInterfaceStrategy, TWrapperClassStrategy>
        where TInterfaceStrategy : class, ICodeGeneratorStrategy
        where TWrapperClassStrategy : class, ICodeGeneratorStrategy
    {
        string GenerateCode(IList<Type> typesToGenerate, IList<Type> typesToTranslate, IList<TypeTranslationInfo> specialTypeTranslation);
    }
}
