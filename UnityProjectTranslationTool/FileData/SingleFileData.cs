using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityProjectTranslationTool.FileData
{
    class SingleFileData : BaseFileData
    {
        public ObservableCollection<TextFinder.TextEntry> texts { get; }
        public SingleFileData(string name, FolderData dir) : base(name, dir)
        {
            texts = new ObservableCollection<TextFinder.TextEntry>();
        }
    }
}
