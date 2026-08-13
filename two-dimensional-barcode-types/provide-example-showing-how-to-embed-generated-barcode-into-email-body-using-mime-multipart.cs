// Title: Embed generated barcode image into an email body using MIME multipart
// Description: Demonstrates creating a Code128 barcode, embedding it as an inline image in an HTML email, and saving the message to a pickup directory.
// Category-Description: This example belongs to the Aspose.BarCode generation and email integration category. It shows how to use BarcodeGenerator, BarCodeImageFormat, and .NET System.Net.Mail classes to produce a barcode, convert it to a PNG stream, and embed it as a linked resource in a multipart/alternative email. Developers often need to programmatically attach barcodes to emails for invoices, tickets, or notifications.
// Prompt: Provide example showing how to embed generated barcode into an email body using MIME multipart.
// Tags: barcode, code128, email, mime, multipart, html, linkedresource, aspose.barcode, generation, png

using System;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode and embedding it into an email body as an inline image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, builds the MIME email, and saves it to a pickup directory.
    /// </summary>
    static void Main(string[] args)
    {
        // Define the text to encode in the barcode.
        string barcodeText = "1234567890";

        // Create a BarcodeGenerator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set the barcode color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Render the barcode to a memory stream in PNG format.
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] barcodeBytes = imageStream.ToArray();

                // Build the HTML body that references the embedded image via Content-ID.
                string htmlBody = @"<html><body>
                                    <h3>Embedded Barcode</h3>
                                    <img src=""cid:barcodeImage"" alt=""Barcode""/>
                                    </body></html>";

                // Initialize the email message.
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "Barcode Email Example";
                    message.IsBodyHtml = true;

                    // Create an HTML view and attach the barcode image as a linked resource.
                    using (var htmlView = AlternateView.CreateAlternateViewFromString(
                        htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html))
                    {
                        // Wrap the barcode byte array in a stream for the linked resource.
                        using (var barcodeImageStream = new MemoryStream(barcodeBytes))
                        {
                            // Configure the linked resource with proper MIME type and Content-ID.
                            using (var linkedResource = new LinkedResource(
                                barcodeImageStream, MediaTypeNames.Image.Png))
                            {
                                linkedResource.ContentId = "barcodeImage";
                                linkedResource.TransferEncoding = TransferEncoding.Base64;

                                // Attach the linked resource to the HTML view.
                                htmlView.LinkedResources.Add(linkedResource);
                                // Add the HTML view to the email message.
                                message.AlternateViews.Add(htmlView);

                                // Set up an SmtpClient that writes the email to a temporary pickup directory.
                                using (var client = new SmtpClient())
                                {
                                    string pickupDir = Path.Combine(
                                        Path.GetTempPath(),
                                        "EmailPickup_" + Guid.NewGuid().ToString("N"));
                                    Directory.CreateDirectory(pickupDir);
                                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                                    client.PickupDirectoryLocation = pickupDir;

                                    // Send (save) the email.
                                    client.Send(message);
                                    Console.WriteLine($"Email saved to: {pickupDir}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}