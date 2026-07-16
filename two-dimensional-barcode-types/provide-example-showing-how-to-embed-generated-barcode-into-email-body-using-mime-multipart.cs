// Title: Embed Barcode Image into Email Body Using MIME Multipart
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, embedding it as an inline image in an HTML email using MIME multipart, and saving the message to a pickup directory.
// Category-Description: This example belongs to the Aspose.BarCode email integration category, showing how to combine barcode generation (BarcodeGenerator) with .NET System.Net.Mail classes (MailMessage, AlternateView, LinkedResource) to embed barcodes directly in email bodies. Typical use cases include sending invoices, tickets, or verification codes where the barcode must appear inline. Developers often need to generate barcodes on‑the‑fly and attach them as inline images without writing temporary files.
// Prompt: Provide example showing how to embed generated barcode into an email body using MIME multipart.
// Tags: code128, barcode generation, png, email, mime, aspose.barcode, aspose.drawing, system.net.mail

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode and embedding it into an email body using MIME multipart.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, embeds it in an HTML email,
    /// and writes the resulting MIME message to a temporary .eml file.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode and keep it in a memory stream
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Optional: customize appearance
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            barcodeStream.Position = 0;

            // Create the email message
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("sender@example.com");
                mail.To.Add("recipient@example.com");
                mail.Subject = "Barcode Embedded in Email";

                // HTML body referencing the embedded image via Content-ID
                string htmlBody = @"<html><body>
                                    <p>Here is your barcode:</p>
                                    <img src=""cid:barcodeImage"" alt=""Barcode"" />
                                    </body></html>";

                // Create an alternate view for HTML content
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");

                // Create a linked resource for the barcode image
                LinkedResource barcodeResource = new LinkedResource(barcodeStream, "image/png")
                {
                    ContentId = "barcodeImage",
                    TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                };

                // Attach the linked resource to the HTML view
                htmlView.LinkedResources.Add(barcodeResource);
                mail.AlternateViews.Add(htmlView);

                // OPTIONAL: send the email (requires a reachable SMTP server)
                // using (SmtpClient client = new SmtpClient("localhost"))
                // {
                //     client.Send(mail);
                // }

                // For demonstration, output the raw MIME message to console
                using (MemoryStream mimeStream = new MemoryStream())
                {
                    // Configure SmtpClient to write the email to a pickup directory
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = Path.GetTempPath();
                        client.Send(mail);
                    }

                    // Read the generated .eml file (last file in pickup directory)
                    string[] files = Directory.GetFiles(Path.GetTempPath(), "*.eml");
                    if (files.Length > 0)
                    {
                        string emlPath = files[files.Length - 1];
                        string emlContent = File.ReadAllText(emlPath);
                        Console.WriteLine(emlContent);
                    }
                    else
                    {
                        Console.WriteLine("Email was not saved to pickup directory.");
                    }
                }
            }
        }
    }
}