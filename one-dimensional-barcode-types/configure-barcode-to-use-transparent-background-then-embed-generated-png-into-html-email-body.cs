// Title: Generate a transparent Code128 barcode and embed it in an HTML email body
// Description: Demonstrates how to create a Code128 barcode with a transparent background, convert it to a PNG, and embed the image as a Base64 data URI in an HTML email.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, setting visual parameters like BackColor, and exporting the barcode as a PNG bitmap. Typical use cases include creating barcode images for email communications, web pages, or documents where a transparent background is required. Developers often need to embed generated barcodes directly into HTML content without saving intermediate files.
// Prompt: Configure barcode to use transparent background, then embed generated PNG into an HTML email body.
// Tags: code128, transparent background, png, html email, base64, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background,
/// converts it to a PNG image, and embeds the image in an HTML email body using a Base64 data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, encodes it, and writes the HTML to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the barcode to have a transparent background.
            generator.Parameters.BackColor = Color.Transparent;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();

                    // Convert the PNG byte array to a Base64 string for embedding.
                    string base64 = Convert.ToBase64String(pngBytes);

                    // Build a simple HTML email body that includes the barcode image.
                    string html = $"<html><body><p>Barcode image:</p><img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" /></body></html>";

                    // Output the generated HTML to the console.
                    Console.WriteLine(html);
                }
            }
        }
    }
}