// Title: Generate QR Code and embed as Base64 image in email HTML
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a Base64 PNG, and inserting it inline into an HTML email template.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator, set QR parameters, and render the barcode as an image. Typical use cases include embedding barcodes in emails, web pages, or documents where a self‑contained image is required. Developers often need to convert generated images to Base64 for inline display without external files.
// Prompt: Generate QR Code barcode and embed barcode into an email template using inline base64 image.
// Tags: qr, barcode, generation, base64, email, html, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code, converts it to a Base64‑encoded PNG,
/// and embeds the image directly into an HTML email template.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code, encodes it, builds the email HTML, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text (URL in this case)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set the QR error correction level to improve readability after damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image as a Bitmap object
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Encode the Bitmap to PNG format in a memory stream
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);

                    // Convert the PNG byte array to a Base64 string for inline embedding
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Build a simple HTML email template that includes the Base64 image
                    string emailHtml = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>QR Code Email</title>
</head>
<body>
    <p>Hello,</p>
    <p>Please scan the QR code below:</p>
    <img src=""data:image/png;base64,{base64}"" alt=""QR Code"" />
    <p>Thank you.</p>
</body>
</html>";

                    // Output the generated HTML to the console (could be saved to a file or sent via email)
                    Console.WriteLine(emailHtml);
                }
            }
        }
    }
}