// Title: Generate QR Code and embed as base64 image in email HTML
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG byte array, encoding it as Base64, and inserting it inline into an email HTML template.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image format handling to produce PNG output, then uses standard .NET conversion to embed the image as a data URI. Developers often need to embed barcodes directly into HTML emails or web pages without external image files, making this pattern useful for dynamic email content generation.
// Prompt: Generate QR Code barcode and embed barcode into an email template using inline base64 image.
// Tags: qr code, barcode generation, base64, email template, html, aspose.barcode, png, inline image

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode, encodes it as a Base64 PNG,
/// and embeds the image directly into an HTML email template.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the resulting HTML to the console.
    /// </summary>
    static void Main()
    {
        // Define the content that the QR code will encode.
        string qrContent = "https://example.com";

        // Initialize the barcode generator for QR Code with the specified content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Configure optional appearance settings for the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;                     // Size of a single module.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;      // Error correction level.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;   // Foreground color.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;          // Background color.

            // Render the barcode into a memory stream as a PNG image.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG byte array to a Base64 string for embedding.
                string base64Image = Convert.ToBase64String(imageBytes);

                // Build a simple HTML email template that includes the QR code as an inline image.
                string emailHtml = $"<html><body>" +
                                   $"<h2>Welcome!</h2>" +
                                   $"<p>Scan the QR code below to visit our site:</p>" +
                                   $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"QR Code\"/>" +
                                   $"</body></html>";

                // Output the generated HTML to the console (or further processing).
                Console.WriteLine(emailHtml);
            }
        }
    }
}