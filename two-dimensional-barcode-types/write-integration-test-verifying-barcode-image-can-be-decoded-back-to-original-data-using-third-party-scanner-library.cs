// Title: Integration test for barcode generation and verification
// Description: Generates a Code128 barcode, decodes it, and verifies the decoded data matches the original input.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a barcode image and BarCodeReader to decode it. Developers commonly need to validate that generated barcodes can be read by third‑party scanners, ensuring interoperability in inventory, logistics, and retail applications.
// Prompt: Write integration test verifying barcode image can be decoded back to original data using third‑party scanner library.
// Tags: code128, generation, recognition, png, aspose.barcode, integration-test, barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates an integration test that generates a barcode, decodes it, and validates the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test. Generates a Code128 barcode, reads it back, and checks the decoded text.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string originalText = "Test12345";

        // Create a barcode generator for Code128 with the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalText))
        {
            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader that supports all available symbologies
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();

                    // If no barcode is detected, report and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Take the first detected barcode (the one we generated)
                    var decodedText = results[0].CodeText;

                    // Compare the decoded text with the original input
                    if (decodedText == originalText)
                    {
                        Console.WriteLine("Success: Decoded text matches original.");
                    }
                    else
                    {
                        Console.WriteLine($"Failure: Decoded text '{decodedText}' does not match original '{originalText}'.");
                    }
                }
            }
        }
    }
}