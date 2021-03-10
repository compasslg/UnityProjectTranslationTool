using System.Collections.Generic;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.AssemblyData
{
    class AssemblyMethodData : BaseDataElement
    {
        public List<AssemblyTextEntry> texts { get; }
        public AssemblyMethodData(string name, AssemblyTypeData typeData) : base(name, typeData)
        {
            texts = new List<AssemblyTextEntry>();
        }
        
    }
}
