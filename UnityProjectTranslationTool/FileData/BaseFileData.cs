using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.FileData
{
    class BaseFileData
    {
        public string name { get; set; }
        public FolderData dir;
        public BaseFileData(string name, FolderData dir)
        {
            this.name = name;
            this.dir = dir;
        }
    }
}
