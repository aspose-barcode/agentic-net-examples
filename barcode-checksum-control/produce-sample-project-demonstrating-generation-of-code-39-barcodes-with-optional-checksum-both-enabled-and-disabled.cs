// Title: Generate Code 39 Barcodes with Optional Checksum
// Description: This example shows how to create Code 39 barcodes using Aspose.BarCode, illustrating both checksum‑enabled and checksum‑disabled configurations.
// Category-Description: The sample belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It demonstrates the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG files, a common requirement for inventory, shipping, and retail applications where developers need to control checksum visibility.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: code39, checksum, barcode generation, png, aspose.barcode, barcodegenerator, encodetypes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code 39 barcodes with checksum enabled and disabled using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates output folder, generates two PNG barcodes, and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Text to encode in the Code 39 barcode
        string codeText = "CODE39";

        // ---------- Generate barcode with checksum enabled ----------
        string checksumEnabledPath = Path.Combine(outputDir, "Code39_ChecksumEnabled.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Turn on checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Show the checksum value in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode as a PNG image
            generator.Save(checksumEnabledPath, BarCodeImageFormat.Png);
        }

        // ---------- Generate barcode with checksum disabled ----------
        string checksumDisabledPath = Path.Combine(outputDir, "Code39_ChecksumDisabled.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Turn off checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            // Hide the checksum value from the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Save the barcode as a PNG image
            generator.Save(checksumDisabledPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated barcode images
        Console.WriteLine("Barcodes generated:");
        Console.WriteLine($" - Checksum enabled : {checksumEnabledPath}");
        Console.WriteLine($" - Checksum disabled: {checksumDisabledPath}");
    }
}