// Title: Generate and Verify Codabar Barcode with Mod10 Checksum
// Description: Demonstrates how to generate a Codabar barcode with checksum enabled using the Mod10 algorithm, save it as an image, and then recognize it while validating the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to configure checksum settings on the BarcodeGenerator, use Codabar-specific parameters, and perform checksum validation with BarCodeReader. Developers working with one-dimensional symbologies often need to ensure data integrity by enabling and verifying checksums during both encoding and decoding phases.
// Prompt: Enable checksum calculation, choose Mod10 algorithm, and verify the checksum after generation.
// Tags: codabar, checksum, mod10, barcode generation, barcode recognition, aspose.barcode, one-dimensional, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codabar barcode with a Mod10 checksum,
/// saves it to a PNG file, and then reads it back while validating the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and verifies the checksum during recognition.
    /// </summary>
    static void Main()
    {
        // Initialize a Codabar barcode generator with sample code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "A12345B"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Select the Mod10 algorithm for Codabar checksum calculation.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Allow generation even if the code text is slightly incorrect.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Generate the barcode image and save it to a file (format inferred from extension).
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                generator.Save("codabar.png");
            }

            // Create a reader to recognize the saved barcode and validate its checksum.
            using (BarCodeReader reader = new BarCodeReader("codabar.png", DecodeType.Codabar))
            {
                // Turn on checksum validation during the recognition process.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all recognized barcodes (there should be one).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Recognized CodeText: " + result.CodeText);
                    // Output the checksum value if it is present in the extended OneD parameters.
                    Console.WriteLine("Checksum (if any): " + result.Extended.OneD.CheckSum);
                }
            }
        }
    }
}