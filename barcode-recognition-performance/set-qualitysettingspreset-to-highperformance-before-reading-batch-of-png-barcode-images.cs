// Title: High‑Performance Barcode Recognition from PNG Images
// Description: Demonstrates setting QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images, improving recognition speed.
// Category-Description: This example belongs to the Aspose.BarCode image‑processing category, showcasing how to generate barcode images, store them, and efficiently recognize them using the BarCodeReader. It highlights key API classes such as BarcodeGenerator, BarCodeReader, QualitySettings, and DecodeType, which developers commonly use for batch barcode scanning and performance tuning.
// Prompt: Set QualitySettings.Preset to HighPerformance before reading a batch of PNG barcode images.
// Tags: barcode symbology, generation, recognition, png, qualitysettings, highperformance, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a small batch of PNG barcode images and reads them using a high‑performance quality preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates sample barcodes, saves them as PNG files, and reads them with
    /// <see cref="QualitySettings.HighPerformance"/> to demonstrate faster recognition.
    /// </summary>
    static void Main()
    {
        // Define a folder to store sample barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        // Generate a small batch of sample PNG barcode images (5 items)
        for (int i = 1; i <= 5; i++)
        {
            string fileName = $"barcode{i}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Retrieve all PNG files from the folder
        string[] pngFiles = Directory.GetFiles(folderPath, "*.png");

        // Process each PNG image in the folder
        foreach (string pngFile in pngFiles)
        {
            if (!File.Exists(pngFile))
            {
                Console.WriteLine($"File not found: {pngFile}");
                continue;
            }

            // Initialize a reader for the image, using all supported decode types
            using (BarCodeReader reader = new BarCodeReader(pngFile, DecodeType.AllSupportedTypes))
            {
                // Apply the high‑performance quality preset before reading
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read and output detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(pngFile)} - Detected CodeText: {result.CodeText}");
                }
            }
        }
    }
}