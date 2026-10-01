// Title: Disable Checksum Validation for Code 11 Barcodes in Batch Processing
// Description: Demonstrates how to generate Code 11 barcodes, then decode them with checksum validation turned off using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes and BarCodeReader with BarcodeSettings to control checksum validation. Developers often need to process large batches of barcodes where checksum verification may be unnecessary or cause false negatives, especially for Code 11 symbology.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, checksum, batch processing, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of Code 11 barcode images, then reads them back with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files, decodes them without checksum verification, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the batch processing
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // Generate sample Code 11 barcode images and collect their file paths
        // --------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string codeText = "12345" + i; // Sample code text for each barcode
            string filePath = Path.Combine(batchFolder, $"code11_{i}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
            {
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Batch decode the generated barcodes with checksum validation turned off
        // --------------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code11))
                {
                    // Disable checksum verification for Code 11
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                    // Read all barcodes found in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Skip files that cannot be loaded as images (e.g., corrupted or unsupported format)
                Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder (optional)
        // --------------------------------------------------------------------
        try
        {
            foreach (var file in barcodeFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(batchFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}