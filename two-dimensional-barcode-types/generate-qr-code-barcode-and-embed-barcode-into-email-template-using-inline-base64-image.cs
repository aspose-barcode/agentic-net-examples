// Title: Generate QR Code and embed as base64 image in email template
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG, encoding it as a base64 string, and inserting it inline into an HTML email body.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It shows how to use BarcodeGenerator, set QR code parameters, save the barcode to a memory stream in PNG format, and embed the resulting image using a data URI. Typical use cases include generating dynamic QR codes for marketing emails, invoices, or authentication links where the barcode must be displayed directly in HTML without external image files.
// Prompt: Generate QR Code barcode and embed barcode into an email template using inline base64 image.
// Tags: qr code, barcode generation, base64, email template, html, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, converts it to a Base64‑encoded PNG,
/// and embeds the image directly into an HTML email template using a data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, encodes it, builds the email HTML,
    /// and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Define the content that the QR code will encode.
        string qrText = "https://example.com";

        // Generate the QR code and obtain its PNG representation as a Base64 string.
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional appearance settings: set module size and error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position before reading.
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Build a simple HTML email body that includes the QR code as an inline image.
        string emailHtml = $"<html><body><p>Hello, here is your QR code:</p>" +
                           $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"QR Code\"/>" +
                           $"</body></html>";

        // Output the generated email template to the console.
        Console.WriteLine(emailHtml);
    }
}