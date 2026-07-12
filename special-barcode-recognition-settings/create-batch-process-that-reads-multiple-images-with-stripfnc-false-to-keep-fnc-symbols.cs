// Title: Batch barcode reading with StripFNC disabled
// Description: Demonstrates reading multiple barcode images while preserving FNC symbols by setting StripFNC to false.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure BarCodeReader settings for batch processing of images. It highlights the use of BarCodeReader, BarcodeSettings, and DecodeType to read various symbologies, a common task for developers needing to extract raw barcode data without stripping control characters.
// Prompt: Create a batch process that reads multiple images with StripFNC false to keep FNC symbols.
// Tags: barcode, batch processing, stripfnc, gs1code128, decode, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of barcode images while keeping FNC symbols (StripFNC = false).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample images if needed and reads all PNG files in the InputImages folder,
    /// printing detected barcode information without stripping FNC characters.
    /// </summary>
    static void Main()
    {
        // Define the folder that will contain input images.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");

        // Ensure the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a few sample GS1‑Code128 barcodes containing FNC characters.
        string[] existingFiles = Directory.GetFiles(inputFolder, "*.png");
        if (existingFiles.Length == 0)
        {
            List<string> sampleTexts = new List<string>
            {
                "(01)12345678901231",          // GTIN
                "(01)98765432109876(10)ABCD", // GTIN with lot number
                "(01)55555555555555(21)XYZ"   // GTIN with serial number
            };

            int index = 1;
            foreach (string text in sampleTexts)
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, text))
                {
                    string filePath = Path.Combine(inputFolder, $"Sample{index}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                index++;
            }
        }

        // Retrieve all PNG images from the input folder.
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");

        // Process each image file.
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize the barcode reader for all supported symbologies.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Disable stripping of FNC characters to keep them in the result.
                reader.BarcodeSettings.StripFNC = false;

                // Read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                }
                else
                {
                    // Output details for each detected barcode.
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}