// Title: Disable Checksum Validation for Code 11 Barcodes in Batch Processing
// Description: Demonstrates how to generate a set of Code 11 barcode images and read them with checksum validation turned off using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and BarcodeSettings.ChecksumValidation to control checksum verification. Typical scenarios include batch processing of barcodes where checksum errors must be ignored, such as legacy data migration or bulk scanning operations. Developers often need to adjust validation settings to handle imperfect or legacy symbologies efficiently.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, checksumvalidation, batch-processing, barcode-generation, barcode-recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code 11 barcodes and reads them with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary barcode images, reads them with checksum validation off, and cleans up.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for batch processing
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample Code11 barcode texts
        string[] codeTexts = new string[] { "123456", "12345", "1234567" };
        List<string> barcodeFiles = new List<string>();

        // Generate barcode images
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(batchFolder, $"Code11_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, codeTexts[i]))
            {
                // Set X‑dimension to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch reading Code11 barcodes with ChecksumValidation set to Off:");

        // Read each barcode with checksum validation disabled
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code11))
                {
                    // Disable checksum verification for Code 11
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Cleanup (optional)
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}