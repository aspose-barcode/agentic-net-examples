// Title: Resetting BarCodeReader QualitySettings for Batch Processing
// Description: Demonstrates how to reset all QualitySettings of a BarCodeReader to their default values before reading each image in a batch of barcode files.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It shows how to use the BarCodeReader class together with QualitySettings to ensure consistent decoding performance across multiple images. Developers often need to adjust or reset quality presets such as XDimension, Deconvolution, and InverseImage when processing large batches of barcodes, making this pattern useful for batch automation scripts.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode, qualitysettings, batch processing, aspose.barcode, barcode generation, barcode recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a batch of barcode images, resets quality settings for each reader,
/// and reads the barcodes while outputting their details.
/// </summary>
class Program
{
    /// <summary>
    /// Resets all QualitySettings of the provided BarCodeReader to their default values.
    /// </summary>
    /// <param name="reader">The BarCodeReader whose quality settings will be reset.</param>
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Set the preset to the default NormalQuality.
        reader.QualitySettings = QualitySettings.NormalQuality;

        // Reset individual quality-related properties to their defaults.
        reader.QualitySettings.XDimension = XDimensionMode.Auto;
        reader.QualitySettings.Deconvolution = DeconvolutionMode.Normal;
        reader.QualitySettings.InverseImage = InverseImageMode.Auto;
    }

    /// <summary>
    /// Entry point of the program. Generates sample barcodes, processes them in a batch,
    /// and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the sample batch.
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate a few sample barcode images.
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(batchFolder, $"barcode_{i}.png");
            var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}");
            // Example setting for XDimension.
            generator.Parameters.Barcode.XDimension.Point = 2.5f;
            generator.Save(filePath, BarCodeImageFormat.Png);
            barcodeFiles.Add(filePath);
        }

        // Process each barcode image in the batch.
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Create a reader for the current image.
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Reset quality settings before processing this image.
                ResetQualitySettings(reader);

                // Read barcodes and output their details.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                    Console.WriteLine($"  Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files.
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}