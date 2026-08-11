// Title: Render Code128 barcode to Base64 string using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, rendering it to a PNG image in memory, and converting the image to a Base64 string suitable for inclusion in a JSON API response.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create barcodes, render them to streams, and obtain binary data. Typical use cases include server‑side barcode creation for web services, mobile apps, or document automation where the image must be transmitted as text (e.g., Base64) in JSON payloads. Developers often need to embed barcodes directly into API responses without writing temporary files.
// Prompt: Render barcode to a MemoryStream, convert the stream to a Base64 string for JSON API response.
// Tags: code128, barcode, generation, base64, json, memorystream, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, saves it to a memory stream as PNG,
/// and outputs the image as a Base64 string for use in JSON responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a BarcodeGenerator, encodes text, saves to MemoryStream,
    /// converts to Base64, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "123ABC";

            // Render the barcode to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string (simulating JSON API response)
                Console.WriteLine(base64);
            }
        }
    }
}