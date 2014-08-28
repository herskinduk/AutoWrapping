using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoWrapping
{
    public class AutoWrapperCodeGenerator
    {
        private readonly AutoWrapperConfiguration _configuration;
        private readonly RoslynCodeGenerator _codeGenerator;

        public AutoWrapperCodeGenerator(AutoWrapperConfiguration configuration)
        {
            this._configuration = configuration;
            _codeGenerator = new RoslynCodeGenerator(configuration.SpecialTypes);
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            foreach (var staticClass in _configuration.StaticClasses)
            {
                sb.Append(_codeGenerator.GenerateInterfaceForStaticMembers(staticClass));
                sb.Append(_codeGenerator.GenerateClassForStaticMembers(staticClass));
            }

            foreach (var instaceClass in _configuration.InstanceClasses)
            {
                sb.Append(_codeGenerator.GenerateInterfaceForInstanceMembers(instaceClass));
                sb.Append(_codeGenerator.GenerateClassForInstanceMembers(instaceClass));
            }

            return sb.ToString();
        }
    }
}
