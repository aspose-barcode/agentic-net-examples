// Title: Generate QR Code with UTF‑8 URL
// Description: Demonstrates creating a QR Code that encodes a URL containing UTF‑8 characters, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode QR Code generation category. It shows how to use BarcodeGenerator, set QR encoding mode to ECI, specify UTF‑8 encoding, adjust error correction level, and customize image size and colors. Developers often need to embed internationalized URLs in QR codes for web links, marketing, or product information.
// Prompt: Generate QR Code barcode and embed a URL containing UTF‑8 characters for international link.
// Tags: qr code, utf-8, url, barcode generation, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program demonstrating QR Code generation with a UTF‑8 encoded URL using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code image file named "qr_utf8.png".
    /// </summary>
    static void Main()
    {
        // International URL containing UTF‑8 characters
        string url = "https://例子.测试/路径?查询=值";

        // Create a QR Code generator with the URL as the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, url))
        {
            // Use ECI encoding for UTF‑8 characters
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Set a high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: define image size (300 × 300 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Optional: set foreground and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the QR Code image as PNG
            generator.Save("qr_utf8.png");
        }

        // Indicate completion
        Console.WriteLine("QR Code generated: qr_utf8.png");
    }
}