// Title: Render Code128 barcode to Base64 string
// Description: Demonstrates generating a Code128 barcode, rendering it to a PNG image in memory, and converting the image to a Base64 string suitable for JSON responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to create barcodes, save them to a MemoryStream with BarCodeImageFormat, and obtain a Base64 representation for web APIs. Developers often need to embed barcode images directly in JSON payloads without writing files to disk.
// Prompt: Render barcode to a MemoryStream, convert the stream to a Base64 string for JSON API response.
// Tags: code128, barcode generation, png, base64, memorystream, aspose.barcode, aspose.drawing, json response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, encodes it as PNG in memory,
/// and returns the image as a Base64 string for inclusion in JSON responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the Base64 barcode string and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Generate the barcode and obtain its Base64 representation
        string base64Barcode = GenerateBarcodeBase64();

        // Output the Base64 string (e.g., to be captured by a calling process or API)
        Console.WriteLine(base64Barcode);
    }

    /// <summary>
    /// Creates a Code128 barcode, saves it as a PNG image into a memory stream,
    /// and converts the resulting byte array to a Base64 string.
    /// </summary>
    /// <returns>Base64-encoded PNG image of the generated barcode.</returns>
    static string GenerateBarcodeBase64()
    {
        // Initialize the barcode generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Prepare a memory stream to hold the PNG image data
            using (var memoryStream = new MemoryStream())
            {
                // Render the barcode into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes from the stream to a Base64 string
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}