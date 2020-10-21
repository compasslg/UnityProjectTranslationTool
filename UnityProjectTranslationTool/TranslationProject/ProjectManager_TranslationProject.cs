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
        public static string curProjPath;

        /// <summary>
        /// Open a TranslationProject file and add parse the data.
        /// </summary>
        /// <param name="projPath">a string representing the path of the project file.</param>
        public static void OpenTranslationProject(string projPath)
        {
            // Enqueue progress
            //strBuilder.Clear();
            //strBuilder.Append("Start opening translation project at: ");
            //strBuilder.Append(projPath);
            //progressQueue.Clear();
            //progressQueue.Enqueue(strBuilder.ToString());
            curState = ProgressState.LoadingTranslationProject;
            curProjPath = projPath;
            StreamReader reader = new StreamReader(projPath, encoding);
            projectData = new ProjectData(reader.ReadLine(), reader.ReadLine());
            reader.ReadLine();
            OpenTranslationProjectHelper(reader, projectData);
            reader.Close();
        }

        private static void OpenTranslationProjectHelper(StreamReader reader, BaseFileData cur)
        {
            if (cur is SingleFileData)
                OpenTranslationProjectHelper(reader, cur as SingleFileData);
            else if (cur is FolderData)
                OpenTranslationProjectHelper(reader, cur as FolderData);
            else
                throw new Exception.ProjectCorruptedException(projectData.name, cur.name);
        }

        private static void OpenTranslationProjectHelper(StreamReader reader, SingleFileData cur)
        {
            // append loading progress
            AppendProgress(cur.name);
            string line = reader.ReadLine();

            while (line.Length != 0 && line[0] != '}')
            {
                cur.texts.Add(Text2TextEntry(line));
                line = reader.ReadLine();
            }
        }

        private static void OpenTranslationProjectHelper(StreamReader reader, FolderData cur)
        {
            // append loading progress
            AppendProgress(cur.name);
            string line = reader.ReadLine();
            
            while (line.Length != 0 && line[0] != '}')
            {
                if(line[0] == '{')
                {
                    BaseFileData file;
                    // single file
                    if (line[1] == '#')
                        file = new SingleFileData(line.Substring(2), cur);
                    // folder
                    else
                        file = new FolderData(line.Substring(2), cur);
                    cur.files.Add(file);
                    OpenTranslationProjectHelper(reader, file);
                }
                line = reader.ReadLine();
            }
        }

        public static void SaveTranslationProject(string projPath)
        {
            curState = ProgressState.Saving;
            StreamWriter writer = new StreamWriter(projPath, false, encoding);
            writer.WriteLine(Path.GetFileNameWithoutExtension(projPath));
            writer.WriteLine(projectData.path);
            writer.WriteLine("{");
            SaveTranslationProjectHelper(writer, projectData);
            writer.WriteLine("}");
            writer.Close();
        }

        private static void SaveTranslationProjectHelper(StreamWriter writer, FolderData curFolder)
        {
            // iterate over all file instances in the current folder
            foreach(BaseFileData file in curFolder.files)
            {
                AppendProgress(file.name);
                writer.WriteLine(FileData2Text(file));
                // folder : recurse
                if (file is FolderData)
                    SaveTranslationProjectHelper(writer, file as FolderData);
                // file : iterate over all text entries
                else
                    foreach (TextEntry entry in (file as SingleFileData).texts) writer.WriteLine(TextEntry2Text(entry));
                writer.WriteLine("}");
            }
            writer.Flush();
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
