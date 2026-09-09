// Title: Reset QualitySettings and Process a Batch of QR Barcodes
// Description: Demonstrates resetting the BarCodeReader quality settings to defaults before reading a batch of generated QR code images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to reset QualitySettings (using the NormalQuality preset) between reads. Developers working with image batches often need to ensure consistent decoding performance, making this pattern common for batch processing scenarios.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode, qr, qualitysettings, batch processing, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates resetting QualitySettings and reading a batch of QR barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Resets the reader's quality settings to the default NormalQuality preset.
    /// </summary>
    /// <param name="reader">The BarCodeReader instance whose settings are to be reset.</param>
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Apply the default NormalQuality preset
        reader.QualitySettings = QualitySettings.NormalQuality;
    }

    /// <summary>
    /// Entry point. Generates sample QR barcodes, resets reader quality settings, reads them, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            BaseEncodeType encodeType = EncodeTypes.QR;
            string codeText = $"Sample{i}";
            BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText);
            string filePath = Path.Combine(batchFolder, $"barcode{i}.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }

        // Process the batch: read each barcode with reset quality settings
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.QR))
            {
                // Ensure the reader uses default quality before each read
                ResetQualitySettings(reader);
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText}");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        // Clean up temporary files (optional)
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