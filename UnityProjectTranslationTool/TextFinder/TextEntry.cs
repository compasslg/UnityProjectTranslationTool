using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.TextFinder
{
    struct TextEntry
    {
        public uint line { get; }
        public uint index { get; }
        public string text { get; }
        public string translation { get; set; }
        public TextEntry(uint line, uint index, string text, string translation)
        {
            this.line = line;
            this.index = index;
            this.text = text;
            this.translation = translation;
        }
    }
}
