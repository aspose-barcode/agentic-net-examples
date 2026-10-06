// Title: Generate QR barcode with transparent background and embed in HTML email body
// Description: Demonstrates creating a QR code with a transparent background, converting it to a Base64 PNG, and embedding the image directly into an HTML email body.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode appearance (background and bar colors) using BarcodeGenerator, export to PNG, and embed the result in HTML. Developers often need to generate barcodes for emails or web content without external image files, requiring transparent backgrounds and Base64 encoding.
// Prompt: Configure barcode to use transparent background, then embed generated PNG into an HTML email body.
// Tags: qr, barcode, transparent background, png, html email, base64, aspose.barcode, aspose.drawing, image generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR barcode with a transparent background,
/// encodes it as a Base64 PNG, and embeds it into an HTML email body saved to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology (QR code).
        string codeText = "Hello, Aspose!";
        BaseEncodeType encodeType = EncodeTypes.QR;

        // Create a BarcodeGenerator instance with the specified type and content.
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the background to transparent and the bar color to black.
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Render the barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Convert the PNG bytes to a Base64 string for embedding.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build an HTML string that includes the barcode image as a data URI.
                string html = $"<html><body><h2>Barcode Image</h2>" +
                              $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />" +
                              $"</body></html>";

                // Determine the output file path in the current directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "EmailBody.html");

                // Write the HTML content to the file using UTF-8 encoding.
                File.WriteAllText(outputPath, html, Encoding.UTF8);

                // Inform the user where the HTML file was saved.
                Console.WriteLine("HTML email body with embedded barcode saved to:");
                Console.WriteLine(outputPath);
            }
        }
    }
}