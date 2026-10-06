// Title: Generate DotCode barcode and return as Base64 string
// Description: Demonstrates how to create a DotCode barcode using Aspose.BarCode, encode it as PNG, and return the image as a Base64 string for client‑side rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes. Typical scenarios include web services that need to supply barcode images to browsers without storing files, enabling client‑side rendering via data URIs. Developers often need quick, in‑memory barcode creation and conversion to common formats such as PNG or Base64.
// Prompt: Expose an API that returns DotCode barcode as base64 string for client‑side rendering.
// Tags: dotcode, barcode, generation, base64, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a DotCode barcode and returning it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode for a sample text and writes the Base64 string to console.
    /// </summary>
    static void Main()
    {
        // Sample data to encode in the barcode
        string codeText = "Aspose";

        // Generate the Base64 representation of the DotCode barcode
        string base64 = GenerateDotCodeBase64(codeText);

        // Output the Base64 string (can be used as a data URI on the client side)
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a DotCode barcode image in PNG format and returns it as a Base64 string.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Base64‑encoded PNG image of the generated barcode.</returns>
    static string GenerateDotCodeBase64(string codeText)
    {
        // Initialize the barcode generator with DotCode symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Use a memory stream to avoid writing to disk
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the stream contents to a byte array
                byte[] imageBytes = ms.ToArray();

                // Encode the byte array to a Base64 string
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}