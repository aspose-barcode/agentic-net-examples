using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Expect a directory path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a directory path as an argument.");
            return;
        }

        string directoryPath = args[0];

        // Verify the directory exists; create it if missing and exit gracefully
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory does not exist. Creating: {directoryPath}");
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Directory created. Please add barcode images and run the program again.");
            return;
        }

        // Get image files (common extensions)
        string[] imageFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly);
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No files found in the specified directory.");
            return;
        }

        foreach (string filePath in imageFiles)
        {
            // Simple filter for image extensions
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp" && extension != ".gif" && extension != ".tif" && extension != ".tiff")
                continue;

            using (var reader = new BarCodeReader(filePath))
            {
                // Apply default checksum validation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: Type={result.CodeTypeName}, Confidence={result.Confidence}");
                }
            }
        }
    }
}