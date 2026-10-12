// Title: Batch barcode processing with StripFNC to remove FNC symbols
// Description: Demonstrates how to generate multiple barcode images, read them in a batch, and strip Function Code (FNC) symbols during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and the StripFNC setting to remove FNC symbols from the decoded text. Typical use cases include bulk barcode processing, data cleanup, and preparing scanned data for downstream systems. Developers often need to batch‑process images, adjust decoding options, and handle various symbologies efficiently.
// Prompt: Create a batch process that reads multiple images with StripFNC true to strip FNC symbols.
// Tags: barcode, batch, stripfnc, code128, image, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and reading of Code128 barcodes with the StripFNC option enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcode images, reads them while stripping FNC symbols,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchStripFNC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images and collect their file paths
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(batchFolder, $"Sample{i}.png");
            string codeText = $"Sample{i}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Process each generated image with StripFNC = true
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Enable stripping of FNC symbols from the decoded text
                    reader.BarcodeSettings.StripFNC = true;

                    // Read and output all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading {file}: {ex.Message}");
            }
        }

        // Cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}