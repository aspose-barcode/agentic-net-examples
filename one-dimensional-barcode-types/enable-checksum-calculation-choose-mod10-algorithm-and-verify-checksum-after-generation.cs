// Title: Codabar Barcode Generation with Mod10 Checksum and Validation
// Description: Demonstrates how to generate a Codabar barcode with a Mod10 checksum, save it as PNG, and then read it back while verifying the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to enable checksum calculation, select the Mod10 algorithm for Codabar, and use BarCodeReader with checksum validation to verify the result. Developers working with one‑dimensional symbologies often need to ensure data integrity by generating and validating checksums, making this pattern essential for inventory, shipping, and banking applications.
// Prompt: Enable checksum calculation, choose Mod10 algorithm, and verify the checksum after generation.
// Tags: codabar, checksum, mod10, barcode generation, barcode recognition, png, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Codabar barcode with a Mod10 checksum,
/// saves it as a PNG image, and then reads the image back to verify the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saving, and validation.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder for the generated barcode image
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "CodabarChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "codabar.png");

        // ------------------------------------------------------------
        // Generate a Codabar barcode with Mod10 checksum enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Choose Mod10 algorithm for Codabar checksum
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // ------------------------------------------------------------
        // Read the saved barcode image and verify the checksum
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Codabar;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Turn on checksum validation during reading
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Attempt to retrieve the checksum value from the extended result
                try
                {
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Checksum not available: {ex.Message}");
                }
            }
        }
    }
}