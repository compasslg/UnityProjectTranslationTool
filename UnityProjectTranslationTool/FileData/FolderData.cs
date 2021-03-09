using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.FileData
{
    class FolderData : BaseDataContainer
    {
        public FolderData(string name, BaseDataContainer dir) : base(name, dir)
        {
            children = new List<BaseDataElement>();
        }
    }
}
