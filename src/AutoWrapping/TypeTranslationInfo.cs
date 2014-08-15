using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoWrapping
{
    public class TypeTranslationInfo
    {
        public Type ActualType { get; set; }
        public string TranslatedType { get; set; }
        public string TranslationExpression { get; set; }
    }
}
