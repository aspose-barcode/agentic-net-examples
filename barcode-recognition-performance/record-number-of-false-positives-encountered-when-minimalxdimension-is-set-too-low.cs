// Title: Minimal XDimension false positive detection example
// Description: Demonstrates how setting MinimalXDimension too low can cause false positives during barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of BarcodeGenerator, BarCodeReader, and QualitySettings to explore the impact of XDimension settings on decoding accuracy. Developers often need to fine‑tune module size and quality parameters to avoid misreads in high‑throughput scanning scenarios.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: barcode symbology, generation, recognition, minimalxdimension, false positives, code128, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of Code128 barcodes with an intentionally low XDimension,
/// then reads them back with MinimalXDimension set unrealistically low to
/// count any false positive detections.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes, reads them, and reports false positives.
    /// </summary>
    static void Main()
    {
        // Sample data and output configuration
        string codeText = "1234567890";
        int sampleCount = 5;
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Generate barcodes with a very low XDimension (module size)
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(outputFolder, $"barcode_{i}.png");

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension to a low value (0.5 point) to simulate a small module size
                generator.Parameters.Barcode.XDimension.Point = 0.5f;

                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        int falsePositiveCount = 0;

        // --------------------------------------------------------------------
        // Read the generated images with MinimalXDimension set too low
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(outputFolder, $"barcode_{i}.png");

            // Skip if the file was not created for any reason
            if (!File.Exists(filePath))
                continue;

            // Initialize a barcode reader for Code128
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Set MinimalXDimension to an unrealistically low value to provoke false positives
                reader.QualitySettings.MinimalXDimension = 0.1f;

                // Iterate over all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Count as false positive if the decoded text does not match the original
                    if (!string.Equals(result.CodeText, codeText, StringComparison.Ordinal))
                    {
                        falsePositiveCount++;
                    }
                }
            }
        }

        // Output the total number of false positives detected
        Console.WriteLine($"Number of false positives detected: {falsePositiveCount}");
    }
}