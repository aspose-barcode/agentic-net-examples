// Title: Generate Code128 Barcode and Encode as Base64
// Description: Creates a Code128 barcode, saves it to a MemoryStream as PNG, and converts the image bytes to a Base64 string for easy transport or embedding.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: code128, barcode generation, memorystream, base64, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, store it in a <see cref="MemoryStream"/>,
/// and convert the resulting image to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image directly into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes from the memory stream to a Base64-encoded string
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string to the console
                Console.WriteLine(base64String);
            }
        }
    }
}