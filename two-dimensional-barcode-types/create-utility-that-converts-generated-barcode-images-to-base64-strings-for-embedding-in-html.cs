// Title: Generate Code128 barcode and embed as Base64 image in HTML
// Description: Demonstrates creating a Code128 barcode, converting the PNG image to a Base64 string, and outputting an HTML <img> tag for embedding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images programmatically. Typical use cases include creating barcodes for web pages, emails, or reports where the image must be embedded directly in HTML without separate file storage. Developers often need to convert generated images to Base64 strings for inline display, and this snippet shows the complete workflow.
// Prompt: Create a utility that converts generated barcode images to Base64 strings for embedding in HTML.
// Tags: barcode, code128, base64, html, image, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and conversion to a Base64-encoded HTML image tag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, encodes it as Base64, and prints an HTML <img> tag.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "Sample123";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array
                byte[] imageBytes = memoryStream.ToArray();

                // Encode the byte array to a Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                // Build an HTML <img> tag with the Base64-encoded image data
                string imgTag = $"<img src=\"data:image/png;base64,{base64String}\" alt=\"barcode\"/>";

                // Output the HTML tag to the console
                Console.WriteLine(imgTag);
            }
        }
    }
}