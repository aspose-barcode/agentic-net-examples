// Title: Generate Code128 barcode and output as Base64 string
// Description: Demonstrates creating a Code128 barcode, saving it to a MemoryStream, and converting the image data to a Base64-encoded string for easy transport or embedding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce barcode images in memory. Typical use cases include embedding barcodes in web pages, JSON payloads, or email bodies without writing files to disk. Developers often need to convert the image stream to Base64 for HTML img src or API responses.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: code128, barcode generation, memorystream, base64, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation and conversion to Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, writes it to a memory stream, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "12345678";

        // Create a memory stream to hold the barcode image
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the generated barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading
            ms.Position = 0;

            // Convert the image bytes in the stream to a Base64 string
            string base64 = Convert.ToBase64String(ms.ToArray());

            // Output the Base64 string to the console
            Console.WriteLine(base64);
        }
    }
}