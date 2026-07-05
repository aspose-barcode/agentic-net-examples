// Title: Code 128 barcode generation with checksum validation
// Description: Demonstrates creating a Code 128 barcode with checksum enabled, saving it as an image, then decoding it while verifying the checksum.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, barcode, generation, decoding, checksum, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code 128 barcode with checksum enabled and decoding it to validate the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image, saves it, and then reads it back while validating the checksum.
    /// </summary>
    static void Main()
    {
        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

        // Create a Code128 barcode generator with the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Optionally display the checksum in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader to decode the saved image, specifying Code128 as the decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enforce checksum validation during the recognition process
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text from the barcode
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                // For 1D barcodes, the checksum value is available via the extended parameters
                Console.WriteLine($"Checksum (from barcode): {result.Extended.OneD.CheckSum}");
            }
        }
    }
}