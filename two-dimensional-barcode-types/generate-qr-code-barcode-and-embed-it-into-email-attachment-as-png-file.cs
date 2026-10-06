// Title: Generate QR Code and embed as PNG email attachment
// Description: Creates a QR code image using Aspose.BarCode, saves it as a PNG in memory, and attaches it to an email saved to a pickup directory.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and export category. It demonstrates how to use the BarcodeGenerator class to create QR Code barcodes, export them to common image formats (PNG), and integrate the resulting image with .NET's System.Net.Mail API for email composition. Typical use cases include automated report generation, marketing emails, and document workflows where barcodes need to be delivered as email attachments. Developers often combine Aspose.BarCode with MailMessage and SmtpClient to embed barcodes in communications without persisting temporary files on disk.
/// Prompt: Generate QR Code barcode and embed it into an email attachment as PNG file.
// Tags: qr code, barcode generation, image png, email attachment, aspose.barcode, csharp

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, converting it to a PNG image,
/// and attaching it to an email saved in a pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code, builds the email with the PNG attachment,
    /// and stores the email in a temporary pickup folder.
    /// </summary>
    static void Main()
    {
        // Define the content to encode in the QR code.
        string qrText = "https://example.com";

        // Initialize the barcode generator for QR encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Create a memory stream to hold the PNG image.
            using (var qrStream = new MemoryStream())
            {
                // Save the generated QR code directly to the memory stream as PNG.
                generator.Save(qrStream, BarCodeImageFormat.Png);
                qrStream.Position = 0; // Reset stream position for reading.

                // Build the email message.
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "QR Code Attachment";
                    message.Body = "Please find the QR code attached.";

                    // Create an attachment from the PNG stream.
                    var attachment = new Attachment(qrStream, "qr.png", "image/png");
                    message.Attachments.Add(attachment);

                    // Define a temporary pickup directory for the email.
                    string pickupDir = Path.Combine(Path.GetTempPath(), "EmailOutput");
                    Directory.CreateDirectory(pickupDir);

                    // Configure the SMTP client to use the pickup directory.
                    using (var client = new SmtpClient())
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = pickupDir;

                        try
                        {
                            // Save the email to the pickup folder instead of sending it.
                            client.Send(message);
                            Console.WriteLine($"Email saved to: {pickupDir}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to create email: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}