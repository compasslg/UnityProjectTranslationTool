using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace UnityProjectTranslationTool.TextFinder
{
    class TextFinder
    {
        public static ObservableCollection<TextEntry> FindText(string path, ObservableCollection<TextEntry> list) {
            StreamReader reader = new StreamReader(path);
            int lineIndex = 1;
            
            for(string line = reader.ReadLine(); line != null; line = reader.ReadLine()) 
            {
                string[] arr = line.Split('"');
                // read all the text
                for(int i = 1; i < arr.Length; i += 2)
                {
                    // skip all the cases where the string doesn't really represent a text in game
                    if (arr[i - 1].Contains("SetInt"))
                        continue;
                    if (arr[i - 1].Contains("SetBool"))
                        continue;
                    if (arr[i - 1].Contains("SetTrigger"))
                        continue;
                    if (arr[i - 1].Contains("Find"))
                        continue;
                    if (arr[i - 1].Contains("SetColor"))
                        continue;
                    string trimed = arr[i].Trim();
                    if (trimed.Length < 2)
                        continue;
                    if (trimed.Length < 10 && double.TryParse(trimed, out _))
                        continue;
                    // pick out the text
                    list.Add(new TextEntry(lineIndex, (i - 1) / 2, arr[i], null));
                }
                lineIndex++;
            }

            reader.Close();
            reader.Dispose();
            return list;
        }
    }
}
