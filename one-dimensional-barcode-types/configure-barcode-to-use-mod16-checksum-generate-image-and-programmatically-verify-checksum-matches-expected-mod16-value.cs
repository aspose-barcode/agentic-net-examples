// Title: Codabar barcode generation with Mod16 checksum and verification
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum algorithm, save it as an image, and programmatically verify the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to configure checksum settings for Codabar, generate a PNG image, and use BarCodeReader to validate the checksum. Developers working with barcode symbologies, checksum algorithms, and image output can use these APIs (BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType) for similar tasks.
// Prompt: Configure barcode to use Mod16 checksum, generate image, and programmatically verify checksum matches expected Mod16 value.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Codabar barcode with a Mod16 checksum, saves it as a PNG image,
/// and verifies the checksum using the Aspose.BarCode recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, image saving,
    /// and checksum verification.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "CodabarMod16.png");

        // --------------------------------------------------------------------
        // Create a Codabar barcode generator with the data "12345".
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Enable checksum calculation and specify the Mod16 algorithm.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Set start and stop symbols to 'A' (required for Codabar).
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // --------------------------------------------------------------------
        // Read the saved barcode image and validate the checksum.
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Turn on checksum validation during decoding.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // Remove the leading and trailing start/stop symbols.
                if (result.CodeText.Length >= 2)
                {
                    string inner = result.CodeText.Substring(1, result.CodeText.Length - 2);

                    // The last character before the stop symbol is the checksum digit.
                    if (inner.Length > 0)
                    {
                        string dataPart = inner.Substring(0, inner.Length - 1);
                        char checksumChar = inner[inner.Length - 1];

                        // Compute Mod16 checksum from numeric characters in the data part.
                        int sum = 0;
                        foreach (char c in dataPart)
                        {
                            if (char.IsDigit(c))
                                sum += c - '0';
                        }
                        int computedChecksum = sum % 16;

                        // Convert the numeric checksum to its character representation (0‑9, A‑F).
                        char expectedChar = computedChecksum < 10
                            ? (char)('0' + computedChecksum)
                            : (char)('A' + (computedChecksum - 10));

                        // Compare the extracted checksum character with the computed one.
                        bool match = char.ToUpperInvariant(checksumChar) == char.ToUpperInvariant(expectedChar);
                        Console.WriteLine($"Extracted checksum character: {checksumChar}");
                        Console.WriteLine($"Computed Mod16 checksum: {computedChecksum} (expected char: {expectedChar})");
                        Console.WriteLine($"Checksum match: {match}");
                    }
                }
            }
        }
    }
}