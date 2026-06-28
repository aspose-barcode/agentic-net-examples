using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Han Xin barcode with UTF‑8 ECI encoding,
/// then reading and verifying the encoded text from the generated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, decodes it, and validates the result.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text containing characters from different scripts
        string originalText = "Hello, 世界! Привет! مرحبا!";

        // Create a barcode generator for Han Xin symbology with the original text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, originalText))
        {
            // Configure Han Xin specific parameters:
            // - Use ECI (Extended Channel Interpretation) mode
            // - Set the ECI encoding to UTF‑8 to support Unicode characters
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Initialize a barcode reader to decode Han Xin barcodes from the stream
                using (var reader = new BarCodeReader(ms, DecodeType.HanXin))
                {
                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, inform the user and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    // Assuming only one barcode was generated, take the first result
                    var result = results[0];
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                    // Verify that the decoded text matches the original input
                    if (result.CodeText == originalText)
                    {
                        Console.WriteLine("Success: Decoded text matches the original.");
                    }
                    else
                    {
                        Console.WriteLine("Failure: Decoded text does not match the original.");
                    }
                }
            }
        }
    }
}