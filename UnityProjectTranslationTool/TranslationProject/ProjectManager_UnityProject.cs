using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityProjectTranslationTool.FileData;

namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager 
    {
        public static void OpenUnityProject(string projName, string path) {
            curState = ProgressState.LoadingUnityProject;
            // Enqueue progress
            strBuilder.Clear();
            strBuilder.Append("Start opening unity project at: ");
            strBuilder.Append(path);
            progressQueue.Clear();
            progressQueue.Enqueue(strBuilder.ToString());

            projectData = new ProjectData(projName, path);
            ScanFiles(path, projectData);
        }
        private static void ScanFiles(string path, FolderData curFolder)
        {
            // iterate over all files in the current directory
            // read information intoa SingleFileData and add it to projectData
            foreach(string filename in Directory.EnumerateFiles(path))
            {
                // append loading progress
                AppendProgress(filename);

                // load file
                SingleFileData fileData = new SingleFileData(Path.GetFileName(filename), curFolder);
                TextFinder.TextFinder.FindText(filename, fileData.texts);
                if(fileData.texts.Count > 0)
                    curFolder.files.Add(fileData);
            }

            // iterate over all directories and scan recursively
            foreach(string dirname in Directory.EnumerateDirectories(path))
            {
                // append loading progress
                AppendProgress(dirname);

                // load folder
                FolderData folder = new FolderData(Path.GetFileName(dirname), curFolder);
                ScanFiles(dirname, folder);
                if(folder.files.Count > 0)
                    curFolder.files.Add(folder);
            }
        }
    }
}
