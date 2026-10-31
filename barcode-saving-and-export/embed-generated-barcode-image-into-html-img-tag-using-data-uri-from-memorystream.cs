// Title: Generate Code128 barcode and embed as Data URI in HTML
// Description: This example creates a Code128 barcode, saves it as a PNG image in a memory stream, converts it to a Base64 string, and embeds the result in an HTML <img> tag using a data URI.
// Category-Description: Demonstrates Aspose.BarCode generation features, specifically creating barcodes, exporting them to image formats via streams, and integrating the output into web content. The example uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and standard .NET streams—common tasks for developers building dynamic barcode images for HTML emails, reports, or web pages.
// Prompt: Embed a generated barcode image into an HTML img tag using a data URI from a MemoryStream.
// Tags: code128, barcode generation, data uri, png, aspose.barcode, memorystream, html

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates embedding a generated barcode image into an HTML img tag using a data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, converts it to a Base64 data URI, and writes the HTML img tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology
        var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);

        // Use a memory stream to hold the generated PNG image
        using (var ms = new MemoryStream())
        {
            // Save the barcode image to the stream in PNG format
            generator.Save(ms, BarCodeImageFormat.Png);

            // Convert the image bytes to a Base64 string
            string base64 = Convert.ToBase64String(ms.ToArray());

            // Build the data URI for embedding in HTML
            string dataUri = $"data:image/png;base64,{base64}";

            // Create the HTML img tag with the data URI as the source
            string html = $"<img src=\"{dataUri}\" alt=\"Barcode\" />";

            // Output the HTML string
            Console.WriteLine(html);
        }
    }
}