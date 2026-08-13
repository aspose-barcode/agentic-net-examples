// Title: Embed Barcode as Data URI in HTML
// Description: Demonstrates generating a Code128 barcode, converting it to PNG, and embedding it in an HTML img tag using a data URI.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images in memory. Typical use cases include embedding barcodes directly into web pages or emails without writing files to disk. Developers often need to convert generated images to Base64 strings for data URI usage, enabling seamless integration in HTML content.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: barcode symbology, generation, png, data-uri, html, memorystream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, converts it to a PNG image in memory,
/// and outputs an HTML <img> tag with a data URI containing the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Writes the HTML img tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator for Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Retrieve the raw image bytes from the stream.
                byte[] imageBytes = ms.ToArray();

                // Encode the image bytes to a Base64 string for the data URI.
                string base64 = Convert.ToBase64String(imageBytes);

                // Build the HTML <img> tag with the data URI source.
                string htmlImg = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Output the HTML string to the console.
                Console.WriteLine(htmlImg);
            }
        }
    }
}