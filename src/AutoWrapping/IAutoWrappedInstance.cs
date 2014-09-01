using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWrapping
{
    public interface IAutoWrappedInstance : IAutoWrapped
    {
        object InnerWrappedObject { get; set; }
    }
}
