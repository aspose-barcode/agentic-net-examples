// Title: Generate Code 39 Barcodes with Optional Checksum
// Description: This example shows how to create Code 39 barcodes using Aspose.BarCode, saving PNG files with the checksum disabled and enabled.
// Category-Description: Demonstrates barcode generation with Aspose.BarCode, focusing on the Code 39 symbology. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce images. Developers often need to toggle checksum for validation purposes, and this sample illustrates the typical pattern for enabling or disabling it in .NET applications.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: code39, barcode, generation, checksum, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates Code 39 barcodes with and without checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates two PNG images: one without checksum and one with checksum.
    /// </summary>
    static void Main()
    {
        // Determine the output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Text to encode in the barcode
        string codeText = "CODE39";

        // Generate Code 39 barcode without checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            string path = Path.Combine(outputDir, "Code39_NoChecksum.png");
            generator.Save(path, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode without checksum: {path}");
        }

        // Generate Code 39 barcode with checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            string path = Path.Combine(outputDir, "Code39_WithChecksum.png");
            generator.Save(path, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with checksum: {path}");
        }
    }
}