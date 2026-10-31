// Title: Export MaxiCode barcode as Base64 string
// Description: Generates a MaxiCode barcode, saves it as a PNG image in memory, and converts the image to a Base64 string for embedding in JSON API responses.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class, focusing on MaxiCode symbology. It shows how to configure barcode parameters, render the barcode to a stream, and transform the binary image data into a Base64-encoded string—common tasks when delivering barcodes via web services or JSON payloads. Developers working with barcode creation, image handling, and API integration will find this pattern useful.
// Prompt: Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses.
// Tags: maxicode, barcode, generation, base64, json, aspose.barcode, imageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode and export it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, encodes it to Base64, and writes the result to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Text to encode in the MaxiCode barcode
        string codeText = "Sample MaxiCode";

        // Initialize the barcode generator with MaxiCode symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure MaxiCode specific parameters
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4; // Use Mode 4 (arbitrary text mode)
            generator.Parameters.Barcode.XDimension.Pixels = 15f;           // Set module size (pixel dimension)

            // Render the barcode to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the PNG byte array to a Base64 string
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string (suitable for embedding in JSON)
                Console.WriteLine(base64);
            }
        }
    }
}