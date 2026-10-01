// Title: Read Mixed Symbology Barcodes with Default Checksum Validation
// Description: Generates Code128 and Code39 barcode images, saves them as PNG files, and reads them using Aspose.BarCode while enforcing the default checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes of different symbologies, and BarCodeReader with ChecksumValidation.Default to decode them. Developers commonly need to generate barcodes for labeling and then validate them during scanning, handling checksum rules automatically.
// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
// Tags: barcode, symbology, generation, recognition, checksumvalidation, mixed-symbology, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating mixed‑symbology barcodes and reading them with default checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, reads them, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "MixedSymbology_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (mixed symbologies)
        var barcodes = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "1234567890", "code128.png"),
            (EncodeTypes.Code39, "CODE39*CHECK", "code39.png")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images
        foreach (var (encodeType, text, fileName) in barcodes)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Read each barcode image with default checksum validation
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Enforce default checksum handling
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Perform reading
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  Code Text : {result.CodeText}");
                        Console.WriteLine($"  Symbology : {result.CodeTypeName}");
                    }
                }
            }
        }

        // Cleanup temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}