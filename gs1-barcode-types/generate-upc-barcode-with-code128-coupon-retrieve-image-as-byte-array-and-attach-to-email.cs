using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample UPC‑A with GS1‑128 coupon data
        const string barcodeText = "514141100906(8102)03";

        // Create the barcode generator for the specific symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, barcodeText))
        {
            // Optional: set image size (points) and resolution
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 300f; // 300 DPI

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream as PNG and obtain the byte array
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Prepare an email with the barcode attached
                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress("sender@example.com");
                        message.To.Add("recipient@example.com");
                        message.Subject = "UPC‑A Coupon Barcode";
                        message.Body = "Please find the generated barcode attached.";

                        // Attach the barcode image from the byte array
                        using (var attachmentStream = new MemoryStream(imageBytes))
                        {
                            attachmentStream.Position = 0;
                            var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                            message.Attachments.Add(attachment);

                            // Send the email (configure SMTP as needed)
                            using (var client = new SmtpClient("localhost"))
                            {
                                try
                                {
                                    client.Send(message);
                                    Console.WriteLine("Email sent successfully.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Failed to send email: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}