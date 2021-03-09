using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityProjectTranslationTool.DataElement;

namespace UnityProjectTranslationTool.FileData
{
    class SingleFileData : BaseDataElement
    {
        public ObservableCollection<TextFinder.TextEntry> texts { get; }
        public SingleFileData(string name, BaseDataContainer dir) : base(name, dir)
        {
            texts = new ObservableCollection<TextFinder.TextEntry>();
        }
    }
}
