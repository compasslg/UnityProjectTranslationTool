using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.Exception
{
    class ProjectCorruptedException : System.Exception
    {
        public ProjectCorruptedException(string projName, string line) : base("Project Corrupted: " + projName + "\nLine: " + line) { }
    }
}
