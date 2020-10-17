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
            uint lineIndex = 1;
            for(string line = reader.ReadLine(); line != null; line = reader.ReadLine()) 
            {
                string[] arr = line.Split('"');
                // read all the text
                for(uint i = 1; i < arr.Length; i += 2)
                {
                    // skip all the cases where the string doesn't really represent a text in game
                    if (arr[i - 1].Contains("SetInt"))
                        continue;
                    else if (arr[i - 1].Contains("SetBool"))
                        continue;
                    else if (arr[i - 1].Contains("SetTrigger"))
                        continue;
                    else if (arr[i - 1].Contains("Find"))
                        continue;

                    // pick out the text
                    list.Add(new TextEntry(lineIndex, (i - 1) / 2, arr[i], null));
                }
                lineIndex++;
            }
            return list;
        }
    }
}
