// Title: MaxiCode barcode generation and verification with ECI UTF-8 encoding
// Description: Demonstrates generating a MaxiCode barcode containing special Unicode characters using ECI UTF-8 encoding, then reads it back to verify the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator and BarCodeReader classes for creating and decoding MaxiCode symbols, a common requirement in logistics and shipping applications. Developers often need to ensure proper handling of special characters via ECI encodings such as UTF-8 when working with international data.
// Prompt: Create unit test verifying correct handling of special characters when using ECI encoding in MaxiCode.
// Tags: barcode symbology, eci encoding, maxicode, generation, recognition, unit-test, utf-8, special-characters, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a MaxiCode barcode with special Unicode characters using ECI UTF-8 encoding,
/// then reads the barcode back to verify that the decoded text matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, verification, and cleanup.
    /// </summary>
    static void Main()
    {
        // Sample text containing special Unicode characters (Japanese kanji for "dog" and "right")
        string originalText = "犬Right狗";

        // Create a temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), $"MaxiCode_{Guid.NewGuid():N}.png");

        // Generate a MaxiCode barcode with ECI UTF-8 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Set the ECI encoding to UTF-8 to support special characters
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;
            // Save the barcode image to the temporary file
            generator.Save(tempFile);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and compare the decoded text with the original
        bool success = false;
        using (var reader = new BarCodeReader(tempFile, DecodeType.MaxiCode))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                if (result.CodeText == originalText)
                {
                    success = true;
                    Console.WriteLine("Success: Decoded text matches original.");
                }
                else
                {
                    Console.WriteLine($"Failure: Decoded text '{result.CodeText}' does not match original '{originalText}'.");
                }
            }
        }

        // Clean up the temporary barcode image file
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome
        }

        // Report overall test result if no matching barcode was found
        if (!success)
        {
            Console.WriteLine("Test completed with mismatched results.");
        }
    }
}