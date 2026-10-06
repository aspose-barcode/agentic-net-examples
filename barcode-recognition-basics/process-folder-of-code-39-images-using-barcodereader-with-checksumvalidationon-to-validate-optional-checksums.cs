// Title: Process Code 39 Images with Checksum Validation Using BarCodeReader
// Description: Demonstrates how to generate Code 39 barcode images with optional checksum enabled, then read them from a folder using BarCodeReader with ChecksumValidation.On to verify the checksums.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting checksum validation—a common requirement when working with Code 39 symbology in inventory, shipping, and tracking systems. Developers often need to batch‑process images, validate optional checksums, and extract barcode data efficiently.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, checksum, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates temporary Code 39 barcode images with checksum enabled,
/// then reads each image using BarCodeReader with checksum validation turned on.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, validates them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a dedicated temporary folder for the sample barcode images.
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Path.GetTempPath(), "Code39Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folderPath);

        // ---------------------------------------------------------------
        // 2. Generate sample Code 39 barcode images with checksum enabled.
        // ---------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string codeText = $"CODE{i}";
            string filePath = Path.Combine(folderPath, $"Code39_{i}.png");

            // Use BarcodeGenerator to create a PNG image for each code.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // ---------------------------------------------------------------
        // 3. Process each barcode image with checksum validation turned on.
        // ---------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize BarCodeReader for Code 39 decoding.
                using (var reader = new BarCodeReader(file, DecodeType.Code39))
                {
                    // Enable checksum validation for the reader.
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Read all barcodes found in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                        continue;
                    }

                    // Output details for each detected barcode.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  CheckSum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error loading image '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // ---------------------------------------------------------------
        // 4. Cleanup: delete the temporary folder and its contents.
        // ---------------------------------------------------------------
        try
        {
            Directory.Delete(folderPath, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files in use).
        }
    }
}