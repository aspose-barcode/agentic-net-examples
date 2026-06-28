using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with UTF‑8 ECI encoding,
/// saving it to a memory stream, and then recognizing it to verify
/// that the decoded text matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it back, and validates the result.
    /// </summary>
    static void Main()
    {
        // Original text containing special Unicode characters (Japanese kanji and English)
        string originalText = "犬Right狗";

        // Create a barcode generator for MaxiCode with the original text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Configure the generator to use UTF‑8 ECI encoding and ECI mode
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.ECI;

            // Save the generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode MaxiCode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    // Read all barcodes found in the stream
                    var results = reader.ReadBarCodes();
                    bool success = false;

                    // Iterate through each decoded result
                    foreach (var result in results)
                    {
                        // Compare the decoded text with the original input
                        if (result.CodeText == originalText)
                        {
                            success = true;
                            Console.WriteLine("Test Passed: Decoded text matches original.");
                        }
                        else
                        {
                            Console.WriteLine($"Test Failed: Decoded text '{result.CodeText}' does not match original '{originalText}'.");
                        }
                    }

                    // If no barcodes were detected, report failure
                    if (!success && results.Length == 0)
                    {
                        Console.WriteLine("Test Failed: No barcode detected.");
                    }
                }
            }
        }
    }
}