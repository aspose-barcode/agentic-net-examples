// Title: Convert generated barcode image to Base64 string for HTML embedding
// Description: Demonstrates creating a barcode with Aspose.BarCode, saving it to a memory stream, and converting the image to a Base64 string that can be embedded directly in HTML.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to produce barcode images programmatically. Typical use cases include generating barcodes for web pages, emails, or reports where the image must be embedded as a data URI. Developers often need to convert the image to Base64 to avoid separate file handling.
// Prompt: Implement a function that converts a generated barcode image to a Base64 string for embedding in HTML.
// Tags: barcode, code128, base64, image, html, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates converting a generated barcode image to a Base64 string for embedding in HTML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, converts it to Base64, and writes the result to console.
    /// </summary>
    static void Main()
    {
        // Define the barcode text and symbology to use
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Generate the Base64 representation of the barcode image
        string base64 = ConvertBarcodeToBase64(codeText, encodeType);

        // Output the Base64 string (can be used in an <img src="data:image/png;base64,..."> tag)
        Console.WriteLine("Base64 Barcode Image:");
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a barcode image using the specified text and symbology, then returns the image as a Base64 string.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology type.</param>
    /// <returns>Base64-encoded PNG image of the generated barcode.</returns>
    static string ConvertBarcodeToBase64(string codeText, BaseEncodeType encodeType)
    {
        // Create a BarcodeGenerator with the desired symbology and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the barcode image to a memory stream in PNG format
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                // Convert the stream's bytes to a Base64 string
                byte[] imageBytes = stream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}