using System;
using System.IO;
using System.Linq;

namespace PCFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string _sourceFolder = @"E:\Plurasight\Y";
            const string _targetFolder = @"E:\Temp\P\";
            var _moveFolder = @"E:\Temp\Move\";

            Console.WriteLine("-----------File Filter--------");
            try
            {
                var SourceDirectory = new DirectoryInfo(_sourceFolder);
                var sourceFolderList = SourceDirectory
                    .GetDirectories()
                    .Select(subDirectory => subDirectory.Name);

                var targetDirectory = new DirectoryInfo(_targetFolder);
                var targetFolderList = targetDirectory
                    .GetDirectories()
                    .Select(subDirectory => subDirectory.Name);

                foreach (var source in sourceFolderList)
                {
                    //var found = false;
                    if (targetFolderList.Where(p => p == source).Count() == 1)
                    {
                        Console.WriteLine($"{source} : FOUND");
                        var sourcePath = $"{_sourceFolder}\\{source}";
                        var zipFiles = Directory.GetFiles(sourcePath, "*.zip", SearchOption.AllDirectories);

                        //*Move Zip files to target folder**/
                        foreach (var zipfile in zipFiles)
                        {
                            File.Move($"{zipfile}", $"{_targetFolder}{source}\\{Path.GetFileName(zipfile)}", true);
                        }
                        //*Move the entiar folder to delete location*//
                        Directory.Move(sourcePath, $"{_moveFolder}{source}");
                    }
                    else
                    {
                        Console.WriteLine($"{source} : NOT FOUND");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            Console.ReadKey();
        }
    }
}
