// Title: Generate Swiss Post Postal Barcode and Email as Attachment
// Description: Demonstrates generating a Swiss Post domestic mail barcode and sending it as a PNG attachment via SMTP.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and email integration category. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configure barcode dimensions, render the image to a stream, and attach it to a System.Net.Mail.MailMessage for sending through SmtpClient. Developers often need to embed barcodes in communications such as order confirmations or shipping notifications, and this pattern illustrates the typical workflow.
// Prompt: Generate a postal barcode and embed it as an attachment in an email message using SMTP client.
// Tags: barcode generation, swisspost, postal, email attachment, smtp, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss Post barcode and sends it as an email attachment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, attaches it to an email, and sends via SMTP.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Swiss Post Domestic Mail with the required data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "98.34.123456.12345678"))
        {
            // Set barcode visual parameters: X-dimension and bar height in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Render the barcode to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Build the email message and attach the barcode image.
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "Postal Barcode Attachment";
                    message.Body = "Please find the postal barcode attached.";
                    message.Attachments.Add(new Attachment(barcodeStream, "barcode.png", "image/png"));

                    // Configure the SMTP client (localhost on port 25 by default).
                    using (var client = new SmtpClient("localhost", 25))
                    {
                        // Uncomment and set credentials if the SMTP server requires authentication.
                        // client.Credentials = new NetworkCredential("username", "password");
                        // client.EnableSsl = true;

                        try
                        {
                            client.Send(message);
                            Console.WriteLine("Email sent successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to send email: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}