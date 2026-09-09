// Title: Generate DotCode barcode and return as Base64 string
// Description: Demonstrates creating a DotCode barcode using Aspose.BarCode, encoding the PNG image to a Base64 string for client‑side rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DotCode to produce barcode images. Typical use cases include generating barcodes on the fly for web APIs, embedding them in HTML, or sending them to client applications as Base64 strings. Developers often need to convert barcode images to Base64 for seamless integration with front‑end frameworks without handling file I/O.
// Prompt: Expose an API that returns DotCode barcode as base64 string for client‑side rendering.
// Tags: dotcode, barcode, generation, base64, png, aspose.barcode, api

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides functionality to generate a DotCode barcode and output it as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DotCode barcode for a sample text and prints the Base64 string.
    /// </summary>
    static void Main()
    {
        // Sample text to encode in the barcode
        string sampleText = "Aspose";

        // Generate Base64 representation of the barcode image
        string base64 = GenerateDotCodeBase64(sampleText);

        // Output the result to the console
        Console.WriteLine("Base64 PNG of DotCode barcode:");
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a DotCode barcode image from the provided text and returns it as a Base64‑encoded PNG string.
    /// </summary>
    /// <param name="codeText">The text to encode in the DotCode barcode.</param>
    /// <returns>Base64 string representing the PNG image of the generated barcode.</returns>
    static string GenerateDotCodeBase64(string codeText)
    {
        // MemoryStream will hold the generated PNG image
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator with DotCode symbology and the input text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Save the barcode image directly to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the memory stream contents to a byte array
            byte[] imageBytes = ms.ToArray();

            // Encode the byte array to a Base64 string
            return Convert.ToBase64String(imageBytes);
        }
    }
}