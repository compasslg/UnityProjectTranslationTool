using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.DataElement
{
    class BaseDataEntry
    {
        public string text { get; }
        public string translation { get; set; }
        public BaseDataElement parent;
        public BaseDataEntry(string text, string translation, BaseDataElement parent)
        {
            this.text = text;
            this.translation = translation;
            this.parent = parent;
        }
    }
}
