using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.FileData;
using System.Windows.Data;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.AssemblyData
{
    class AssemblyTypeData : BaseDataContainer
    {
        public List<AssemblyTypeData> nestedTypes;
        public List<AssemblyMethodData> methods;
        public AssemblyTypeData(string name, BaseDataContainer dir) : base(name, dir){
            nestedTypes = new List<AssemblyTypeData>();
            methods = new List<AssemblyMethodData>();
            children = new CompositeCollection();
            children.Add(new CollectionContainer { Collection = nestedTypes });
            children.Add(new CollectionContainer { Collection = methods });
        }

        public override void AddChild(BaseDataElement child)
        {
            if(child is AssemblyTypeData typeData)
            {
                nestedTypes.Add(typeData);
            }
            else if(child is AssemblyMethodData methodData)
            {
                methods.Add(methodData);
            }
        }
    }
}
