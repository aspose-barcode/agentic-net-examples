// Title: Encode Unicode Text in Han Xin Barcode with UTF‑8 ECI and Verify Decoding
// Description: Demonstrates generating a Han Xin barcode that contains Unicode characters (including Chinese and emoji) using UTF‑8 ECI encoding, then reads the barcode back to confirm the text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.HanXin, configure HanXinEncodeMode.ECI and ECIEncodings.UTF8, and then employ BarCodeReader to decode the image. Developers working with multi‑language or Unicode data often need to embed such text in 2‑D barcodes and verify correct round‑trip encoding.
// Prompt: Encode Unicode text in Han Xin barcode using UTF‑8 ECI and verify correct decoding.
// Tags: hanxin,unicode,eci,utf-8,barcode generation,barcode recognition,aspnet,aspose.barcode,c#,png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Han Xin barcode containing Unicode text (Chinese characters and an emoji) using UTF‑8 ECI encoding,
/// saves it to a temporary PNG file, then reads the barcode back to verify the decoded text matches the original.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Unicode text to encode (includes Chinese characters and an emoji)
        string unicodeText = "汉字测试 🚀";

        // Create a unique temporary file path for the barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "HanXin_" + Guid.NewGuid().ToString("N") + ".png");

        // Generate Han Xin barcode with UTF-8 ECI encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, unicodeText))
        {
            // Set encoding mode to ECI and specify UTF-8 charset
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image to the temporary file in PNG format
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the barcode from the saved image
        using (var reader = new BarCodeReader(tempFile, DecodeType.HanXin))
        {
            // Ensure the reader detects Unicode encoding (default is true)
            reader.BarcodeSettings.DetectEncoding = true;

            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Retrieve the decoded text from the first detected barcode
                string decodedText = results[0].CodeText ?? string.Empty;
                Console.WriteLine("Decoded text: " + decodedText);
                Console.WriteLine("Match original: " + (decodedText == unicodeText));
            }
        }

        // Optionally delete the temporary file
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}