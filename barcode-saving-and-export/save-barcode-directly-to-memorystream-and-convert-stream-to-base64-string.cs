// Title: Save barcode to MemoryStream and get Base64 string
// Description: Demonstrates generating a Code128 barcode, saving it directly into a MemoryStream, and converting the image data to a Base64-encoded string for easy transport or embedding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to produce barcode images in memory. Typical use cases include creating barcodes for web APIs, embedding in JSON, or storing in databases without writing to disk. Developers often need to convert the image to Base64 for HTML img tags or data URIs.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: barcode, code128, generation, memorystream, base64, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a MemoryStream,
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

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // Convert the image bytes in the stream to a Base64 string.
            string base64 = Convert.ToBase64String(ms.ToArray());

            // Output the Base64 string to the console.
            Console.WriteLine(base64);
        }
    }
}