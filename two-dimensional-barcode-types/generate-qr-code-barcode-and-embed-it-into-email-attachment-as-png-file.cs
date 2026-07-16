// Title: Generate QR Code and embed in email attachment
// Description: Demonstrates creating a QR Code barcode, saving it as a PNG file, and attaching it to an email message stored in a pickup directory.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and email integration category. It showcases the use of BarcodeGenerator (EncodeTypes, QRErrorLevel) to produce QR Code images, and System.Net.Mail (MailMessage, Attachment, SmtpClient) to compose an email with the barcode as an attachment. Developers often need to automate barcode creation and embed them in communications such as emails, reports, or documents; this snippet provides a concise reference for those scenarios.
/// Prompt: Generate QR Code barcode and embed it into an email attachment as PNG file.
/// Tags: qr code, barcode generation, email attachment, png, aspose.barcode, smtp

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as a PNG image,
/// and attaches the image to an email saved in a specified pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory for the PNG and email pickup files
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the QR code image
        string qrImagePath = Path.Combine(outputDir, "qrcode.png");

        // Generate the QR code barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the QR code error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Save(qrImagePath);
        }

        // Create an email message and attach the generated QR code image
        using (var message = new MailMessage())
        {
            message.From = new MailAddress("sender@example.com");
            message.To.Add(new MailAddress("recipient@example.com"));
            message.Subject = "QR Code Attachment";
            message.Body = "Please find the QR code attached.";

            // Add the PNG file as an attachment to the email
            using (var attachment = new Attachment(qrImagePath))
            {
                message.Attachments.Add(attachment);

                // Configure SmtpClient to write the email to the pickup directory instead of sending it
                using (var client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = outputDir;
                    client.Send(message);
                }
            }
        }

        // Notify that the process has completed successfully
        Console.WriteLine("QR code generated and email saved to pickup directory.");
    }
}