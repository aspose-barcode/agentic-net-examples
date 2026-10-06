// Title: Generate a Code128 barcode and embed it in an HTML img tag using a data URI
// Description: This example creates a Code128 barcode, saves it to a PNG image in memory, converts it to a Base64 data URI, and outputs an HTML <img> tag. Useful for embedding barcodes directly into web pages without separate image files.
// Category-Description: Demonstrates Aspose.BarCode image generation and in‑memory handling, part of the barcode generation and rendering category. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat options, which developers commonly use to create barcodes for web, reports, or email content where embedding images as data URIs is required.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: code128, barcode generation, data uri, html img, aspose.barcode, png, memory stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and embedding it in an HTML img tag using a data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, converts it to Base64, builds the img tag, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading
                memoryStream.Position = 0;

                // Convert the image bytes to a Base64 string
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Build the HTML <img> tag with a data URI containing the Base64 PNG
                string htmlImgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML tag to the console
                Console.WriteLine(htmlImgTag);
            }
        }
    }
}