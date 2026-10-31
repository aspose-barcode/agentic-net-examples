// Title: Render Barcode to Base64 String for JSON API Response
// Description: Generates a Code128 barcode, saves it to a memory stream as PNG, and converts the image to a Base64 string suitable for inclusion in a JSON API response.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create barcode images in memory. Typical use cases include generating barcodes on-the-fly for web services, embedding them in JSON payloads, or returning them to client applications without writing to disk. Developers often need to render barcodes to streams, convert them to Base64, and integrate with REST APIs.
// Prompt: Render barcode to a MemoryStream, convert the stream to a Base64 string for JSON API response.
// Tags: code128, barcode generation, base64, memorystream, json, aspnet, aspose.barcode, image format png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates rendering a barcode to a memory stream and converting it to a Base64 string for JSON responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, encodes it as PNG in memory, and outputs the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "12345678";

        // Initialize the barcode generator with the desired symbology (Code128) and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Create a memory stream to hold the generated PNG image.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64 string.
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Write the Base64 string to the console (simulating a JSON API response payload).
                Console.WriteLine(base64);
            }
        }
    }
}