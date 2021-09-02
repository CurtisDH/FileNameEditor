using System;
using System.IO;

namespace StringRemovalFromDirectory
{
    internal static class Program
    {
        private static string _globalDirectory;

        public static void Main(string[] args)
        {
            Initial();
        }

        private static void Initial()
        {
            SetDirectory();
            Console.WriteLine("Directory Selected:" + _globalDirectory);
            Console.WriteLine("Is Directory Correct? Y/N");
            var response = GetYesOrNoResponse();
            if (response == "n")
            {
                Initial();
            }

            var files = Directory.GetFiles(_globalDirectory);
            Console.WriteLine($"Found:{files.Length} files");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            ModifyFiles(_globalDirectory, files);
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

                Console.WriteLine("Continue editing?");
                var input = GetYesOrNoResponse();
                if (input != "n") continue;
                Initial();
                break;
            }
        }

        private static string GetYesOrNoResponse()
        {
            var response = Console.ReadLine();
            if (!string.IsNullOrEmpty(response))
            {
                response = response.ToLower();
                var responseCharArray = response.ToCharArray();

                if (responseCharArray[0] == 'y' || responseCharArray[0] == 'n')
                {
                    return responseCharArray[0].ToString();
                }
            }

            Console.WriteLine("Invalid response..");
            Console.WriteLine("Try again Please use 'y' and 'n'");
            GetYesOrNoResponse();
            return response;
        }

        private static void SetDirectory()
        {
            while (true)
            {
                Console.WriteLine("Enter Directory");
                var directory = Console.ReadLine();
                if (Directory.Exists(directory))
                {
                    _globalDirectory = directory;
                    return;
                }

                Console.WriteLine($"Directory:{directory} does not exist");
            }
        }
    }
}