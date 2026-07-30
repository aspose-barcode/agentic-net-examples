// Title: Generate and validate a Code 128 barcode with checksum
// Description: Demonstrates creating a Code 128 barcode with checksum enabled, saving it as PNG, then decoding it while enforcing checksum validation to confirm correctness.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on checksum handling. Developers often need to ensure data integrity when generating Code 128 barcodes, making checksum validation a common requirement in inventory, shipping, and tracking systems.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, checksum, barcode generation, barcode recognition, aspose.barcode, png

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code 128 barcode with checksum enabled,
/// saves it as an image, and then decodes it while validating the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and validation.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string barcodeFile = "code128.png";

        // -------------------------------------------------
        // Generate a Code128 barcode with checksum enabled
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Optionally display the checksum digit in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image (PNG format by default) to the specified file.
            generator.Save(barcodeFile);
        }

        // -------------------------------------------------
        // Decode the barcode and validate the checksum
        // -------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
        {
            // Force checksum validation during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text from the barcode.
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // Retrieve the checksum calculated by the recognizer.
                string checksum = result.Extended.OneD.CheckSum;
                Console.WriteLine($"Checksum from barcode: {checksum}");

                // Report whether the checksum was successfully validated.
                if (!string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine("Checksum validation succeeded.");
                }
                else
                {
                    Console.WriteLine("Checksum not found or validation failed.");
                }
            }
        }
    }
}