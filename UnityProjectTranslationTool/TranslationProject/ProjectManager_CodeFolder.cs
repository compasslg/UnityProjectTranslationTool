using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityProjectTranslationTool.FileData;
using UnityProjectTranslationTool.TextFinder;
using UnityProjectTranslationTool.DataElement;

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

        public static void ApplyTranslation(string path, BaseDataContainer curFolder)
        {
            foreach(BaseDataElement file in curFolder.children)
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
                    // new line
                    if (curLine != (entry.line - 1))
                    {
                        // join the parts and update the previous line
                        if (curLine >= 0 && parts != null)
                            lines[curLine] = string.Join("\"", parts);
                        // update currently processing line and split to parts
                        curLine = entry.line - 1;
                        parts = lines[curLine].Split('"');
                        
                    }

                    // only update if translation is made; otherwise remain the same
                    if(entry.translation != null && entry.translation.Length > 0)
                        parts[2 * entry.index + 1] = entry.translation;
                }

                lines[curLine] = string.Join("\"", parts);

                File.WriteAllLines(curPath, lines);
            }
        }

        private static void ScanFiles(string path, BaseDataContainer curFolder)
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
                    curFolder.AddChild(fileData);
            }

            // iterate over all directories and scan recursively
            foreach(string dirname in Directory.EnumerateDirectories(path))
            {
                // append loading progress
                AppendProgress(dirname);

                // load folder
                FolderData folder = new FolderData(Path.GetFileName(dirname), curFolder);
                ScanFiles(dirname, folder);
                if(folder.children.Count > 0)
                    curFolder.AddChild(folder);
            }
        }
    }
}
