// Title: Generate Transparent Barcode PNG and Embed in HTML Email
// Description: Demonstrates creating a Code128 barcode with a transparent background, saving it as PNG, and embedding the image directly into an HTML email body using a data URI.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance (background, colors) and integrate the generated image into HTML content. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing classes. Developers often need to embed barcodes in emails or web pages without external image files, and this snippet shows the typical workflow.
// Prompt: Configure barcode to use transparent background, then embed generated PNG into an HTML email body.
// Tags: code128, transparent background, png, html email, barcode generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background,
/// converts it to a Base64 PNG, and embeds it in an HTML email body.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, encodes it, and writes the HTML.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure the barcode to have a transparent background
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the bar (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string for embedding in HTML
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build a simple HTML email body that includes the barcode image via a data URI
                string htmlBody = $"<html><body>" +
                                  $"<h3>Generated Barcode</h3>" +
                                  $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\"/>" +
                                  $"</body></html>";

                // Output the HTML; in a real scenario this would be set as the email body
                Console.WriteLine(htmlBody);
            }
        }
    }
}