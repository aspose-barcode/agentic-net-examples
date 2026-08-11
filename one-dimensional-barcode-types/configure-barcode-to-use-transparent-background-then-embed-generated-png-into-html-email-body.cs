// Title: Generate transparent barcode PNG and embed in HTML email
// Description: Demonstrates how to create a Code128 barcode with a transparent background, save it as a PNG, convert it to Base64, and embed it directly into an HTML email body.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images with custom visual properties. Typical scenarios include creating email-friendly barcode graphics, web embedding, or generating reports where a transparent background is required. Developers often need to customize colors, export formats, and embed images as data URIs for seamless integration.
// Prompt: Configure barcode to use transparent background, then embed generated PNG into an HTML email body.
// Tags: code128, transparent background, png, html email, base64, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background,
/// encodes the image as Base64, and embeds it in an HTML email body.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure the barcode to have a transparent background
            generator.Parameters.BackColor = Color.Transparent;

            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string for embedding
                string base64Image = Convert.ToBase64String(memoryStream.ToArray());

                // Build the HTML email body with the barcode embedded as a data URI
                string htmlEmailBody = $@"
<html>
  <body>
    <p>Here is the generated barcode with a transparent background:</p>
    <img src=""data:image/png;base64,{base64Image}"" alt=""Barcode"" />
  </body>
</html>";

                // Output the HTML content to the console (or further processing)
                Console.WriteLine(htmlEmailBody);
            }
        }
    }
}