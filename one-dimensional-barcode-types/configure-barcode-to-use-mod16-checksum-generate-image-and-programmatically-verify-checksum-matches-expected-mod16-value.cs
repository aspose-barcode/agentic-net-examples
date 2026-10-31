// Title: Codabar barcode generation with Mod16 checksum and verification
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum algorithm, save it as a PNG image, and programmatically verify that the checksum matches the expected value.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the checksum validation features. Typical scenarios include ensuring data integrity for Codabar symbols in logistics, inventory, and healthcare applications where Mod16 checksum is required. Developers often need to generate barcodes with specific checksum modes and validate them during scanning.
// Prompt: Configure barcode to use Mod16 checksum, generate image, and programmatically verify checksum matches expected Mod16 value.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codabar barcode with a Mod16 checksum,
/// saves it as an image, and validates the checksum using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, computes the expected checksum,
    /// reads the barcode back, and verifies the checksum matches the expected character.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "CodabarMod16_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "codabar.png");

        // Base codetext includes start/stop symbols required by Codabar (A...A)
        string baseCodeText = "A12345A";

        // Create a barcode generator for Codabar and configure Mod16 checksum
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, baseCodeText))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Set the checksum algorithm to Mod16
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Compute the expected Mod16 checksum character manually
        int sum = 0;
        foreach (char ch in baseCodeText)
        {
            // Only numeric characters contribute to the checksum calculation
            if (char.IsDigit(ch))
                sum += ch - '0';
        }
        int mod16 = sum % 16;
        char expectedChecksumChar = mod16 < 10 ? (char)('0' + mod16) : (char)('A' + (mod16 - 10));

        // Read the barcode image and verify the checksum using the reader
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Codabar))
        {
            // Enable checksum validation during decoding
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string readCodeText = result.CodeText;
                // Verify that the read code ends with the expected checksum character
                bool checksumMatches = readCodeText.EndsWith(expectedChecksumChar.ToString(), StringComparison.Ordinal);
                Console.WriteLine($"Read CodeText: {readCodeText}");
                Console.WriteLine($"Expected checksum character: {expectedChecksumChar}");
                Console.WriteLine($"Checksum verification result: {(checksumMatches ? "PASS" : "FAIL")}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}