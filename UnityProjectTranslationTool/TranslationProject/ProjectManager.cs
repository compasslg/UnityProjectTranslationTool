using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityProjectTranslationTool.DataElement;
using System.Diagnostics;
namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager
    {
        public static ProjectData projectData;
        public static Encoding encoding = Encoding.GetEncoding("GB2312");
        public const string spliter = "$-$";
        public static Queue<string> progressQueue = new Queue<string>();
        public static bool onProgress;
        public enum ProgressState {LoadingUnityProject, LoadingTranslationProject, Saving}
        public static ProgressState curState;

        public static void PrepareProgress()
        {
            onProgress = true;
            progressQueue.Clear();
        }

        public static void EndProgress()
        {
            onProgress = false;
        }

        public static void SetState(ProgressState state)
        {
            curState = state;
            
        }

        public static void AppendProgress(string cur)
        {
            strBuilder.Clear();
            if(curState == ProgressState.Saving)
                strBuilder.Append("Saving ");
            else
                strBuilder.Append("Loading ");
            strBuilder.Append(cur);
            Trace.WriteLine(strBuilder.ToString());
            /*
            progressQueue.Enqueue(strBuilder.ToString());
            */
        }
    }
}
