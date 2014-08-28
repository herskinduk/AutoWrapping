using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoWrapping
{
    public class AutoWrapperConfiguration
    {
        public Type[] StaticClasses { get; set; }
        public Type[] InstanceClasses { get; set; }
        public TypeTranslationInfo[] SpecialTypes { get; set; }
    }
}
