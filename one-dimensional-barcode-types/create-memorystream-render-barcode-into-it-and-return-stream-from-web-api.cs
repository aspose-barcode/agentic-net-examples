// Title: Generate Code128 barcode into a MemoryStream and output as Base64
// Description: Demonstrates creating a Code128 barcode with Aspose.BarCode, saving it as a PNG image into a MemoryStream, and displaying the stream length and Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images in memory. Typical use cases include web APIs that need to return barcode images without writing to disk. Developers often need to stream barcode data directly to HTTP responses or other services, and this pattern shows the essential steps.
// Prompt: Create a MemoryStream, render the barcode into it, and return the stream from a web API.
// Tags: barcode symbology, generation, png, memorystream, aspnet, aspose.barcode, code128, base64

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, stores it in a MemoryStream,
/// and prints the stream length and Base64 representation to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode stream and writes diagnostic information.
    /// </summary>
    static void Main()
    {
        // Generate the barcode image and obtain it as a MemoryStream.
        using (MemoryStream barcodeStream = GenerateBarcodeStream())
        {
            // Output the length of the generated stream for verification.
            Console.WriteLine($"Generated barcode stream length: {barcodeStream.Length}");

            // Convert the stream's byte array to a Base64 string for easy transport or display.
            string base64 = Convert.ToBase64String(barcodeStream.ToArray());
            Console.WriteLine($"Base64: {base64}");
        }
    }

    /// <summary>
    /// Creates a BarcodeGenerator for Code128, renders the barcode as a PNG image,
    /// saves it into a MemoryStream, and returns the stream positioned at the start.
    /// </summary>
    /// <returns>A MemoryStream containing the PNG barcode image.</returns>
    static MemoryStream GenerateBarcodeStream()
    {
        // Initialize a memory stream to hold the barcode image.
        MemoryStream ms = new MemoryStream();

        // Configure the generator with the desired symbology and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Save the generated barcode into the memory stream in PNG format.
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset the stream position to the beginning so it can be read from the start.
        ms.Position = 0;
        return ms;
    }
}