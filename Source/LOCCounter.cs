using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLOC_Tools.Source
{
    public class LOCCounter
    {
        private string currentDir;
        private int totalLines;

        public string CurrentDir => currentDir;

        public LOCCounter()
        {
            currentDir = @"C:\Workspace\Disorder\Source\";
        }

        void NegativeDown(string dir)
        {
            if (!Directory.Exists(currentDir + dir) || dir == "")
            {
                Console.WriteLine("Directory not exist");
                return;
            }
            currentDir += dir + @"\";
        }

        void NegativeUp()
        {
            DirectoryInfo parentDir = Directory.GetParent(currentDir);
            if (parentDir != null)
            {
                currentDir = parentDir.FullName;
            }
            else
            {
                Console.WriteLine("Already at the root directory.");
            }
        }

        void DisplayFilesAndFolders(string path, string indent)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                Console.WriteLine(indent + Path.GetFileName(file));
            }

            string[] folders = Directory.GetDirectories(path);

            foreach (string folder in folders)
            {
                Console.WriteLine(indent + Path.GetFileName(folder));
                DisplayFilesAndFolders(folder, indent + "  ");
            }
        }

        void DisplayLinesInCsFiles(string path, string indent)
        {
            string[] files = Directory.GetFiles(path, "*.cs");
            foreach (string file in files)
            {
                int fileLineCount = File.ReadAllLines(file).Length;
                totalLines += fileLineCount;

                Console.WriteLine($"{indent}{Path.GetFileName(file)}: {fileLineCount}");
            }

            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                Console.WriteLine($"{indent}{Path.GetFileName(folder)}:");
                DisplayLinesInCsFiles(folder, indent + "  ");
            }
        }

        public void Command(string cmd)
        {
            if (cmd.Contains("cd "))
                NegativeDown(cmd.Remove(0, 3));


            if (cmd == "..")
                NegativeUp();

            if (cmd == "show all files")
            {
                Console.WriteLine("--------------------------------------------");
                DisplayFilesAndFolders(currentDir, String.Empty);
                Console.WriteLine("--------------------------------------------");
            }

            if (cmd == "CLOC cs")
            {
                Console.WriteLine("--------------------------------------------");
                DisplayLinesInCsFiles(currentDir, String.Empty);
                Console.WriteLine("\nTotal lines: " + totalLines);
                Console.WriteLine("--------------------------------------------");
            }
        }
    }
}
