// Title: Generate Codabar barcode with Mod16 checksum and verify it
// Description: Demonstrates how to create a Codabar barcode using the Mod16 checksum, save it as an image, and read it back to confirm the checksum matches the expected value.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for encoding, setting checksum options via Parameters.Barcode, and BarCodeReader for decoding with checksum validation. Developers working with one‑dimensional symbologies often need to enable and verify checksums to ensure data integrity in scanning applications.
// Prompt: Configure barcode to use Mod16 checksum, generate image, and programmatically verify checksum matches expected Mod16 value.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Codabar barcode with a Mod16 checksum,
/// saves it as a PNG image, and then reads the image back to verify the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and verification.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare output directory and file path for the generated image
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeMod16Demo");
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "codabar_mod16.png");

        // ------------------------------------------------------------
        // Generate Codabar barcode with Mod16 checksum (default)
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Specify Mod16 checksum mode for Codabar
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {imagePath}");
            Console.WriteLine($"Generated CodeText (includes checksum): {generator.CodeText}");
        }

        // ------------------------------------------------------------
        // Read the barcode image and verify the checksum
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Codabar))
        {
            // Turn on checksum validation during decoding
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum from result: {result.Extended.OneD.CheckSum}");

                if (!string.IsNullOrEmpty(result.CodeText))
                {
                    // The last character of the CodeText should be the checksum character
                    char checksumChar = result.CodeText[result.CodeText.Length - 1];
                    bool match = checksumChar.ToString() == result.Extended.OneD.CheckSum.ToString();
                    Console.WriteLine($"Checksum verification: {(match ? "PASS" : "FAIL")}");
                }
            }
        }
    }
}