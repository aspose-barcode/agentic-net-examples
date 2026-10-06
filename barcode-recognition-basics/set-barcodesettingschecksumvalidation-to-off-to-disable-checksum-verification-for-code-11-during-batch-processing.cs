// Title: Disable Checksum Validation for Code 11 Barcodes in Batch Processing
// Description: Demonstrates how to generate Code 11 barcode images and read them in a batch while disabling checksum verification.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting how to configure BarcodeSettings.ChecksumValidation to Off. Developers working with bulk barcode processing often need to relax checksum checks for legacy or non‑standard Code 11 data, making this pattern useful for high‑throughput scenarios.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, checksumvalidation, batch-processing, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of Code 11 barcode images, then reads them back in a batch with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files, processes them, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for batch processing
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Code 11 barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = "12345" + i; // Sample numeric text
            string filePath = Path.Combine(tempFolder, $"code11_{i}.png");

            // Use BarcodeGenerator to create a PNG image for each barcode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Batch read the generated barcodes with checksum validation turned off
        foreach (string file in barcodeFiles)
        {
            try
            {
                // Initialize BarCodeReader for Code 11 decoding
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code11))
                {
                    // Disable checksum verification for this reader instance
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                    // Iterate over all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be read as a barcode
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}