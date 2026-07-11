// Title: Generate Postal Barcode and Email as Attachment
// Description: Demonstrates creating a Postnet barcode image and sending it as an email attachment via SMTP.
// Category-Description: This example belongs to the Aspose.BarCode generation and email integration category. It shows how to use BarcodeGenerator (EncodeTypes) to create barcode images, save them, and then attach them to a MailMessage using System.Net.Mail. Typical use cases include automating mailing labels, shipping notifications, and integrating barcode generation into notification workflows. Developers often need to generate barcodes and embed them in communications, requiring knowledge of Aspose.BarCode classes and the .NET SMTP client.
// Prompt: Generate a postal barcode and embed it as an attachment in an email message using SMTP client.
// Tags: postal barcode, postnet, email attachment, smtp, aspose.barcode, generation, png

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Postnet barcode image and sends it as an email attachment using SMTP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image.
        string barcodePath = "postal.png";

        // Generate a Postal (Postnet) barcode with the data "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Save the barcode image as a PNG file.
            generator.Save(barcodePath);
        }

        // Email configuration – replace placeholder values with real credentials and server details.
        string smtpHost = "smtp.example.com";
        int smtpPort = 587;
        string smtpUser = "user@example.com";
        string smtpPass = "password";
        string fromAddress = "sender@example.com";
        string toAddress = "recipient@example.com";

        // Create the email message and set its properties.
        using (var message = new MailMessage())
        {
            message.From = new MailAddress(fromAddress);
            message.To.Add(toAddress);
            message.Subject = "Postal Barcode Attachment";
            message.Body = "Please find the generated postal barcode attached.";

            // Attach the barcode image if the file exists.
            if (File.Exists(barcodePath))
            {
                message.Attachments.Add(new Attachment(barcodePath));
            }
            else
            {
                Console.WriteLine($"Barcode file not found: {barcodePath}");
            }

            // Configure the SMTP client and send the email.
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);
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
    }
}