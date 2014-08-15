using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public class SimpleCodeGenerator<TInterfaceStrategy, TWrapperClassStrategy> : ICodeGenerator<TInterfaceStrategy, TWrapperClassStrategy>
        where TInterfaceStrategy : class, ICodeGeneratorStrategy
        where TWrapperClassStrategy : class, ICodeGeneratorStrategy
    {
        private readonly ICodeGeneratorStrategy _interfaceStrategy;
        private readonly ICodeGeneratorStrategy _wrapperClassStrategy;

        public SimpleCodeGenerator(TInterfaceStrategy interfaceStrategy, TWrapperClassStrategy wrapperClassStrategy)
        {
            _interfaceStrategy = interfaceStrategy;
            _wrapperClassStrategy = wrapperClassStrategy;
        }

        public string GenerateCode(IList<Type> typesToGenerate, IList<Type> typesToTranslate, IList<TypeTranslationInfo> specialTypeTranslation)
        {
            var sb = new StringBuilder();

            foreach (var type in typesToGenerate)
            {
                sb.Append(_interfaceStrategy.GenerateCode(type, typesToTranslate, specialTypeTranslation));
                sb.Append(_wrapperClassStrategy.GenerateCode(type, typesToTranslate, specialTypeTranslation));
            }

            return sb.ToString();
        }
    }
}
