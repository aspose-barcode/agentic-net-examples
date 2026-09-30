// Title: Generate and Validate Code 128 Barcode with Checksum
// Description: This example creates a Code 128 barcode with checksum enabled, saves it as a PNG, then reads it back to confirm the checksum passes.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for Code 128 symbology. It showcases the use of BarcodeGenerator for creating barcodes with checksum support and BarCodeReader for decoding and validating checksum integrity. Ideal for developers needing reliable barcode creation and verification in .NET applications.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, checksum, barcode generation, barcode recognition, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Code 128 barcode with checksum enabled,
/// save it as an image, and then decode it to verify checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, reads, and validates a Code 128 barcode.
    /// </summary>
    static void Main()
    {
        // Original data to encode
        string codeText = "ABC123456";

        // Temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "code128.png");

        // -------------------- Barcode Generation --------------------
        // Create a generator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum (required for Code128)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Show checksum in the human‑readable text (optional)
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image as PNG to the temporary file
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // -------------------- Barcode Decoding & Validation --------------------
        // Initialize a reader for Code128 from the saved image
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            // Ensure checksum validation is active (default is On)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all detected barcodes
            var results = reader.ReadBarCodes();

            if (results != null && results.Length > 0)
            {
                var result = results[0];
                Console.WriteLine($"Original CodeText : {codeText}");
                Console.WriteLine($"Decoded CodeText  : {result.CodeText}");
                Console.WriteLine($"Detected Type     : {result.CodeTypeName}");
                // Presence of a result indicates checksum passed
                Console.WriteLine("Checksum validation: Passed");
            }
            else
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // -------------------- Cleanup --------------------
        // Delete the temporary image file
        try
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}