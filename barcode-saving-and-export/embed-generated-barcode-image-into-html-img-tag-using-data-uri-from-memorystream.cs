// Title: Generate Code128 barcode and embed as Base64 data URI in HTML
// Description: Creates a Code128 barcode, encodes it as PNG, converts it to a Base64 string, and outputs an HTML <img> tag that embeds the image via a data URI.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation capabilities, focusing on exporting a barcode to an in‑memory image, converting it to Base64, and embedding it directly in HTML. It showcases the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat, a common pattern for web developers who need to display barcodes without storing image files on disk. Typical use cases include generating barcodes for emails, web pages, or API responses where a data URI is preferred.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: code128, barcode-generation, png, base64, html, aspose.barcode, memorystream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, convert it to a Base64‑encoded PNG,
/// and embed the result in an HTML <img> tag using a data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the HTML string to the console.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Convert the image bytes to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the HTML <img> tag with a data URI containing the Base64 PNG.
                string html = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML string.
                Console.WriteLine(html);
            }
        }
    }
}