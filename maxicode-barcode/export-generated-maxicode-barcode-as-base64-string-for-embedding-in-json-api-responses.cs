// Title: Export MaxiCode barcode as Base64 string
// Description: Generates a MaxiCode barcode, encodes it to PNG, and returns the image as a Base64 string suitable for embedding in JSON responses.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It demonstrates how to use the BarcodeGenerator class with EncodeTypes.MaxiCode, configure barcode parameters, save the barcode to a memory stream in PNG format, and convert the resulting byte array to a Base64 string. Developers often need to embed barcode images directly in JSON payloads for web APIs, mobile apps, or other client‑side integrations, making this pattern a common requirement.
// Prompt: Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses.
// Tags: maxicode, barcode generation, base64, json, aspose.barcode, image export, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and converting it to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it to a memory stream, and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the MaxiCode barcode (arbitrary text mode)
        string codetext = "Sample MaxiCode";

        // Initialize the barcode generator for MaxiCode with the specified codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Configure MaxiCode mode to Mode4 (arbitrary text mode)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Set the module size (pixel dimension) for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Create a memory stream to hold the generated PNG image
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string (e.g., to be included in a JSON API response)
                Console.WriteLine(base64);
            }
        }
    }
}