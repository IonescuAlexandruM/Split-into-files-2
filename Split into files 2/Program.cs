using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string sourceFolderPath = "D:\\useless\\DEER\\test";
        string outputRootDirectory = "D:\\useless\\DEER\\test";

        try
        {
            CreateFoldersAndMoveFiles(sourceFolderPath, outputRootDirectory);
            Console.WriteLine("Files moved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CreateFoldersAndMoveFiles(string sourceFolderPath, string outputRootDirectory)
    {
        string[] files = Directory.GetFiles(sourceFolderPath);

        int folderIndex = 1;


        // Create a folder for the current set of files


        long currentFolderSize = 0;
        // Move the files to the folder
        foreach (var filePath in files)
        {
            string folderPath = Path.Combine(outputRootDirectory, $"Folder_{folderIndex}");
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine(folderPath, fileName);

            long currentFileSize = GetFileSize(filePath);
            long maxFolderSize = 49000000;
            //long maxFolderSize = 3500000;
            currentFolderSize += currentFileSize;

            if (currentFolderSize < maxFolderSize)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);

                }
                File.Move(filePath, destinationPath);
            }
            else
            {
                folderIndex++;
                folderPath = Path.Combine(outputRootDirectory, $"Folder_{folderIndex}");
                destinationPath = Path.Combine(folderPath, fileName);
                Directory.CreateDirectory(folderPath);
                File.Move(filePath, destinationPath);
                currentFolderSize = currentFileSize;

            }
        }


    }

    static long GetFileSize(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        // Check if the file exists
        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        // Get the file size in bytes
        long fileSize = fileInfo.Length;

        return fileSize;
    }
}
