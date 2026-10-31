// Title: Generate QR Code and embed as PNG email attachment
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, save it as a PNG image, and attach it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and email integration category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and .NET SmtpClient to produce QR Code images and embed them as attachments in .eml files. Developers often need to automate barcode creation for marketing, authentication, or inventory workflows and send them via email without manual steps.
// Prompt: Generate QR Code barcode and embed it into an email attachment as PNG file.
// Tags: qr code, barcode generation, email attachment, png, aspose.barcode, generation, smtp

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code, saves it as a PNG, and attaches it to an email saved in a pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary base directory for all generated files.
        string baseTemp = Path.Combine(Path.GetTempPath(), "AsposeBarcodeEmail_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(baseTemp);

        // Define paths for the PNG image and the email pickup directory.
        string pngPath = Path.Combine(baseTemp, "qr.png");
        string emailPickupDir = Path.Combine(baseTemp, "EmailPickup");
        Directory.CreateDirectory(emailPickupDir);

        // Generate QR Code and obtain PNG in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR Code appearance and error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            using (var pngStream = new MemoryStream())
            {
                // Save the generated barcode to the memory stream as PNG.
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0;

                // Optionally write the PNG to a file for verification.
                using (var fileStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
                {
                    pngStream.CopyTo(fileStream);
                }

                // Prepare the email message with the PNG attachment.
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add("recipient@example.com");
                    message.Subject = "QR Code Attachment";
                    message.Body = "Please find the QR code attached.";

                    // Reset stream position and create a separate stream for the attachment.
                    pngStream.Position = 0;
                    using (var attachmentStream = new MemoryStream(pngStream.ToArray()))
                    {
                        var attachment = new Attachment(attachmentStream, "qr.png", "image/png");
                        message.Attachments.Add(attachment);

                        // Configure SMTP client to use a pickup directory and send the message.
                        using (var client = new SmtpClient())
                        {
                            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            client.PickupDirectoryLocation = emailPickupDir;
                            client.Send(message);
                        }
                    }
                }
            }
        }

        // Output locations of generated files for reference.
        Console.WriteLine($"QR code PNG saved to: {pngPath}");
        Console.WriteLine($"Email .eml files saved to: {emailPickupDir}");
    }
}