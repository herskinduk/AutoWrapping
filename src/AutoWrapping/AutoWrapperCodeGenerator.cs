﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoWrapping
{
    public class AutoWrapperCodeGenerator
    {
        private readonly AutoWrapperConfiguration _configuration;
        private readonly RoslynCodeGenerator _codeGenerator;

        public AutoWrapperCodeGenerator(AutoWrapperConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            this._configuration = configuration;
            _codeGenerator = new RoslynCodeGenerator(configuration.SpecialTypes, assemblies);
        }
        public AutoWrapperCodeGenerator(AutoWrapperConfiguration configuration)
        {
            this._configuration = configuration;
            _codeGenerator = new RoslynCodeGenerator(configuration.SpecialTypes);
        }


        public string Generate()
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");

            foreach (var staticClass in _configuration.StaticClasses)
            {
                sb.AppendLine(_codeGenerator.GenerateInterfaceForStaticMembers(staticClass));
                sb.AppendLine(_codeGenerator.GenerateClassForStaticMembers(staticClass));
            }

            foreach (var instanceClass in _configuration.InstanceClasses)
            {
                sb.AppendLine(_codeGenerator.GenerateInterfaceForInstanceMembers(instanceClass));
                sb.AppendLine(_codeGenerator.GenerateClassForInstanceMembers(instanceClass));
            }

            return sb.ToString();
        }
    }
}
