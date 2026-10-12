// Title: Render Barcode to Base64 String via MemoryStream
// Description: Generates a Code128 barcode, saves it as a PNG image in a MemoryStream, and converts the image data to a Base64 string for inclusion in JSON API responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use BarcodeGenerator to create barcodes, save them directly to streams, and transform the binary output for web services. Key API classes include BarcodeGenerator, BarCodeImageFormat, and EncodeTypes. Typical use cases involve returning barcode images from REST endpoints without writing temporary files.
// Prompt: Render barcode to a MemoryStream, convert the stream to a Base64 string for JSON API response.
// Tags: barcode, code128, base64, memorystream, json, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates rendering a barcode to a MemoryStream and converting it to a Base64 string for JSON responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it as PNG to a MemoryStream,
    /// converts the stream to a Base64 string, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "12345678";

        // Choose the barcode symbology (Code128 in this case).
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a BarcodeGenerator with the specified symbology and text.
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Prepare a memory stream to hold the generated PNG image.
            using (MemoryStream stream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(stream, BarCodeImageFormat.Png);

                // Convert the stream's byte array to a Base64 string for JSON transport.
                string base64 = Convert.ToBase64String(stream.ToArray());

                // Output the Base64 string (e.g., to be captured by a calling service).
                Console.WriteLine(base64);
            }
        }
    }
}