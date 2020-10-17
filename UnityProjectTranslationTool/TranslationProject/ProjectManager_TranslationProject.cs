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
        private static StringBuilder strBuilder = new StringBuilder();

        /// <summary>
        /// Open a TranslationProject file and add parse the data.
        /// </summary>
        /// <param name="projPath">a string representing the path of the project file.</param>
        public static void OpenTranslationProject(string projPath)
        {
            // Enqueue progress
            strBuilder.Clear();
            strBuilder.Append("Start opening translation project at: ");
            strBuilder.Append(projPath);
            progressQueue.Clear();
            progressQueue.Enqueue(strBuilder.ToString());

            StreamReader reader = new StreamReader(projPath);
            projectData = new ProjectData(reader.ReadLine(), projPath);
            OpenTranslationProjectHelper(reader, projectData);
            reader.Close();
        }

        private static void OpenTranslationProjectHelper(StreamReader reader, BaseFileData cur)
        {
            // append loading progress
            AppendProgress(cur.name);

            string line = reader.ReadLine();
            // Base Case: end of file scope
            if (line.Length == 0 || line[0] == '}')
                return;

            // FileData
            if (line[0] == '{')
            {
                FolderData dir = cur as FolderData;
                if (dir == null) throw new Exception.ProjectCorruptedException(projectData.name, line);
                BaseFileData file;
                // single file
                if (line[1] == '#')
                    file = new SingleFileData(line.Substring(2), dir);
                // folder
                else
                    file = new FolderData(line.Substring(2), dir);
                dir.files.Add(file);
                OpenTranslationProjectHelper(reader, file);
                return;
            }
            // TextEntries
            SingleFileData code = cur as SingleFileData;
            if(code == null) throw new Exception.ProjectCorruptedException(projectData.name, line);
            while (line != null && line[0] != '}')
            {
                code.texts.Add(Text2TextEntry(line));
                line = reader.ReadLine();
            }

        }

        public static void SaveTranslationProject(string projPath)
        {
            StreamWriter writer = new StreamWriter(File.OpenWrite(projPath));
            writer.WriteLine(projectData.path);
            writer.WriteLine(projectData.name);
            SaveTranslationProjectHelper(writer, projectData);
            writer.Close();
        }

        private static void SaveTranslationProjectHelper(StreamWriter writer, FolderData curFolder)
        {
            // iterate over all file instances in the current folder
            foreach(BaseFileData file in curFolder.files)
            {
                writer.WriteLine(FileData2Text(file));
                // folder : recurse
                if(file is FolderData)
                    SaveTranslationProjectHelper(writer, curFolder);
                // file : iterate over all text entries
                else
                    foreach (TextEntry entry in (file as SingleFileData).texts) TextEntry2Text(entry);
                writer.WriteLine("}");
            }
        }



        /// <summary>
        /// Parse a line of string text into TextEntry.
        /// </summary>
        /// <param name="text">a string representing a line of TextEntry data in the format of "line$-$index$-$original$-$translated"</param>
        /// <returns>an instance of TextEntry containing the information of a text.</returns>
        private static TextEntry Text2TextEntry(string text) {
            // split by wierd text in case it actually shows up in the text
            string[] arr = text.Split(new [] {spliter}, StringSplitOptions.None);
            if (arr.Length != 4)
                throw new Exception.ProjectCorruptedException(projectData.name, text);

            uint line = uint.Parse(arr[0]);
            uint index = uint.Parse(arr[1]);
            return new TextEntry(line, index, arr[2], arr[3]);
        } 

        private static string TextEntry2Text(TextEntry entry)
        {
            strBuilder.Clear();
            strBuilder.Append(entry.line);
            strBuilder.Append(spliter);
            strBuilder.Append(entry.index);
            strBuilder.Append(spliter);
            strBuilder.Append(entry.text);
            strBuilder.Append(spliter);
            strBuilder.Append(entry.translation);
            return strBuilder.ToString();
        }

        private static string FileData2Text(BaseFileData data)
        {
            strBuilder.Clear();
            strBuilder.Append("{");
            if(data is SingleFileData)
            {
                strBuilder.Append("#");
            }
            strBuilder.Append(data.name);
            return strBuilder.ToString();
        }
    }
}
