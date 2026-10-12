// Title: Generate GS1 Code 128 Barcode and Return as Base64 via MemoryStream
// Description: Demonstrates creating a GS1 Code 128 barcode with Aspose.BarCode, saving it to a PNG image in a MemoryStream, and converting the bytes to a Base64 string for API responses.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128, configure barcode parameters, and output the image in common formats such as PNG. Developers often need to generate barcodes on the fly for web APIs, embed them in documents, or return them as byte streams. The snippet shows typical steps: instantiate the generator, adjust visual settings, save to a stream, and retrieve the byte array.
// Prompt: Produce a GS1 Code 128 barcode, write image bytes to a MemoryStream, and return as an API response.
// Tags: gs1code128, barcode generation, png, memorystream, aspose.barcode, aspose.drawing, base64

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 Code 128 barcode,
/// writes the PNG image to a <see cref="MemoryStream"/>,
/// and outputs the image as a Base64 string (simulating an API response).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, saves it to a memory stream,
    /// and writes the Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Code 128 data to encode.
        string codeText = "(02)04006664241007(37)1";

        // Initialize the barcode generator with the GS1 Code 128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Optional: set the module (X) dimension to control image size.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Retrieve the raw image bytes from the stream.
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the image bytes to a Base64 string (typical for API payloads).
                string base64 = Convert.ToBase64String(imageBytes);

                // Output the Base64 string to the console.
                Console.WriteLine(base64);
            }
        }
    }
}