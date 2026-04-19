using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the network share containing barcode images.
        string inputFolder = @"\\networkshare\barcodes";

        // Verify the folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine("Input folder does not exist: " + inputFolder);
            return;
        }

        // Get a safe sample of image files (up to 10 files).
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            .Take(10)
            .ToArray();

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found in the folder.");
            return;
        }

        // Process each image file.
        foreach (string filePath in imageFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found: " + filePath);
                continue;
            }

            // Use BarCodeReader to detect barcodes in the image.
            using (BarCodeReader reader = new BarCodeReader(filePath))
            {
                // Optional: set desired quality preset.
                // reader.QualitySettings = QualitySettings.NormalQuality;

                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Text: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
    }
}