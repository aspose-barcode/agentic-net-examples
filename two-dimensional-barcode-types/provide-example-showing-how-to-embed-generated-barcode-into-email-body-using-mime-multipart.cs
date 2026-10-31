// Title: Embed generated barcode image into an email body using MIME multipart
// Description: Demonstrates creating a barcode with Aspose.BarCode, storing it in memory, and embedding it as an inline image in an HTML email via MIME multipart.
// Category-Description: This example belongs to the Aspose.BarCode email integration category, showing how to combine barcode generation (BarcodeGenerator, EncodeTypes) with .NET System.Net.Mail to produce MIME messages containing embedded images. Typical use cases include sending invoices, tickets, or verification codes where the barcode must appear directly in the email body. Developers often need to generate the barcode on the fly, attach it as a LinkedResource, and reference it with a content ID in HTML.
// Prompt: Provide example showing how to embed generated barcode into an email body using MIME multipart.
// Tags: barcode, code128, embed, email, mime, multipart, html, linkedresource, aspose.barcode, generation

using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a generated barcode image into an email body using MIME multipart.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, embeds it in an HTML email, and writes a summary to the console.
    /// </summary>
    static void Main()
    {
        // Generate barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set high resolution for better quality
            generator.Parameters.Resolution = 300;

            // Save barcode to a memory stream in PNG format
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                barcodeStream.Position = 0;

                // Create a new email message
                var mail = new MailMessage
                {
                    From = new MailAddress("sender@example.com"),
                    Subject = "Barcode Embedded Email"
                };
                mail.To.Add("recipient@example.com");

                // Build HTML body that references the embedded image via Content-ID
                string htmlBody = @"<html><body><p>Here is your barcode:</p><img src=""cid:BarcodeImage"" alt=""Barcode""/></body></html>";
                var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                // Create a linked resource for the barcode image
                using (var linkedResource = new LinkedResource(barcodeStream, MediaTypeNames.Image.Png))
                {
                    linkedResource.ContentId = "BarcodeImage";
                    linkedResource.TransferEncoding = TransferEncoding.Base64;

                    // Attach the linked resource to the HTML view
                    htmlView.LinkedResources.Add(linkedResource);

                    // Add the HTML view to the email
                    mail.AlternateViews.Add(htmlView);

                    // Output summary to console
                    Console.WriteLine("Email prepared with embedded barcode.");
                    Console.WriteLine("From: {0}", mail.From);
                    Console.WriteLine("To: {0}", string.Join(", ", mail.To));
                    Console.WriteLine("Subject: {0}", mail.Subject);
                }
            }
        }
    }
}