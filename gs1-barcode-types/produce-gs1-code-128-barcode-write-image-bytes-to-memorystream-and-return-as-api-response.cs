// Title: Generate GS1 Code 128 Barcode and Return as Base64 API Response
// Description: Demonstrates creating a GS1 Code 128 barcode, writing the PNG image to a MemoryStream, and outputting the image as a Base64 string suitable for an API response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to produce a barcode image. Typical use cases include creating product identifiers for e‑commerce, inventory, or logistics systems where a GS1‑compliant barcode is required. Developers often need to render the barcode to a stream, adjust visual parameters, and return the image in a web API response.
// Prompt: Produce a GS1 Code 128 barcode, write image bytes to a MemoryStream, and return as an API response.
// Tags: gs1,code128,barcode,generation,memory stream,base64,api response,aspose.barcode,aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// writes the PNG image to a memory stream, and outputs a Base64
/// representation suitable for an API response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, encodes the image,
    /// and writes HTTP‑style headers followed by the Base64 image data to the console.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code 128 data: Application Identifier (01) with a 14‑digit GTIN
        string gs1CodeText = "(01)12345678901231";

        // Create a memory stream to hold the generated barcode image
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with GS1 Code 128 symbology and the data string
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
            {
                // Optional: adjust the module (X) size for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the barcode image as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            memoryStream.Position = 0;

            // Convert the image bytes to a Base64 string for transmission
            byte[] imageBytes = memoryStream.ToArray();
            string base64Image = Convert.ToBase64String(imageBytes);

            // Simulated API response: write HTTP headers and the Base64 payload
            Console.WriteLine("Content-Type: image/png");
            Console.WriteLine("Content-Transfer-Encoding: base64");
            Console.WriteLine();
            Console.WriteLine(base64Image);
        }
    }
}