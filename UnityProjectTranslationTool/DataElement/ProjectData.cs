using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.DataElement
{
    class ProjectData : BaseDataContainer
    {
        public string path;
        public ProjectData(string name, string path) : base(name, null)
        {
            this.path = path;
            children = new List<BaseDataElement>();
        }
    }
}
