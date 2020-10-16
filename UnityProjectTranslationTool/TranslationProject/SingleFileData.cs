using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.TranslationProject
{
    class SingleFileData
    {
        public readonly string name;
        public readonly string dir;
        public readonly List<TextFinder.TextEntry> texts;
        public SingleFileData(string name, string dir, List<TextFinder.TextEntry> texts)
        {
            this.name = name;
            this.dir = dir;
            this.texts = texts;
        }
    }
}
