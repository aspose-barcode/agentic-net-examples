// Title: Generate QR Code and embed as PNG email attachment
// Description: Demonstrates creating a QR Code barcode, saving it as a PNG in memory, and attaching it to an email message.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator (EncodeTypes, QRErrorLevel) and BarCodeImageFormat to produce barcode images. Typical scenarios include embedding barcodes in documents, reports, or communications such as emails. Developers often need to create barcodes on the fly and attach them to messages without writing temporary files to disk.
// Prompt: Generate QR Code barcode and embed it into an email attachment as PNG file.
// Tags: qr code, barcode generation, email attachment, png, aspose.barcode, aspose.drawing, smtp

using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, saves it as a PNG image,
/// and attaches it to an email message using <see cref="System.Net.Mail"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the QR code email example.
    /// </summary>
    static void Main()
    {
        GenerateQrAndCreateEmail();
    }

    static void GenerateQrAndCreateEmail()
    {
        // Text to encode in QR code
        const string qrText = "https://example.com";

        // Create a memory stream to hold the PNG image
        using (MemoryStream ms = new MemoryStream())
        {
            // Generate QR code and save as PNG into the memory stream
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Optional: set error correction level (LevelM provides a good balance of capacity and resilience)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading so the attachment can read from the beginning
            ms.Position = 0;

            // Prepare email message
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("sender@example.com");
                message.To.Add("recipient@example.com");
                message.Subject = "QR Code Attachment";
                message.Body = "Please find the QR code attached.";

                // Attach the PNG image directly from the memory stream
                Attachment attachment = new Attachment(ms, "qr.png", "image/png");
                message.Attachments.Add(attachment);

                // Configure SmtpClient to write the email to a pickup directory (no actual SMTP server needed)
                string pickupDir = Path.Combine(Path.GetTempPath(), "EmailPickup_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(pickupDir);

                using (SmtpClient client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = pickupDir;
                    client.Send(message);
                }

                Console.WriteLine($"Email with QR code saved to: {pickupDir}");
            }
        }
    }
}