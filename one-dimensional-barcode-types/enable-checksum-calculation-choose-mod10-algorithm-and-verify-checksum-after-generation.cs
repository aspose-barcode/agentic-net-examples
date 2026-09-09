// Title: Codabar Barcode Generation with Mod10 Checksum and Validation
// Description: Demonstrates how to generate a Codabar barcode with Mod10 checksum enabled, save it as PNG, and then read it back to verify the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator, BarCodeReader, and related parameter classes to configure checksum calculation, generate an image, and validate the checksum during decoding. Developers working with one‑dimensional symbologies often need to enable and verify checksums to ensure data integrity.
// Prompt: Enable checksum calculation, choose Mod10 algorithm, and verify the checksum after generation.
// Tags: codabar, checksum, mod10, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codabar barcode with a Mod10 checksum,
/// saves it to a temporary PNG file, reads it back, and validates the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saving, reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "CodabarMod10.png");

        // Generate a Codabar barcode with Mod10 checksum enabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Choose Mod10 algorithm for Codabar checksum.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Read the saved barcode image and verify the checksum.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Codabar))
        {
            // Turn on checksum validation during decoding.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Output checksum information if available.
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum Value: {result.Extended.OneD.CheckSum}");
                }
                else
                {
                    Console.WriteLine("Checksum information not available.");
                }
            }
        }

        // Clean up temporary files (optional).
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}