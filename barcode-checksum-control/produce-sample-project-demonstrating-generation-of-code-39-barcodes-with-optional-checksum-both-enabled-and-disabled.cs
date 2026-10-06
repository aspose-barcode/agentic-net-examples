// Title: Generate Code 39 barcodes with optional checksum
// Description: Demonstrates how to create Code 39 barcodes using Aspose.BarCode, showing both checksum disabled and enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and checksum settings. Developers often need to generate Code 39 barcodes for inventory or tracking systems, and may require toggling the checksum for validation purposes. The snippet shows typical steps: initializing the generator, configuring checksum, and saving to PNG.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: code39, barcode, checksum, generation, png, aspose.barcode, csharp

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
    /// Entry point that creates output directory, generates two barcode images (checksum disabled/enabled), and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Define and create the output folder for generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Paths for the two barcode images
        string noChecksumPath = Path.Combine(outputDir, "Code39_NoChecksum.png");
        string checksumPath = Path.Combine(outputDir, "Code39_WithChecksum.png");

        // Generate barcode without checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            generator.Save(noChecksumPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(checksumPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine($"Generated barcode without checksum: {noChecksumPath}");
        Console.WriteLine($"Generated barcode with checksum: {checksumPath}");
    }
}