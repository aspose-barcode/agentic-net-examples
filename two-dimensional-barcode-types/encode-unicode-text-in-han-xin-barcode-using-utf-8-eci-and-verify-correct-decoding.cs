// Title: Encode Unicode text in Han Xin barcode with UTF‑8 ECI
// Description: Demonstrates generating a Han Xin barcode that encodes Unicode characters using UTF‑8 ECI and then decoding it to verify the text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.HanXin, configure HanXinEncodeMode to ECI, set ECIEncoding to UTF8, and then read the barcode using BarCodeReader. Developers working with multi‑language or special‑character data often need to embed Unicode text in barcodes and ensure correct round‑trip decoding.
// Prompt: Encode Unicode text in Han Xin barcode using UTF‑8 ECI and verify correct decoding.
// Tags: hanxin, unicode, eci, utf-8, barcode generation, barcode recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Han Xin barcode containing Unicode text,
/// encodes it with UTF‑8 ECI, and then reads it back to verify correct decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, decodes it, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Sample Unicode text containing Latin, Chinese, and an emoji character.
        string originalText = "Hello 你好 🌍";

        // Build a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "hanxin.png");

        // ------------------------------------------------------------
        // Generate a Han Xin barcode with UTF‑8 ECI encoding.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, originalText))
        {
            // Set the encoding mode to ECI and specify UTF‑8 as the character set.
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as PNG.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode from the saved image.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tempPath, DecodeType.HanXin))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Ensure at least one barcode was detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Retrieve the decoded text; fall back to empty string if null.
            string decodedText = results[0].CodeText ?? string.Empty;

            // Output original and decoded texts along with a success indicator.
            Console.WriteLine($"Original Text: {originalText}");
            Console.WriteLine($"Decoded Text : {decodedText}");
            Console.WriteLine($"Decoding {(string.IsNullOrEmpty(decodedText) ? "failed" : "succeeded")}");
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid interrupting the flow.
        }
    }
}