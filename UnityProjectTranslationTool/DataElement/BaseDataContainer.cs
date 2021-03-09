using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.DataElement
{
    class BaseDataContainer : BaseDataElement
    {
        public IList children { get; set; }
        public BaseDataContainer(string name, BaseDataContainer parent) : base(name, parent) { }
        public virtual void AddChild(BaseDataElement child)
        {
            children.Add(child);
        }
    }
}
