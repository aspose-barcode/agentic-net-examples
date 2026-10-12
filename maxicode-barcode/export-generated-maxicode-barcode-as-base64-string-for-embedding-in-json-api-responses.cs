// Title: Export MaxiCode barcode as Base64 string
// Description: Demonstrates generating a MaxiCode barcode, converting it to PNG, and encoding the image as a Base64 string for inclusion in JSON responses.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.MaxiCode, configure barcode parameters such as XDimension and MaxiCode mode, and output the result in a web‑friendly format. Developers creating APIs, mobile apps, or web services often need to embed barcode images directly in JSON payloads, and this pattern illustrates the typical workflow using Aspose.BarCode classes like BarcodeGenerator, BarCodeImageFormat, and related parameter objects.
// Prompt: Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses.
// Tags: maxicode, barcode generation, base64, json, aspose.barcode, image encoding, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a MaxiCode barcode, encodes the PNG image to Base64, and writes the string to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode barcode, saves it to a memory stream,
    /// converts the image bytes to a Base64 string, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the MaxiCode barcode (Mode 4 supports arbitrary text)
        string codetext = "Sample MaxiCode";

        // Initialize the barcode generator with MaxiCode symbology and the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codetext))
        {
            // Configure the size of each module (pixel) in the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Set the MaxiCode mode to 4, which allows encoding of arbitrary text
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Create a memory stream to hold the generated PNG image
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Write the Base64 string to the console (suitable for embedding in JSON)
                Console.WriteLine(base64);
            }
        }
    }
}