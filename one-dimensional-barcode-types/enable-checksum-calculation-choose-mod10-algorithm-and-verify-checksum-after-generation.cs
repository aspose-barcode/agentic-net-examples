// Title: Enable Mod10 checksum for Codabar barcode and verify it
// Description: Demonstrates generating a Codabar barcode with checksum enabled using the Mod10 algorithm, then reads the barcode to confirm the checksum is present.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to configure checksum settings for one‑dimensional symbologies. It uses BarcodeGenerator for creating barcodes and BarCodeReader for decoding, common tasks when developers need data integrity verification in inventory, shipping, or point‑of‑sale systems.
// Prompt: Enable checksum calculation, choose Mod10 algorithm, and verify the checksum after generation.
// Tags: codabar, checksum, mod10, generation, recognition, aspose.barcode, one-dimensional

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates enabling Mod10 checksum for a Codabar barcode, saving it, and verifying the checksum during recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Codabar barcode with checksum, saves it, and validates the checksum on read.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "codabar.png";

        // Sample Codabar text including start/stop symbols (A...A)
        string codeText = "A123456A";

        // Create a barcode generator for Codabar with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set the checksum algorithm to Mod10 for Codabar
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Initialize a barcode reader to decode the saved image using Codabar symbology
        using (var reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Turn on checksum validation during the recognition process
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all recognized barcodes (should be only one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the recognized text and the extracted checksum value
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                Console.WriteLine($"Extracted Checksum: {result.Extended.OneD.CheckSum}");

                // Simple verification: ensure a checksum value was returned
                if (!string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                {
                    Console.WriteLine("Checksum verification succeeded.");
                }
                else
                {
                    Console.WriteLine("Checksum verification failed.");
                }
            }
        }
    }
}