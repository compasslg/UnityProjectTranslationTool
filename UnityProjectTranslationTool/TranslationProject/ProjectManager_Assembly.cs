using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.AssemblyData;
using UnityProjectTranslationTool.FileData;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Diagnostics;
using UnityProjectTranslationTool.DataElement;
namespace UnityProjectTranslationTool.TranslationProject
{
    partial class ProjectManager
    {
        private static ModuleDefinition curModule;
        public static void OpenUnityGameAssembly(string projName, string path)
        {
            curModule = ModuleDefinition.ReadModule(path);
            
            projectData = new ProjectData(projName, path);
            Console.WriteLine("Path: " + path);
            foreach(var type in curModule.Types)
            {
                Debug.WriteLine("Type: " + type.FullName);
                AssemblyTypeData typeData = ReadType(type, projectData);
                if(typeData != null) projectData.AddChild(typeData);
            }
        }
        private static AssemblyTypeData ReadType(TypeDefinition type, BaseDataContainer dir) {
            AssemblyTypeData typeData = new AssemblyTypeData(type.FullName, dir);
            foreach(var method in type.Methods)
            {
                AssemblyMethodData methodData = ReadMethod(method, typeData);
                if(methodData != null)
                {
                    typeData.methods.Add(methodData);
                }
            }
            foreach(var nestedType in type.NestedTypes)
            {
                AssemblyTypeData nestedTypeData = ReadType(nestedType, dir);
                if(nestedTypeData != null) typeData.nestedTypes.Add(nestedTypeData);
            }
            if (typeData.nestedTypes.Count == 0 && typeData.methods.Count == 0) return null;
            return typeData;
        }

        public static AssemblyMethodData ReadMethod(MethodDefinition method, AssemblyTypeData typeData)
        {
            if (!method.HasBody) return null;
            AssemblyMethodData methodData = new AssemblyMethodData(method.Name, typeData);
            var instructions = method.Body.Instructions;
            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].OpCode == OpCodes.Ldstr)
                {
                    string text = (string)instructions[i].Operand;
                    AssemblyTextEntry textEntry = new AssemblyTextEntry(i, text, null, methodData);
                    methodData.texts.Add(textEntry);
                }
            }
            if (methodData.texts.Count == 0) return null;
            return methodData;
        }
    }
}
