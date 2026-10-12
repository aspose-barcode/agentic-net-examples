// Title: Encode Unicode text in a Han Xin barcode with UTF‑8 ECI and verify decoding
// Description: Demonstrates generating a Han Xin barcode that contains Unicode characters (including Chinese and emoji) using UTF‑8 ECI encoding, then reads the barcode back to confirm the text is correctly decoded.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator with EncodeTypes.HanXin, setting HanXinEncodeMode to ECI and specifying ECIEncodings.UTF8, followed by BarCodeReader for decoding. Developers working with multi‑language or Unicode data often need to embed such text in barcodes and ensure accurate round‑trip conversion, making this pattern essential for internationalized applications.
// Prompt: Encode Unicode text in Han Xin barcode using UTF‑8 ECI and verify correct decoding.
// Tags: hanxin, unicode, eci, utf-8, barcode generation, barcode recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates encoding Unicode text into a Han Xin barcode using UTF‑8 ECI and verifying the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it to a temporary PNG file, and reads it back to confirm the text.
    /// </summary>
    static void Main()
    {
        // Define the text containing ASCII, Chinese characters, and an emoji
        string codeText = "Hello 你好 🌍";

        // Initialize the barcode generator for Han Xin symbology with the given text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set encoding mode to ECI and specify UTF‑8 encoding
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Determine a temporary file path and save the bitmap as PNG
                string imagePath = Path.Combine(Path.GetTempPath(), "HanXinBarcode.png");
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }

                // Prepare a reader for Han Xin barcode decoding
                BaseDecodeType decodeType = DecodeType.HanXin;
                using (var reader = new BarCodeReader(bitmap, decodeType))
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If a result is found, output original and decoded text; otherwise report failure
                    if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        Console.WriteLine($"Original text: {codeText}");
                        Console.WriteLine($"Decoded text : {results[0].CodeText}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to decode the Han Xin barcode.");
                    }
                }
            }
        }
    }
}