using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityProjectTranslationTool.TextFinder;
namespace UnityProjectTranslationTool.TranslationProject
{
    static partial class ProjectManager
    {
        /// <summary>
        /// Open a TranslationProject file and add parse the data.
        /// </summary>
        /// <param name="projPath">a string representing the path of the project file.</param>
        public static void OpenTranslationProject(string projPath)
        {
            StreamReader reader = new StreamReader(projPath);
            List<TextEntry> list;
            projectData = new ProjectData(reader.ReadLine());
            int fileIndex = -1;
            for (string line = reader.ReadLine(); line != null;line = reader.ReadLine()) {
                // add file
                if (line[0] == '#') {
                    string name = line.Substring(1);
                    string path = reader.ReadLine();
                    list = new List<TextEntry>();
                    projectData.files.Add(new SingleFileData(name, path, list));
                    fileIndex++;
                }
                // incorrect format: try adding entry without file info 
                else if(fileIndex < 0)
                {
                    throw new Exception.ProjectCorruptedException(projectData.name, line);
                }
                // add entry
                else
                {
                    projectData.files[fileIndex].texts.Add(ParseData(line));
                }
            }
            reader.Close();
        }

        public static void SaveTranslationProject(string projPath)
        {
            StreamWriter writer = new StreamWriter(File.OpenWrite(projPath));
            writer.WriteLine(projectData.name);
            StringBuilder strBuilder = new StringBuilder();
            // iterate over all SingleFileData instances in projectData
            foreach(SingleFileData file in projectData.files)
            {
                writer.WriteLine("#" + file.name);
                writer.WriteLine(file.dir);

                // iterate over all text entries
                foreach(TextEntry entry in file.texts)
                {
                    strBuilder.Append(entry.line);
                    strBuilder.Append(entry.index);
                    strBuilder.Append(entry.text);
                    strBuilder.Append(entry.translation);
                    writer.WriteLine(strBuilder.ToString());
                    strBuilder.Clear();
                }
            }
            writer.Close();
        }

        /// <summary>
        /// Parse a line of string text into TextEntry.
        /// </summary>
        /// <param name="text">a string representing a line of TextEntry data in the format of "line$-$index$-$original$-$translated"</param>
        /// <returns>an instance of TextEntry containing the information of a text.</returns>
        private static TextEntry ParseData(string text) {
            // split by wierd text in case it actually shows up in the text
            string[] arr = text.Split(new [] {spliter}, StringSplitOptions.None);
            if (arr.Length != 4)
                throw new Exception.ProjectCorruptedException(projectData.name, text);

            uint line = uint.Parse(arr[0]);
            uint index = uint.Parse(arr[1]);
            return new TextEntry(line, index, arr[2], arr[3]);
        } 


    }
}
