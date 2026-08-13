// Title: Generate QR Code with UTF‑8 URL using Aspose.BarCode
// Description: Demonstrates how to create a QR Code that encodes an international URL containing UTF‑8 characters and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation with ECI (Extended Channel Interpretation) encoding for Unicode support. It showcases the use of BarcodeGenerator, EncodeTypes, and QR‑specific parameters such as EncodeMode, ECIEncoding, and ErrorLevel. Developers commonly need this pattern when embedding multilingual links or data in QR codes for web, marketing, or mobile applications.
// Prompt: Generate QR Code barcode and embed a URL containing UTF‑8 characters for international link.
// Tags: qr code, utf-8, barcode generation, png, aspose.barcode, encode types, eci encoding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code containing a UTF‑8 encoded URL
/// and saves the result as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates an international URL, generates the QR Code, and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Sample international URL containing UTF‑8 characters
        string url = "https://例子.测试/路径?参数=值";

        // Generate the QR code and save it as PNG
        GenerateQrCode(url, "qr_utf8.png");

        // Inform the user that the image has been saved
        Console.WriteLine("QR code saved to qr_utf8.png");
    }

    /// <summary>
    /// Generates a QR Code for the specified URL and saves it to the given file path.
    /// </summary>
    /// <param name="url">The URL (including UTF‑8 characters) to encode in the QR Code.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    static void GenerateQrCode(string url, string outputPath)
    {
        // Initialize a QR Code generator with the QR symbology
        using (Aspose.BarCode.Generation.BarcodeGenerator generator =
            new Aspose.BarCode.Generation.BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text (URL) to be encoded
            generator.CodeText = url;

            // Enable ECI mode to correctly handle UTF‑8 characters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Optionally set a high error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}