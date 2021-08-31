using System;
using System.IO;

namespace StringRemovalFromDirectory
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Initial();
        }

        private static void Initial()
        {
            var dir = SetDirectory();
            Console.WriteLine("Directory Selected:" + dir);
            var response = GetDirectoryResponse();
            if (response == "n")
            {
                Initial();
            }

            var files = Directory.GetFiles(dir);
            Console.WriteLine($"Found:{files.Length} files");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            ModifyFiles(dir, files);
        }

        private static void ModifyFiles(string directory, string[] filesInDirectory)
        {
            while (true)
            {
                filesInDirectory = Directory.GetFiles(directory);
                Console.WriteLine("Enter the text to remove from files in directory:" + directory);
                var response = Console.ReadLine();
                if (response == "")
                {
                    response = " ";
                }

                foreach (var file in filesInDirectory)
                {
                    try
                    {
                        var d = directory;
                        var fileWithoutDirectory = Path.GetFileName(file);
                        var renamedFile = fileWithoutDirectory.Replace(response + "", "");
                        d += $@"\{renamedFile}";
                        Console.WriteLine("D Value::" + d);
                        File.Move(file, d);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error with file:{file}::{e}");
                        continue;
                    }
                }

                Console.WriteLine("Feel free to exit the program when you're done editing");
            }
        }

        private static string GetDirectoryResponse()
        {
            Console.WriteLine("Is Directory Correct? Y/N");
            var response = Console.ReadLine();
            response = response.ToLower();
            var responseCharArray = response.ToCharArray();

            if (responseCharArray[0] == 'y' || responseCharArray[0] == 'n')
            {
                return responseCharArray[0].ToString();
            }

            Console.WriteLine("Invalid response..");
            Console.WriteLine("Try again Please use 'y' and 'n'");
            GetDirectoryResponse();
            return response;
        }

        private static string SetDirectory()
        {
            Console.WriteLine("Enter Directory");
            var directory = Console.ReadLine();
            if (Directory.Exists(directory)) return directory;
            Console.WriteLine($"Directory:{directory} does not exist");
            SetDirectory();
            return directory;
        }
    }
}