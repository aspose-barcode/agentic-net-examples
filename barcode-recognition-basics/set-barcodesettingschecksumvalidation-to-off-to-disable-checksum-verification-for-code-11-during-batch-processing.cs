// Title: Disable checksum validation for Code 11 barcodes during batch processing
// Description: Demonstrates generating a set of Code 11 barcodes, then reading them with checksum validation turned off. Useful when processing barcodes that may have missing or incorrect checksums.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes and BarCodeReader with BarcodeSettings to control checksum validation. Developers working with bulk barcode processing often need to disable checksum checks for legacy or imperfect data, and this snippet illustrates the typical API usage for such scenarios.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, checksumvalidation, off, batch processing, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and reading of Code 11 barcodes with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary Code 11 barcode images, reads them with checksum validation turned off, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Store paths of generated barcode images
        List<string> barcodeFiles = new List<string>();

        // Generate 5 Code 11 barcode images
        for (int i = 0; i < 5; i++)
        {
            string codeText = "12345" + i;
            string filePath = Path.Combine(tempFolder, $"code11_{i}.png");

            // Use BarcodeGenerator to create a Code 11 barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2; // Set module size
                generator.Save(filePath, BarCodeImageFormat.Png);   // Save as PNG
            }

            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch reading Code11 barcodes with ChecksumValidation.Off");

        // Read each generated barcode with checksum validation disabled
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize BarCodeReader for Code 11
                using (var reader = new BarCodeReader(file, DecodeType.Code11))
                {
                    // Disable checksum verification
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle unreadable image files gracefully
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
            catch (Exception ex)
            {
                // Log any other errors encountered during reading
                Console.WriteLine($"Error reading file {file}: {ex.Message}");
            }
        }

        // Clean up temporary folder and files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}