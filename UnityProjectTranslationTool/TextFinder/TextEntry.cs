using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.TextFinder
{
    class TextEntry
    {
        public int line { get; }
        public int index { get; }
        public string text { get; }
        public string translation {get; set;}
        public TextEntry(int line, int index, string text, string translation)
        {
            this.line = line;
            this.index = index;
            this.text = text;
            this.translation = translation;
        }
    }
}
