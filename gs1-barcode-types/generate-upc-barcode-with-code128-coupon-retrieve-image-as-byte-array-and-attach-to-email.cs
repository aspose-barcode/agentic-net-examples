using System;
using System.IO;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A with GS1‑128 coupon barcode,
/// saving it to a PNG image, and attaching it to an email message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, writes it to a memory stream,
    /// and creates an email with the barcode attached.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the barcode text: UPC‑A part, GS1‑128 data in parentheses, and additional data.
        string codeText = "514141100906(8102)03";

        // Initialize the barcode generator for UPC‑A with GS1‑128 coupon encoding.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] barcodeBytes = ms.ToArray(); // Convert stream to byte array.

                // Create a memory stream for the attachment using the barcode bytes.
                using (var attachmentStream = new MemoryStream(barcodeBytes))
                {
                    // Build the email message.
                    using (var message = new MailMessage())
                    {
                        // Set sender and recipient addresses.
                        message.From = new MailAddress("sender@example.com");
                        message.To.Add("recipient@example.com");

                        // Set email subject and body.
                        message.Subject = "UPC‑A with Code128 Coupon Barcode";
                        message.Body = "Please find the generated barcode attached.";

                        // Create the attachment (PNG image) and add it to the message.
                        var attachment = new Attachment(attachmentStream, "barcode.png", "image/png");
                        message.Attachments.Add(attachment);

                        // Output the size of the generated barcode image for verification.
                        Console.WriteLine($"Barcode generated: {barcodeBytes.Length} bytes.");
                    }
                }
            }
        }
    }
}