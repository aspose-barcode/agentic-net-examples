// Title: Generate Code 39 Barcodes with Optional Checksum
// Description: Demonstrates creating Code 39 barcodes using Aspose.BarCode and saving them as PNG images, showing both checksum disabled and enabled scenarios.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code39FullASCII. Developers commonly need to generate barcodes for inventory, shipping, or tracking, and may require toggling checksum validation. The snippet shows setting the IsChecksumEnabled property, saving images, and handling output directories—typical steps in barcode creation workflows.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: code39, barcode, generation, checksum, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates Code 39 barcodes with and without checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, generates two barcodes (checksum disabled and enabled), and saves them as PNG files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working folder.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Text to encode in the barcode.
        string codeText = "CODE39";

        // ------------------------------------------------------------
        // Generate barcode without checksum.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Build the full file path for the image.
            string filePath = Path.Combine(outputDir, "Code39_NoChecksum.png");

            // Save the barcode image as PNG.
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode without checksum: {filePath}");
        }

        // ------------------------------------------------------------
        // Generate barcode with checksum.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Build the full file path for the image.
            string filePath = Path.Combine(outputDir, "Code39_WithChecksum.png");

            // Save the barcode image as PNG.
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with checksum: {filePath}");
        }
    }
}