// Title: Hide Checksum Digit in Code39 Barcode
// Description: Demonstrates configuring BarcodeParameters to hide the checksum digit for a Code 39 barcode and verifying that the decoded data excludes it.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator and BarCodeReader, manipulate checksum settings, and validate output. Developers working with barcode symbologies often need to control checksum visibility for compliance or aesthetic reasons.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, hide checksum, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code 39 barcode without a checksum,
/// saves it to an image file, and then reads the image back to verify the checksum is omitted.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the result.
    /// </summary>
    static void Main()
    {
        // Input data that does NOT include a checksum digit.
        string data = "ABC123";

        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code39.png");

        // Generate a Code 39 barcode with checksum generation disabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, data))
        {
            // Turn off checksum generation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Keep the human‑readable text below the barcode (still without checksum).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Ensure the barcode image was created before attempting to read it.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image and decode it, disabling checksum validation.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code39))
        {
            // Turn off checksum validation so the reader accepts barcodes without a checksum.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all detected barcodes (there should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read CodeText: {result.CodeText}");

                // Verify that the decoded text matches the original data (i.e., no checksum digit was added).
                if (result.CodeText == data)
                {
                    Console.WriteLine("Verification succeeded: checksum digit is not present.");
                }
                else
                {
                    Console.WriteLine("Verification failed: unexpected CodeText.");
                }
            }
        }
    }
}