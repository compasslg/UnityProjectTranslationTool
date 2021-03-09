using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.TextFinder
{
    class TextEntry : BaseDataEntry
    {
        public int line { get; }
        public int index { get; }
        public TextEntry(int line, int index, string text, string translation = null, BaseDataElement parent = null)
            : base(text, translation, parent)
        {
            this.line = line;
            this.index = index;
        }
    }
}
