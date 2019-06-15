using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HtmlScrapper
{
    static class Utilities
    {
        public static void WriteToFile(Dictionary<string, int> traits, string mark, string filepath)
        {
            var line = mark + " | ";
            foreach(var trait in traits)
            {
                line += trait.Key + ":" + trait.Value.ToString() + " ";
            }

            using (StreamWriter file = new StreamWriter(filepath, true))
            {
                file.WriteLine(line);
            }
        }


        public static string GetFilePath()
        {
            string filepath = string.Empty;
            do
            {
                Console.WriteLine("Enter path to save file or press ENTER key to write in default folder");
                filepath = Console.ReadLine();
                if (filepath == string.Empty)
                {
                    string workingDir = Environment.CurrentDirectory;
                    filepath = Directory.GetParent(workingDir).Parent.Parent.FullName + "\\TEST.txt";
                }

            } while (!ValidatePath(filepath));

            return filepath;
        }


        private static bool ValidatePath(string path)
        {
            bool result;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
