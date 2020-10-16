using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager 
    {
        public static void OpenUnityProject(string projName, string path) {
            projectData = new ProjectData(projName);
            ScanFiles(path);
        }
        private static void ScanFiles(string path)
        {
            // iterate over all files in the current directory
            // read information intoa SingleFileData and add it to projectData
            foreach(string filename in Directory.EnumerateFiles(path))
            {
                List<TextFinder.TextEntry> texts = TextFinder.TextFinder.FindText(Path.Combine(path, filename));
                SingleFileData fileData = new SingleFileData(filename, path, texts);
                projectData.files.Add(fileData);
            }

            // iterate over all directories and scan recursively
            foreach(string dirname in Directory.EnumerateDirectories(path))
            {
                ScanFiles(Path.Combine(path, dirname));
            }
        }
    }
}
