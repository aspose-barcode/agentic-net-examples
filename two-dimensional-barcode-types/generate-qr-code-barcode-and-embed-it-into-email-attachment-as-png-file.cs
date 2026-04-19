using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // QR code data
        const string qrText = "https://example.com";

        // Generate QR code image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save bitmap to a memory stream as PNG
                using (var pngStream = new MemoryStream())
                {
                    bitmap.Save(pngStream, ImageFormat.Png);
                    pngStream.Position = 0; // Reset stream position for reading

                    // Create email message with PNG attachment
                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress("sender@example.com");
                        message.To.Add("recipient@example.com");
                        message.Subject = "QR Code Attachment";
                        message.Body = "Please find the QR code attached.";

                        // Attach the PNG image from the memory stream
                        var attachment = new Attachment(pngStream, "qr.png", "image/png");
                        message.Attachments.Add(attachment);

                        // For demonstration, write the email details to console
                        Console.WriteLine("Email prepared with QR code attachment.");
                        Console.WriteLine($"Subject: {message.Subject}");
                        Console.WriteLine($"To: {string.Join(", ", message.To)}");
                        Console.WriteLine($"Attachment: {attachment.Name}");

                        // Optionally, send the email using an SMTP server
                        // using (var smtp = new SmtpClient("localhost"))
                        // {
                        //     smtp.Send(message);
                        // }
                    }
                }
            }
        }
    }
}