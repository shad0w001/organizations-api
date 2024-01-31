using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFileReader.FileManager
{
    public class FileManager
    {
        public static string GetFilePath(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory does not exist: " + directoryPath);
                Directory.CreateDirectory(directoryPath);
                return null;
            }

            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                if (Path.GetExtension(fileName) != ".csv" && Path.GetExtension(fileName) != ".xlsx")
                {
                    Console.WriteLine("The file is not a valid csv file");
                    return null;
                }

                return file;
            }

            return null;
        }

        public static void MovieFile(string filePath, string outDirectory)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file doees not exist");
                return;
            }

            if(!Directory.Exists(outDirectory))
            {
                Console.WriteLine("Write directory did not exist");
                Console.WriteLine("Creating directory");
                Directory.CreateDirectory(outDirectory);
            }

            File.Move(filePath, outDirectory);
        }
    }
}
