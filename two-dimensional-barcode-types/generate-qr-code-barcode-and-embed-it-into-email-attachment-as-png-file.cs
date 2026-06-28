using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, attaching it to an email, and saving the email to a pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, creates an email with the image attached,
    /// and writes the email to a specified pickup directory.
    /// </summary>
    static void Main()
    {
        // Define the content that will be encoded in the QR code.
        const string qrContent = "https://www.example.com";

        // Use a memory stream to hold the generated PNG image of the QR code.
        using (var imageStream = new MemoryStream())
        {
            // Generate the QR code and write it directly into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
            {
                // Set a high error correction level to improve readability if the image is damaged.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the QR code as a PNG image into the stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read from the start.
            imageStream.Position = 0;

            // Create a new email message.
            using (var message = new MailMessage())
            {
                // Set the sender and recipient addresses.
                message.From = new MailAddress("sender@example.com");
                message.To.Add(new MailAddress("recipient@example.com"));

                // Define the subject and body of the email.
                message.Subject = "QR Code Attachment";
                message.Body = "Please find the QR code attached.";

                // Create an attachment from the QR code image stream.
                var attachment = new Attachment(imageStream, "qr.png", "image/png");
                message.Attachments.Add(attachment);

                // Determine the pickup directory path relative to the current working directory.
                string pickupDir = Path.Combine(Directory.GetCurrentDirectory(), "Emails");

                // Ensure the pickup directory exists; create it if it does not.
                if (!Directory.Exists(pickupDir))
                {
                    Directory.CreateDirectory(pickupDir);
                }

                // Configure the SMTP client to write the email to the pickup directory instead of sending it.
                using (var smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = pickupDir;

                    // Send (i.e., save) the email message.
                    smtp.Send(message);
                }

                // Inform the user where the email was saved.
                Console.WriteLine($"Email saved to pickup directory: {pickupDir}");
            }
        }
    }
}