using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.TranslationProject
{
    class ProjectData
    {
        public readonly string name;
        public readonly List<SingleFileData> files;
        public ProjectData(string name)
        {
            this.name = name;
            files = new List<SingleFileData>();
        }
    }
}
