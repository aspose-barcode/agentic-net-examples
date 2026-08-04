// Title: Generate GS1 Code 128 Barcode and Return as Base64 String
// Description: Creates a GS1 Code 128 barcode image, writes it to a memory stream, and outputs the image as a Base64‑encoded string suitable for API responses.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It demonstrates using the BarcodeGenerator class with EncodeTypes.GS1Code128 to produce a barcode, saving it to a MemoryStream in PNG format, and retrieving the raw bytes. Developers often need to generate barcodes on‑the‑fly for web APIs, e‑commerce platforms, or inventory systems, and this pattern shows the typical workflow for image‑based barcode output.
// Prompt: Produce a GS1 Code 128 barcode, write image bytes to a MemoryStream, and return as an API response.
// Tags: gs1, code128, barcode, generation, memory stream, base64, api, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode and returning it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, writes it to a memory stream, and prints the Base64 image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 codetext: AI (01) with a 14‑digit GTIN
        const string gs1CodeText = "(01)01234567890123";

        // Initialize the barcode generator with GS1 Code 128 symbology and the sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Create a memory stream to hold the generated image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for reading
                ms.Position = 0;

                // Convert the image bytes to a Base64 string (simulating an API response)
                string base64Image = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string to the console
                Console.WriteLine(base64Image);
            }
        }
    }
}