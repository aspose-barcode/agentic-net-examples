// Title: Embed a generated barcode image into an email body using MIME multipart
// Description: Demonstrates creating a Code128 barcode, converting it to PNG, and embedding it as an inline image in an HTML email via a linked resource.
// Category-Description: This example belongs to the Aspose.BarCode generation and email integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce barcode images and how to incorporate them into System.Net.Mail messages using AlternateView and LinkedResource. Developers often need to send barcodes in transactional emails, reports, or notifications, and this pattern illustrates the typical workflow for embedding images in MIME multipart messages.
// Prompt: Provide example showing how to embed generated barcode into an email body using MIME multipart.
// Tags: barcode, code128, embed, email, mime, multipart, html, linkedresource, aspose.barcode, generation, png

using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates embedding a generated barcode image into an email body using MIME multipart.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, embeds it in an HTML email, and prepares the message.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set image resolution for better quality.
            generator.Parameters.Resolution = 300;

            // Stream to hold the generated PNG image.
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                barcodeStream.Position = 0;

                // Create the email message.
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "Barcode Embedded Email";

                    // HTML body referencing the embedded image via Content-ID.
                    string htmlBody = @"<html><body><p>Here is your barcode:</p><img src=""cid:barcodeImage"" alt=""Barcode""/></body></html>";

                    // Create an alternate view for HTML content.
                    using (AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html))
                    {
                        // Create a linked resource for the barcode image.
                        LinkedResource barcodeResource = new LinkedResource(barcodeStream, MediaTypeNames.Image.Png)
                        {
                            ContentId = "barcodeImage",
                            TransferEncoding = TransferEncoding.Base64
                        };

                        // Attach the image resource to the HTML view.
                        htmlView.LinkedResources.Add(barcodeResource);

                        // Add the HTML view to the email message.
                        message.AlternateViews.Add(htmlView);

                        // Indicate that the email is ready (sending is out of scope for this example).
                        Console.WriteLine("Email message prepared with embedded barcode.");
                    }
                }
            }
        }
    }
}