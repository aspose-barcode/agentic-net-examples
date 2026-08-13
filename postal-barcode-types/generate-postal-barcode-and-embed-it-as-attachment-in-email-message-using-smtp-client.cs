// Title: Generate Australia Post barcode and email as attachment
// Description: Demonstrates creating an Australia Post (postal) barcode image and sending it via SMTP as an email attachment.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.AustraliaPost, configure encoding tables, and save the image. It also shows integrating the generated barcode into a System.Net.Mail message for typical scenarios such as automated mailing of shipping labels. Developers often need to generate postal barcodes and embed them in emails for logistics workflows.
// Prompt: Generate a postal barcode and embed it as an attachment in an email message using SMTP client.
// Tags: australia post barcode generation email smtp attachment image png

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates an Australia Post barcode, saves it as a PNG file, and sends it as an email attachment using SMTP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, composes the email, sends it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output file name
        const string barcodeText = "5980123456AB"; // Sample data: FCC=59, DPID=8 digits, 2 CTable chars
        const string barcodeFile = "postal_barcode.png";

        // -------------------------------------------------
        // Generate the Australia Post barcode and save it
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeText))
        {
            // Use CTable encoding for customer information
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode directly to a PNG file
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Prepare email message with the barcode attached
        // -------------------------------------------------
        var fromAddress = new MailAddress("sender@example.com", "Sender");
        var toAddress = new MailAddress("recipient@example.com", "Recipient");
        const string subject = "Australia Post Barcode Attachment";
        const string body = "Please find the generated Australia Post barcode attached.";

        using (var message = new MailMessage())
        {
            message.From = fromAddress;
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;

            // Attach the generated barcode image
            using (var attachmentStream = new FileStream(barcodeFile, FileMode.Open, FileAccess.Read))
            {
                var attachment = new Attachment(attachmentStream, "postal_barcode.png", "image/png");
                message.Attachments.Add(attachment);

                // -------------------------------------------------
                // Configure and use the SMTP client to send the email
                // -------------------------------------------------
                using (var smtp = new SmtpClient("smtp.example.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("username", "password");

                    try
                    {
                        smtp.Send(message);
                        Console.WriteLine("Email sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                    }
                }
            }
        }

        // -------------------------------------------------
        // Clean up the temporary barcode file
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodeFile))
                File.Delete(barcodeFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}