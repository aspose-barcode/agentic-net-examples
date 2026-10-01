// Title: Process Code 39 images with checksum validation using BarCodeReader
// Description: Demonstrates generating Code 39 barcodes (with and without optional checksum), saving them as PNG, and reading them back with BarCodeReader while enabling checksum validation.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and generation category. It showcases the use of BarcodeGenerator for creating Code 39 symbols and BarCodeReader with ChecksumValidation.On to verify optional checksums. Developers often need to batch‑process images, validate checksums, and handle decoding results, making this pattern common in inventory and logistics applications.
// Prompt: Process a folder of Code 39 images using BarCodeReader with ChecksumValidation.On to validate optional checksums.
// Tags: code39, checksum, barcode, generation, reading, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code 39 barcodes (with optional checksum) and reading them back with checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them with checksum validation, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare sample Code39 texts (one with checksum enabled, one without)
        var samples = new List<(string Text, bool EnableChecksum)>
        {
            ("CODE39", false),
            ("CHECKSUM", true)
        };

        // List to hold the generated file paths
        var imageFiles = new List<string>();

        // Generate barcode images
        foreach (var (text, enableChecksum) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                // Enable optional checksum for the second sample
                if (enableChecksum)
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                }

                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Process each image with BarCodeReader
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code39))
                {
                    // Validate optional checksums
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No valid Code39 barcode detected in file: {Path.GetFileName(file)}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Symbology: {result.CodeTypeName}");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported or corrupted file: {Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}