// Title: Generate a Code128 barcode and embed it in HTML using a data URI
// Description: This example creates a Code128 barcode, encodes it as PNG in memory, converts it to Base64, and builds an HTML <img> tag with a data URI.
// Category-Description: Demonstrates Aspose.BarCode image generation and in‑memory handling for web integration. It uses BarcodeGenerator, BarCodeImageFormat, and MemoryStream to produce PNG data without writing to disk, a common scenario for dynamically generated barcodes in web pages or email content. Developers looking for quick barcode embedding techniques can reference this pattern.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: barcode, code128, datauri, html, png, memorystream, aspose.barcode, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, convert it to a Base64 data URI,
/// and embed it directly into an HTML <img> tag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, encodes it as PNG,
    /// creates a data URI, and writes the resulting HTML img tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Create a memory stream to hold the generated PNG image.
        using (MemoryStream barcodeStream = new MemoryStream())
        {
            // Initialize the barcode generator with the chosen type and text.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the barcode as a PNG image directly into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Convert the image bytes from the memory stream to a Base64 string.
            string base64 = Convert.ToBase64String(barcodeStream.ToArray());

            // Build the HTML <img> tag using a data URI that embeds the Base64 PNG.
            string imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

            // Output the generated HTML to the console.
            Console.WriteLine(imgTag);
        }
    }
}