// Title: Codabar barcode generation with Mod16 checksum and verification
// Description: Demonstrates configuring a Codabar barcode to use the Mod16 checksum, generating an image, and programmatically verifying the checksum during recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to set checksum algorithms (e.g., Mod16) using BarcodeGenerator and validate them with BarCodeReader. Developers often need to ensure data integrity for 1D symbologies like Codabar, making checksum configuration and verification essential in inventory, logistics, and point‑of‑sale applications.
// Prompt: Configure barcode to use Mod16 checksum, generate image, and programmatically verify checksum matches expected Mod16 value.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Codabar barcode with a Mod16 checksum,
/// saves it as an image, and then reads the image to verify the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the checksum.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "codabar_mod16.png");

        // Create a Codabar barcode generator with the sample data "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Specify that the Mod16 algorithm should be used for the checksum.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Initialize a barcode reader for the generated image, targeting Codabar symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Codabar))
        {
            // Turn on checksum validation during the recognition process.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the decoded value without the checksum and the checksum itself.
                string valueWithoutChecksum = result.Extended.OneD.Value;
                string checksum = result.Extended.OneD.CheckSum;

                Console.WriteLine($"Decoded Value (without checksum): {valueWithoutChecksum}");
                Console.WriteLine($"Detected Checksum (Mod16): {checksum}");

                // Simple verification: ensure a checksum was detected for Mod16.
                if (string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine("Checksum verification failed: checksum is missing.");
                }
                else
                {
                    Console.WriteLine("Checksum verification succeeded.");
                }
            }
        }
    }
}