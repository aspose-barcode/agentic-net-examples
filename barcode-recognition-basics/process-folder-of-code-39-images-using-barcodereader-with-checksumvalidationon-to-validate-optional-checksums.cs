// Title: Validate Code 39 barcodes in a folder using BarCodeReader with checksum validation
// Description: Demonstrates how to generate a set of Code 39 barcode images, some with checksums, and then read them back using Aspose.BarCode's BarCodeReader with ChecksumValidation.On to verify optional checksums.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on checksum validation for one‑dimensional symbologies. It showcases the use of BarCodeGenerator for image creation, BarCodeReader for decoding, and the ChecksumValidation setting to enforce checksum checks. Developers working with inventory, shipping, or any system that relies on Code 39 often need to validate optional checksums to ensure data integrity.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, checksumvalidation, barcode, barcodegeneration, barcoderecognition, aspnet, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code 39 barcodes (with and without checksum) and validating them using BarCodeReader with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them back with checksum validation, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        List<string> imageFiles = new List<string>();

        // Generate sample Code 39 barcodes (some with checksum, one without)
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"CODE{i}";
            string filePath = Path.Combine(tempFolder, $"Code39_{i}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Set barcode appearance and enable checksum generation
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // Add a barcode without checksum to demonstrate validation failure
        string noChecksumPath = Path.Combine(tempFolder, "Code39_NoChecksum.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "NOCHECK"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            generator.Save(noChecksumPath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(noChecksumPath);

        Console.WriteLine("Processing Code 39 images with ChecksumValidation.On");
        Console.WriteLine($"Folder: {tempFolder}");
        Console.WriteLine();

        // Iterate over each generated image and attempt to read it with checksum validation enabled
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39))
                {
                    // Enforce checksum validation for optional Code 39 checksums
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No valid barcode detected in: {Path.GetFileName(imagePath)}");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                            Console.WriteLine($"  Symbology : {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText  : {result.CodeText}");
                            Console.WriteLine($"  Value     : {result.Extended.OneD.Value}");
                            Console.WriteLine($"  CheckSum  : {result.Extended.OneD.CheckSum}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{Path.GetFileName(imagePath)}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(imagePath)}': {ex.Message}");
            }

            Console.WriteLine();
        }

        // Cleanup: optionally delete the temporary folder
        // Directory.Delete(tempFolder, true);
    }
}