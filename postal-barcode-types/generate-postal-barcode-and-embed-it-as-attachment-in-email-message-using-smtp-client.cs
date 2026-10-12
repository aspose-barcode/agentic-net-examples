// Title: Generate Swiss Post Parcel Barcode and Email as Attachment
// Description: Creates a Swiss Post Parcel barcode image and sends it via SMTP as an email attachment.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation (BarcodeGenerator, EncodeTypes) and image saving (BarCodeImageFormat) combined with .NET's System.Net.Mail for sending emails with attachments. It is useful for developers who need to produce postal barcodes (e.g., Swiss Post) and integrate them into automated email workflows, such as shipping notifications or batch processing systems.
// Prompt: Generate a postal barcode and embed it as an attachment in an email message using SMTP client.
// Tags: swisspostparcel, barcode generation, email attachment, smtp, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Swiss Post Parcel barcode, save it as a PNG file,
/// and send it as an email attachment using an SMTP client.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, composes the email,
    /// sends it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempDir, "postal.png");

        // Sample Swiss Post Parcel code text.
        string codeText = "98.34.123456.12345678";

        // Generate the barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Save the barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Email configuration.
        string from = "sender@example.com";
        string to = "recipient@example.com";
        string subject = "Postal Barcode Attachment";
        string body = "Please find the postal barcode attached.";

        // Compose the email message.
        using (var message = new MailMessage())
        {
            message.From = new MailAddress(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            // Attach the generated barcode image if it exists.
            if (File.Exists(barcodePath))
            {
                var attachment = new Attachment(barcodePath);
                message.Attachments.Add(attachment);
            }
            else
            {
                Console.WriteLine("Barcode file not found.");
                return;
            }

            // Configure and use the SMTP client to send the email.
            using (var client = new SmtpClient("localhost"))
            {
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                // client.Credentials = new NetworkCredential("user", "password");

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

        // Clean up temporary files and directory.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions.
        }
    }
}