// Title: Codabar barcode generation with Mod16 checksum and validation
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum mode, save it as PNG, and then read it back while validating the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, setting checksum options via Parameters.Barcode, and BarCodeReader for decoding and checksum validation. Developers working with Codabar symbology often need to ensure data integrity by enabling and verifying checksums, making this pattern essential for inventory, shipping, and tracking applications.
// Prompt: Configure barcode to use Mod16 checksum mode and validate the checksum after generation.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Codabar barcode with Mod16 checksum and validating it after reading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, then reads and validates the checksum.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "CodabarMod16.png");

        // Generate a Codabar barcode with Mod16 checksum enabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Set the specific Mod16 checksum mode for Codabar.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;
            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Read the saved barcode image and validate its checksum.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Turn on checksum validation during decoding.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
            bool anyResult = false;

            // Iterate through all detected barcodes (should be one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"Read CodeText: {result.CodeText}");
                Console.WriteLine("Checksum validation passed.");
            }

            // If no barcode was detected or validation failed, inform the user.
            if (!anyResult)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }
    }
}