using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFileReader.Paths
{
    public static class Paths
    {
        public static string GetWritePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            // Go three folders up
            for (int i = 0; i < 3; i++)
            {
                DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);
                if (parentDirectory != null)
                {
                    currentDirectory = parentDirectory.FullName;
                }
            }

            currentDirectory = Path.Combine(currentDirectory, "Paths");

            currentDirectory = Path.Combine(currentDirectory, "Write");

            return currentDirectory;
        }
        public static string GetReadPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            for (int i = 0; i < 3; i++)
            {
                DirectoryInfo parentDirectory = Directory.GetParent(currentDirectory);
                if (parentDirectory != null)
                {
                    currentDirectory = parentDirectory.FullName;
                }
            }

            currentDirectory = Path.Combine(currentDirectory, "Paths");

            currentDirectory = Path.Combine(currentDirectory, "Read");

            return currentDirectory;
        }
    }
}
