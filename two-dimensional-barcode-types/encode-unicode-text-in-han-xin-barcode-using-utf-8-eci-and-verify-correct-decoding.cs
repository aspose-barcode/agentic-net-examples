// Title: Encode Unicode text in Han Xin barcode with UTF‑8 ECI
// Description: Demonstrates generating a Han Xin barcode containing Unicode characters using UTF‑8 ECI encoding and then reading it back to verify correctness.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Unicode handling with ECI support. It showcases the use of BarcodeGenerator and BarCodeReader classes to create and decode Han Xin symbology, a common requirement for applications that need to embed multilingual text in compact 2‑D barcodes. Developers often need to set ECI mode and specify UTF‑8 encoding to ensure accurate round‑trip of Unicode data.
// Prompt: Encode Unicode text in Han Xin barcode using UTF‑8 ECI and verify correct decoding.
// Tags: hanxin, eci, utf-8, unicode, barcode generation, barcode recognition, png, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Han Xin barcode with Unicode text using UTF‑8 ECI encoding,
/// saves it as an image, and then reads the barcode back to verify the decoded text matches the original.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, reads, and validates a Han Xin barcode.
    /// </summary>
    static void Main()
    {
        // Define the Unicode string to encode in the barcode.
        string originalText = "Unicode test: 測試 🚀 𝔘𝔫𝔦𝔠𝔬𝔡𝔢";

        // --------------------------------------------------------------------
        // Generate Han Xin barcode with UTF‑8 ECI encoding
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, originalText))
        {
            // Configure the barcode to use ECI mode with UTF‑8 encoding.
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to a PNG file.
            string imagePath = "hanxin.png";
            generator.Save(imagePath);
            Console.WriteLine($"Barcode saved to {imagePath}");
        }

        // --------------------------------------------------------------------
        // Read the saved barcode and verify the decoded text
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader("hanxin.png", DecodeType.HanXin))
        {
            // Attempt to read all barcodes from the image.
            var results = reader.ReadBarCodes();

            // If no barcode is found, report and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Retrieve the decoded text from the first barcode result.
            string decodedText = results[0].CodeText;
            Console.WriteLine($"Decoded text: {decodedText}");

            // Compare the decoded text with the original input.
            if (decodedText == originalText)
                Console.WriteLine("Success: Decoded text matches original.");
            else
                Console.WriteLine("Failure: Decoded text does not match original.");
        }
    }
}