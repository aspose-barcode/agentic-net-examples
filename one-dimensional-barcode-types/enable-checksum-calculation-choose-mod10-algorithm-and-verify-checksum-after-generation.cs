// Title: Codabar Barcode Generation with Mod10 Checksum and Verification
// Description: Demonstrates how to generate a Codabar barcode with Mod10 checksum enabled, save it as PNG, and then read it back to verify the checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 1D barcodes, BarCodeReader for decoding, and checksum validation features. Typical scenarios include inventory labeling, point‑of‑sale systems, and any application that requires data integrity checks on Codabar symbols. Developers often need to enable checksums, select algorithms, and confirm correctness during round‑trip processing.
/// Prompt: Enable checksum calculation, choose Mod10 algorithm, and verify the checksum after generation.
// Tags: codabar, checksum, mod10, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Codabar barcode with a Mod10 checksum,
/// saves it to a PNG file, and then reads it back to verify the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image,
    /// reads the image back, and outputs the decoded text and checksum value.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare a temporary output directory and define the barcode file path.
        // -----------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "codabar.png");

        // -----------------------------------------------------------------
        // Generate a Codabar barcode with Mod10 checksum enabled.
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Choose Mod10 algorithm for Codabar.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Define start/stop symbols (both set to 'A' in this example).
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Save the generated barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Read the saved barcode image and verify the checksum.
        // -----------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Codabar))
        {
            // Turn on checksum validation during decoding.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // For 1D barcodes, the checksum value (if present) is available here.
                Console.WriteLine($"Checksum (if any): {result.Extended.OneD.CheckSum}");
            }
        }

        // -----------------------------------------------------------------
        // Optional clean‑up of temporary files.
        // -----------------------------------------------------------------
        // File.Delete(barcodePath);
        // Directory.Delete(outputDir);
    }
}