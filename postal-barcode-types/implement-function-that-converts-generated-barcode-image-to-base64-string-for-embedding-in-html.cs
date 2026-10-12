// Title: Convert Barcode Image to Base64 for HTML Embedding
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, converting the PNG image to a Base64 string, and outputting an HTML <img> tag.
// Category-Description: This example belongs to the Aspose.BarCode image generation and encoding category. It shows how to use BarcodeGenerator (EncodeTypes) to create a barcode, save it to a MemoryStream, and then encode the binary data to Base64 for web embedding. Developers often need to embed barcodes directly in HTML emails or web pages without storing image files, making this pattern a common requirement.
// Prompt: Implement a function that converts a generated barcode image to a Base64 string for embedding in HTML.
// Tags: barcode, code128, base64, html, image, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and conversion to Base64 for HTML embedding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, converts it to Base64, and prints an HTML img tag.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        string codeText = "12345678";

        // Generate the barcode image and obtain its Base64 representation.
        string base64 = ConvertBarcodeToBase64(codeText);

        // Output an HTML <img> element with the Base64-encoded PNG data.
        Console.WriteLine("<img src=\"data:image/png;base64," + base64 + "\" />");
    }

    /// <summary>
    /// Generates a Code128 barcode for the supplied text, saves it as PNG to a memory stream,
    /// and returns the image data encoded as a Base64 string.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Base64 string of the generated PNG barcode image.</returns>
    static string ConvertBarcodeToBase64(string codeText)
    {
        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Use a memory stream to avoid writing to disk.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning before reading.
                ms.Position = 0;

                // Convert the stream contents to a byte array.
                byte[] imageBytes = ms.ToArray();

                // Encode the byte array to a Base64 string.
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}