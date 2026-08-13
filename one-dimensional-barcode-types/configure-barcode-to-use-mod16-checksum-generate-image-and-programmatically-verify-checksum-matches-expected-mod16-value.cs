// Title: Codabar Barcode Generation with Mod16 Checksum and Verification
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum algorithm, save it as an image, and programmatically verify the checksum during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator for creating barcodes with specific checksum settings and BarCodeReader for decoding and validating them. Key API classes include BarcodeGenerator, BarCodeReader, and related parameter objects. Typical scenarios involve ensuring data integrity in logistics, inventory, and point‑of‑sale systems where checksum validation is required. Developers often need to configure checksum modes, render barcode images, and confirm checksum correctness programmatically.
// Prompt: Configure barcode to use Mod16 checksum, generate image, and programmatically verify checksum matches expected Mod16 value.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, image output, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Codabar barcode with Mod16 checksum, saves it as a PNG image,
/// then reads the image back to verify that the checksum matches the expected value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and checksum verification.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "codabar.png";

        // Create a Codabar barcode generator with a sample code.
        // Start/stop characters (A) are required for Codabar.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum generation and select the Mod16 algorithm.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Optional: display the checksum digit in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image directly to a file (PNG format).
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode image and verify the checksum.
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Ensure checksum validation is performed during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all recognized barcode results (should be one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // The full CodeText includes the checksum digit added by the generator.
                string fullCodeText = result.CodeText;

                // Extract the checksum digit from the recognized CodeText (last character).
                string extractedChecksum = fullCodeText.Substring(fullCodeText.Length - 1);

                // Get the checksum reported by the recognition engine.
                string reportedChecksum = result.Extended.OneD.CheckSum;

                // Verify that both checksum values match.
                bool isChecksumMatch = string.Equals(extractedChecksum, reportedChecksum, StringComparison.Ordinal);

                Console.WriteLine($"Full CodeText: {fullCodeText}");
                Console.WriteLine($"Extracted Checksum (last char): {extractedChecksum}");
                Console.WriteLine($"Reported Checksum (Extended.OneD): {reportedChecksum}");
                Console.WriteLine($"Checksum verification result: {(isChecksumMatch ? "PASS" : "FAIL")}");
            }
        }

        // Program ends normally.
    }
}