// Title: Encode Unicode Text in Han Xin Barcode with UTF‑8 ECI and Verify Decoding
// Description: Demonstrates generating a Han Xin barcode that encodes Unicode characters using UTF‑8 ECI mode, then reads the barcode back to confirm the decoded text matches the original.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator with EncodeTypes.HanXin, setting HanXinEncodeMode to ECI and specifying ECIEncodings.UTF8, followed by BarCodeReader for decoding. Developers working with multi‑language or special‑character barcodes often need to encode Unicode data reliably and verify round‑trip accuracy.
// Prompt: Encode Unicode text in Han Xin barcode using UTF‑8 ECI and verify correct decoding.
// Tags: hanxin, barcode, eci, utf8, unicode, generation, recognition, c#, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Han Xin barcode containing Unicode text,
/// encodes it using UTF‑8 ECI, and then reads it back to verify the content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, decodes it, and prints the results.
    /// </summary>
    static void Main()
    {
        // Unicode text containing multiple languages and symbols.
        string unicodeText = "abcd АБВ ıntəˈnæʃənəl 语言语言 แผ่นดินฮั่นเสื่ ∑ f(i) = ∏ 🖨 🚘✉🥇⚽ 你好測試測試 こんにちは テスト テスト 안녕하세요 테스트 테스트";

        // Create a barcode generator for Han Xin symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, unicodeText))
        {
            // Configure the generator to use ECI mode with UTF‑8 encoding.
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Generate the barcode image.
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Set up a reader for Han Xin barcodes.
                BaseDecodeType decodeType = DecodeType.HanXin;
                using (var reader = new BarCodeReader(barcodeImage, decodeType))
                {
                    // Read all barcodes found in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length > 0)
                    {
                        // Retrieve the decoded text from the first result.
                        string decodedText = results[0].CodeText;

                        // Compare the original and decoded texts for an exact match.
                        bool match = string.Equals(unicodeText, decodedText, StringComparison.Ordinal);

                        // Output the comparison results.
                        Console.WriteLine($"Original: {unicodeText}");
                        Console.WriteLine($"Decoded : {decodedText}");
                        Console.WriteLine($"Match   : {match}");
                    }
                    else
                    {
                        // No barcode was detected in the image.
                        Console.WriteLine("No barcode detected.");
                    }
                }
            }
        }
    }
}