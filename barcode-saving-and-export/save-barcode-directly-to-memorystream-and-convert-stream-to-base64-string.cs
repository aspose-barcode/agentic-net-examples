// Title: Save Barcode to MemoryStream and Convert to Base64 String
// Description: Demonstrates generating a barcode, saving it directly into a MemoryStream as PNG, and converting the image data to a Base64-encoded string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with BarCodeImageFormat to create barcode images, store them in streams, and transform them into Base64 strings for embedding in web pages, JSON payloads, or other text-based formats. Developers frequently need to generate barcodes on the fly without writing temporary files, and this pattern shows the typical workflow for such scenarios.
// Prompt: Save a barcode directly to a MemoryStream and convert the stream to a Base64 string.
// Tags: barcode, code128, memorystream, base64, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode, saves it to a memory stream as PNG, and outputs the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, writes it to a <see cref="MemoryStream"/>, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology and the data to encode
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123456";

        // Initialize the barcode generator with the chosen type and text
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Create a memory stream to hold the generated image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image directly into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading
                memoryStream.Position = 0;

                // Convert the raw image bytes from the stream to a Base64 string
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Write the Base64 string to the console (can be used in HTML <img> tags, etc.)
                Console.WriteLine("Base64 Barcode Image:");
                Console.WriteLine(base64String);
            }
        }
    }
}