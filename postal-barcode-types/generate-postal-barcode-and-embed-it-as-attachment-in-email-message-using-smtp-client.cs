// Title: Generate Swiss Post Parcel barcode and embed as email attachment
// Description: Demonstrates creating a Swiss Post Parcel barcode image and attaching it to an email saved via SMTP pickup directory.
// Category-Description: This example belongs to the Aspose.BarCode generation and email integration category. It shows how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, then use System.Net.Mail to compose a MailMessage with an Attachment, and finally deliver the message using SmtpClient with SpecifiedPickupDirectory. Developers working with postal barcodes, automated email reports, or document workflows often need to generate barcode graphics and embed them in emails without sending through an actual SMTP server.
// Prompt: Generate a postal barcode and embed it as an attachment in an email message using SMTP client.
// Tags: postal barcode, swisspostparcel, barcode generation, email attachment, smtp client, aspose.barcode, png

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode and attaching it to an email saved via SMTP pickup directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, creates the email with attachment, and saves the .eml file.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder that will act as the SMTP pickup directory
        string pickupFolder = Path.Combine(Path.GetTempPath(), "EmailPickup_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(pickupFolder);

        // Initialize the barcode generator for Swiss Post Parcel symbology with the required data string
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "98.34.123456.12345678"))
        {
            // Configure visual appearance: set module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Render the barcode to an in‑memory PNG stream
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading

                // Build the email message and attach the barcode image
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress("sender@example.com");
                    message.To.Add(new MailAddress("recipient@example.com"));
                    message.Subject = "Postal Barcode Attachment";
                    message.Body = "Please find the generated postal barcode attached.";

                    // Create an attachment from the barcode stream
                    var attachment = new Attachment(barcodeStream, "SwissPostBarcode.png", "image/png");
                    message.Attachments.Add(attachment);

                    // Configure the SMTP client to write the .eml file to the pickup folder instead of sending
                    using (var client = new SmtpClient())
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = pickupFolder;
                        client.Send(message);
                    }

                    // Clear attachments to release the stream before disposing the message
                    message.Attachments.Clear();
                }

                // Explicitly dispose the barcode stream after the email has been saved
                barcodeStream.Dispose();
            }
        }

        Console.WriteLine("Email with barcode attachment has been saved to: " + pickupFolder);
    }
}