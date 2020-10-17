using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.FileData;
using System.Diagnostics;
namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager
    {
        public static ProjectData projectData;
        public const string spliter = "$-$";
        public static Queue<string> progressQueue = new Queue<string>();
        public static bool onProgress;

        public static void PrepareProgress()
        {
            onProgress = true;
            progressQueue.Clear();
        }

        public static void EndProgress()
        {
            onProgress = false;
        }
        public static void AppendProgress(string cur)
        {
            strBuilder.Clear();
            strBuilder.Append("Loading ");
            strBuilder.Append(cur);
            Trace.WriteLine(strBuilder.ToString());
            /*
            progressQueue.Enqueue(strBuilder.ToString());
            */
        }
    }
}
