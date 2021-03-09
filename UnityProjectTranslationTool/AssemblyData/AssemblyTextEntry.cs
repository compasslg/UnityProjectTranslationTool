using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.AssemblyData
{
    class AssemblyTextEntry : BaseDataEntry
    {
        public int instIndex;
        public AssemblyTextEntry(int instIndex, string text, string translation, AssemblyMethodData methodData)
            : base(text, translation, methodData)
        {
            this.instIndex = instIndex;
        }
    }
}
