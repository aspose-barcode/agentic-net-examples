// Title: Batch barcode reading with StripFNC disabled to retain FNC symbols
// Description: Demonstrates generating multiple Code128 barcode images, then reading them in a batch while keeping FNC characters by setting StripFNC to false.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to configure BarcodeSettings (StripFNC) for preserving FNC symbols. Typical use cases include batch processing of scanned documents where FNC characters carry meaning, such as inventory systems or shipping labels. Developers often need to generate sample images, read them in bulk, and control decoding options via the API.
// Prompt: Create a batch process that reads multiple images with StripFNC false to keep FNC symbols.
// Tags: code128, batch, stripfnc, barcode-generation, barcode-recognition, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and reading of Code128 barcodes while preserving FNC symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, reads them with StripFNC disabled, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold generated barcode image file paths
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcode images
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(batchFolder, $"Code128Sample{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Set barcode module size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save as PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch reading barcodes with StripFNC = false (keep FNC symbols):");

        // Read each barcode image with StripFNC set to false
        foreach (string file in barcodeFiles)
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
                    // Preserve FNC symbols during decoding
                    reader.BarcodeSettings.StripFNC = false;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file {Path.GetFileName(file)} due to error: {ex.Message}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(batchFolder))
            {
                Directory.Delete(batchFolder, true);
            }
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}