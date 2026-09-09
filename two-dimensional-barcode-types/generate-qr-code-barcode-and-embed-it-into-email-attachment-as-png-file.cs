// Title: Generate QR Code and embed as PNG attachment in email
// Description: Demonstrates creating a QR Code barcode, saving it as a PNG image in memory, and attaching it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and email integration category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and .NET MailMessage classes to produce QR Code images and embed them as attachments. Developers often need to automate barcode creation for communications, reports, or notifications, and this pattern illustrates a typical workflow for generating barcodes and sending them via email.
// Prompt: Generate QR Code barcode and embed it into an email attachment as PNG file.
// Tags: qr code, barcode generation, email attachment, png, aspose.barcode, aspose.drawing, smtp

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode, converting it to PNG, and attaching it to an email saved in a pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, creates the email with the PNG attachment, and saves the email to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder that will act as the SMTP pickup directory
        string emailFolder = Path.Combine(Path.GetTempPath(), "EmailOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(emailFolder);

        // Initialize the QR Code generator with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure QR Code appearance
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Render the barcode to a PNG image stored in a memory stream
            using (MemoryStream pngStream = new MemoryStream())
            {
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0; // Reset stream position for reading

                // Build the email message
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sender@example.com");
                    mail.To.Add(new MailAddress("recipient@example.com"));
                    mail.Subject = "QR Code Attachment";
                    mail.Body = "Please find the QR code attached as a PNG image.";

                    // Attach the PNG stream as a file named "qr.png"
                    using (Attachment attachment = new Attachment(pngStream, "qr.png", "image/png"))
                    {
                        mail.Attachments.Add(attachment);

                        // Configure SMTP client to use the pickup directory and send the message
                        using (SmtpClient client = new SmtpClient())
                        {
                            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            client.PickupDirectoryLocation = emailFolder;
                            client.Send(mail);
                        }
                    }
                }
            }
        }

        // Inform the user where the email file was saved
        Console.WriteLine("Email with QR code attachment saved to: " + emailFolder);
    }
}