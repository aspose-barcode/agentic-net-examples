// Title: MaxiCode ECI Encoding with Special Characters Verification
// Description: Generates a MaxiCode barcode using ECI UTF-8 encoding for text containing special characters, then reads it back to confirm the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on MaxiCode symbology with Extended Channel Interpretation (ECI) support. It demonstrates using BarcodeGenerator to create a MaxiCode, configuring ECIEncoding, and employing BarCodeReader to decode the image. Developers working with international text, special characters, or needing reliable round‑trip validation of MaxiCode barcodes will find this pattern useful.
// Prompt: Create unit test verifying correct handling of special characters when using ECI encoding in MaxiCode.
// Tags: barcode, generation, recognition, maxicode, eci, utf8, special-characters, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating and verifying a MaxiCode barcode with ECI encoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a MaxiCode barcode with UTF-8 ECI encoding, reads it back, and validates the decoded text.
    /// </summary>
    static void Main()
    {
        // Text containing special (non‑ASCII) characters to encode.
        const string originalText = "犬Right狗";

        // Initialize a MaxiCode generator with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Configure the generator to use ECI mode and UTF‑8 encoding.
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.ECI;
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize a reader to decode the MaxiCode from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    // Attempt to read barcodes from the image.
                    var results = reader.ReadBarCodes();

                    // Verify that at least one barcode was detected.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("FAILED: No barcode detected.");
                        return;
                    }

                    // Compare the decoded text with the original input.
                    var decodedText = results[0].CodeText;
                    if (decodedText == originalText)
                    {
                        Console.WriteLine("PASSED: Decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Decoded text does not match. Expected '{originalText}', got '{decodedText}'.");
                    }
                }
            }
        }
    }
}