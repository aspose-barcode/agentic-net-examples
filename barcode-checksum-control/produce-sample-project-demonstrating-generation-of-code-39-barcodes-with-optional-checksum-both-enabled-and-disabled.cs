// Title: Code 39 Barcode Generation with Optional Checksum
// Description: Demonstrates creating Code 39 barcodes using Aspose.BarCode with checksum enabled and disabled.
// Prompt: Produce a sample project demonstrating generation of Code 39 barcodes with optional checksum both enabled and disabled.
// Tags: barcode, code39, checksum, image, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application that generates Code 39 barcodes
/// with the checksum feature turned on and off.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images: one with checksum enabled and one with checksum disabled.
    /// </summary>
    static void Main()
    {
        // Text to be encoded into the barcode
        const string codeText = "CODE39";

        // ------------------------------------------------------------
        // Generate Code 39 barcode with checksum enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image to a PNG file
            generator.Save("code39_checksum_enabled.png");
        }

        // ------------------------------------------------------------
        // Generate Code 39 barcode with checksum disabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Save the generated barcode image to a PNG file
            generator.Save("code39_checksum_disabled.png");
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine("Barcodes generated successfully.");
    }
}