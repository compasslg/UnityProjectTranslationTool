using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace UnityProjectTranslationTool.FileData
{
    class FolderData : BaseFileData
    {
        public ObservableCollection<BaseFileData> files { get; }
        public FolderData(string name, FolderData dir) : base(name, dir)
        {
            files = new ObservableCollection<BaseFileData>();
        }
    }
}
