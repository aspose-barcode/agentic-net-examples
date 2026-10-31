// Title: Generate Barcode Image and Convert to Base64 String
// Description: Demonstrates creating a barcode with Aspose.BarCode and encoding the resulting PNG image to a Base64 string for embedding in HTML.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce barcode images and then transform them into Base64 strings. Typical use cases include embedding barcodes directly into web pages or emails without storing separate image files. Developers often need to convert generated images to text‑based formats for seamless integration with HTML, CSS, or JSON payloads.
/// Prompt: Implement a function that converts a generated barcode image to a Base64 string for embedding in HTML.
/// Tags: barcode symbology, generation, base64, image, html, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to generate a barcode image and return it as a Base64‑encoded string.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode using the specified text and encoding type, saves it to a memory stream as PNG,
    /// and returns the image data encoded as a Base64 string.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
    /// <returns>A Base64‑encoded representation of the PNG barcode image.</returns>
    static string GenerateBarcodeBase64(string codeText, BaseEncodeType encodeType)
    {
        // Create a memory stream to hold the generated image.
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator with the desired symbology and text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the stream contents to a byte array.
            byte[] imageBytes = ms.ToArray();

            // Encode the byte array to a Base64 string.
            return Convert.ToBase64String(imageBytes);
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a Code128 barcode for a sample code and prints the Base64 string.
    /// </summary>
    static void Main()
    {
        // Sample barcode data.
        string code = "12345678";

        // Generate the Base64 string for the barcode image.
        string base64 = GenerateBarcodeBase64(code, EncodeTypes.Code128);

        // Output the result to the console.
        Console.WriteLine("Base64 Barcode Image:");
        Console.WriteLine(base64);
    }
}