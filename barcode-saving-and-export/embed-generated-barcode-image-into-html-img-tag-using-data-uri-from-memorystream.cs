// Title: Generate Code128 Barcode and Embed as Data URI in HTML
// Description: Creates a Code128 barcode image, encodes it to Base64, and embeds it in an HTML img tag using a data URI.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: barcode, code128, datauri, html, memorystream, png, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a barcode image, convert it to a Base64 data URI,
/// and embed it within an HTML <img> tag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, encodes it,
    /// and writes the resulting HTML img tag to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64 string
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Build the HTML <img> tag using a data URI that embeds the Base64 image
                string htmlImgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML tag to the console
                Console.WriteLine(htmlImgTag);
            }
        }
    }
}