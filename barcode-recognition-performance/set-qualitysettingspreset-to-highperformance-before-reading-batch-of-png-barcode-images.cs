using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading barcodes from PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes a limited number of PNG barcode images
    /// located in the "Barcodes" directory and outputs detected barcode information.
    /// </summary>
    static void Main()
    {
        // Directory that should contain the PNG barcode images.
        string imagesDir = "Barcodes";

        // Verify that the directory exists before proceeding.
        if (!Directory.Exists(imagesDir))
        {
            Console.WriteLine($"Directory not found: {imagesDir}");
            return;
        }

        // Retrieve all PNG files in the directory (no recursion).
        string[] pngFiles = Directory.GetFiles(imagesDir, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.WriteLine("No PNG files found in the directory.");
            return;
        }

        // Limit processing to a maximum of 5 images to keep the sample quick.
        int maxSamples = Math.Min(pngFiles.Length, 5);
        Console.WriteLine($"Processing {maxSamples} PNG barcode image(s)...");

        // Iterate over the selected sample files.
        for (int i = 0; i < maxSamples; i++)
        {
            string filePath = pngFiles[i];

            // Ensure the file still exists (it could have been removed meanwhile).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Load the image and create a barcode reader instance.
            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader())
            {
                // Use a high‑performance preset for faster recognition.
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Configure the reader to detect all supported barcode types.
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Assign the bitmap image to the reader.
                reader.SetBarCodeImage(bitmap);

                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }
        }

        // Indicate that processing has finished.
        Console.WriteLine("Processing completed.");
    }
}