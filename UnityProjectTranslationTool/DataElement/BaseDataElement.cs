using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.DataElement
{
    class BaseDataElement
    {
        public string name { get; }
        public BaseDataContainer parent { get; }
        public BaseDataElement(string name, BaseDataContainer parent)
        {
            this.name = name;
            this.parent = parent;
        }
    }
}
