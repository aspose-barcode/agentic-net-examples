using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code, converting it to a Base64 string,
/// and embedding it in a simple HTML email body.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code for a given URL and prints an HTML snippet containing the image.
    /// </summary>
    static void Main()
    {
        // The URL or text that will be encoded into the QR code.
        string qrText = "https://example.com";

        // Create a BarcodeGenerator for QR encoding using the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set the QR code error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Use a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the QR code image into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64 string.
                string base64Image = Convert.ToBase64String(ms.ToArray());

                // Build a minimal HTML email body that embeds the Base64 image.
                string emailBody = "<html><body>" +
                                   "<h2>Your QR Code</h2>" +
                                   $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"QR Code\"/>" +
                                   "</body></html>";

                // Output the HTML to the console (replace with SMTP send in production).
                Console.WriteLine(emailBody);
            }
        }
    }
}