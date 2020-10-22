using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityProjectTranslationTool.FileData;
using UnityProjectTranslationTool.TextFinder;

namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager 
    {
        public static void OpenUnityProject(string projName, string path) {
            projectData = null;
            GC.Collect();
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

        public static void ApplyTranslation(string path, FolderData curFolder)
        {
            foreach(BaseFileData file in curFolder.files)
            {
                string curPath = Path.Combine(path, file.name);
                // folder
                if(file is FolderData)
                {
                    ApplyTranslation(curPath, file as FolderData);
                    continue;
                }

                // file
                string[] lines = File.ReadAllLines(curPath);
                string[] parts = null;
                int curLine = -1;
                foreach(TextEntry entry in (file as SingleFileData).texts)
                {
                    if (curLine != (entry.line - 1))
                    {
                        if (curLine >= 0 && parts != null)
                            lines[curLine] = string.Join("\"", parts);
                        curLine = entry.line - 1;
                        parts = lines[curLine].Split('"');
                        
                    }

                    parts[entry.index] = entry.translation;
                }
                File.WriteAllLines(curPath, lines);
            }
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
