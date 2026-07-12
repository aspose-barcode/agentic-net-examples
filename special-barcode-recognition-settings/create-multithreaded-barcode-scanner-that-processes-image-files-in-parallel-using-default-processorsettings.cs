// Title: Multithreaded Barcode Scanning with Aspose.BarCode
// Description: Demonstrates generating sample Code128 barcodes and scanning them concurrently using Aspose.BarCode's default ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, combined with .NET's Parallel.ForEach to process multiple images in parallel. Developers often need fast, scalable barcode processing pipelines for bulk image analysis, inventory automation, or document digitization.
// Prompt: Create a multithreaded barcode scanner that processes image files in parallel using default ProcessorSettings.
// Tags: code128, scanning, console, barcodegenerator, barcodereader, parallel, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates parallel barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images and scans them concurrently.
    /// </summary>
    static void Main()
    {
        // Define the directory that will hold the sample barcode images.
        string imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(imagesDir);

        // Build a list of file paths for the sample images.
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(imagesDir, $"barcode{i}.png");
            imageFiles.Add(filePath);

            // Create a barcode image if it does not already exist.
            if (!File.Exists(filePath))
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
        }

        // Scan all images in parallel using the default ProcessorSettings.
        Parallel.ForEach(imageFiles, file =>
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                return;
            }

            // Initialize the reader for all supported barcode types.
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        });
    }
}