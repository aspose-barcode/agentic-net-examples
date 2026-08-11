// Title: Codabar Barcode Generation with Mod16 Checksum and Validation
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum mode, embed the checksum in the human‑readable text, and then validate the checksum during recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, configuring checksum settings via the Parameters.Barcode properties, and employing BarCodeReader to decode and verify checksums. Developers working with one‑dimensional symbologies such as Codabar often need to ensure data integrity by generating and validating checksums, making this pattern essential for inventory, shipping, and point‑of‑sale applications.
// Prompt: Configure barcode to use Mod16 checksum mode and validate the checksum after generation.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Codabar barcode with Mod16 checksum, saves it as an image,
/// and then reads the image back to validate the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, writes it to disk,
    /// and verifies the checksum during recognition.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "codabar.png");

        // ------------------------------------------------------------
        // Barcode generation
        // ------------------------------------------------------------
        // Create a Codabar barcode generator with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set the checksum mode to Mod16 (recommended AIIM for Codabar).
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Optionally display the checksum in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Barcode recognition and checksum validation
        // ------------------------------------------------------------
        // Initialize a reader for the saved image, specifying Codabar as the decode type.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Codabar))
        {
            // Enable checksum validation during the reading process.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the type of barcode detected.
                Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                // Output the full code text, including checksum if displayed.
                Console.WriteLine("Code Text (including checksum if shown): " + result.CodeText);
                // Output the extracted value without the checksum.
                Console.WriteLine("Extracted Value (without checksum): " + result.Extended.OneD.Value);
                // Output the extracted checksum value.
                Console.WriteLine("Extracted Checksum: " + result.Extended.OneD.CheckSum);
            }
        }
    }
}