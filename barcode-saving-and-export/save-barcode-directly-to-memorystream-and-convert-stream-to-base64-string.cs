// Title: Save barcode to MemoryStream and get Base64 string
// Description: Demonstrates generating a Code128 barcode, saving it directly into a MemoryStream, and converting the image data to a Base64-encoded string for easy transport or embedding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes to create barcodes, save them in various image formats via BarCodeImageFormat, and handle the output in memory. Developers often need to embed barcode images in HTML, JSON, or other text-based payloads, so converting the image stream to Base64 is a common requirement. The snippet shows the typical workflow for in‑memory barcode creation without writing to disk.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: code128, barcode generation, memorystream, base64, aspnet, aspose.barcode, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and converting the image to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and outputs its Base64 representation.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        string codeText = "12345678";

        // Create a memory stream to hold the generated barcode image.
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode image directly into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes from the memory stream to a Base64 string.
            string base64 = Convert.ToBase64String(ms.ToArray());

            // Output the Base64 string to the console.
            Console.WriteLine(base64);
        }
    }
}