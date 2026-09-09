// Title: Embed generated barcode into email body using MIME multipart
// Description: Demonstrates creating a Code128 barcode image, converting it to Base64, and constructing a MIME multipart/related message with the barcode embedded for email.
// Category-Description: This example belongs to the Aspose.BarCode generation and integration category, showing how to generate barcode images (using BarcodeGenerator) and embed them in email content via MIME multipart messages. Developers often need to include barcodes in automated emails, reports, or notifications, and this snippet illustrates the typical workflow of image generation, Base64 encoding, and MIME assembly using standard .NET classes.
// Prompt: Provide example showing how to embed generated barcode into an email body using MIME multipart.
// Tags: code128, barcode generation, png, mime multipart, email embedding, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a generated barcode image into an email body using MIME multipart/related format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, encodes it as Base64, and builds a MIME message with the image embedded.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate barcode image and obtain its Base64 representation
        // ------------------------------------------------------------
        string codeText = "12345678";
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save barcode as PNG into memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert PNG bytes to Base64 string for MIME embedding
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        // ------------------------------------------------------------
        // Build MIME multipart/related email with embedded barcode image
        // ------------------------------------------------------------
        string boundary = "----=_Part_" + Guid.NewGuid().ToString("N");
        var sb = new StringBuilder();

        // Email headers
        sb.AppendLine("From: sender@example.com");
        sb.AppendLine("To: recipient@example.com");
        sb.AppendLine("Subject: Barcode Email");
        sb.AppendLine("MIME-Version: 1.0");
        sb.AppendLine($"Content-Type: multipart/related; boundary=\"{boundary}\"");
        sb.AppendLine();

        // HTML part referencing the embedded image via Content-ID
        sb.AppendLine($"--{boundary}");
        sb.AppendLine("Content-Type: text/html; charset=\"utf-8\"");
        sb.AppendLine("Content-Transfer-Encoding: 7bit");
        sb.AppendLine();
        sb.AppendLine("<html><body><p>Here is the barcode:</p>");
        sb.AppendLine("<img src=\"cid:barcodeImage\" alt=\"Barcode\"/>");
        sb.AppendLine("</body></html>");
        sb.AppendLine();

        // Image part containing the Base64-encoded PNG
        sb.AppendLine($"--{boundary}");
        sb.AppendLine("Content-Type: image/png");
        sb.AppendLine("Content-Transfer-Encoding: base64");
        sb.AppendLine("Content-ID: <barcodeImage>");
        sb.AppendLine();
        sb.AppendLine(base64Image);
        sb.AppendLine($"--{boundary}--");

        // Output the complete MIME message to console
        Console.WriteLine(sb.ToString());
    }
}