// Title: Barcode to Base64 Converter Example
// Description: Demonstrates generating a Code128 barcode image with Aspose.BarCode and converting it to a Base64 string for embedding directly into HTML.
// Category-Description: This example belongs to the Aspose.BarCode image generation and encoding category. It shows how to use BarcodeGenerator, BarCodeImageFormat, and .NET streams to create barcode graphics, then encode the binary image data to Base64. Developers often need this pattern when embedding barcodes in web pages, emails, or JSON payloads without writing files to disk.
// Prompt: Create a utility that converts generated barcode images to Base64 strings for embedding in HTML.
// Tags: barcode, code128, base64, html, image, aspnet, aspose.barcode, generation, encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple console utility that generates a barcode image and returns it as a Base64 string for HTML embedding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode for a sample value and writes an HTML <img> tag with the Base64 data URI.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Text to be encoded in the barcode
        string codeText = "12345678";

        // Generate a Base64 representation of the barcode image
        string base64 = GenerateBarcodeBase64(EncodeTypes.Code128, codeText);

        // Output an HTML <img> element that embeds the barcode via a data URI
        Console.WriteLine($"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />");
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text, then returns the image as a Base64 string.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <returns>Base64‑encoded PNG image data.</returns>
    static string GenerateBarcodeBase64(BaseEncodeType encodeType, string codeText)
    {
        // Create a BarcodeGenerator with the desired symbology and data
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use a memory stream to hold the generated image in memory
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array
                byte[] bytes = ms.ToArray();

                // Encode the byte array to a Base64 string
                return Convert.ToBase64String(bytes);
            }
        }
    }
}