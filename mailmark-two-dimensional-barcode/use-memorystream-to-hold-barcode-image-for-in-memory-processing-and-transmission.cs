// Title: Generate Code128 barcode and process it in-memory using MemoryStream
// Description: Demonstrates creating a Code128 barcode, saving it to a MemoryStream in PNG format, and accessing the image bytes for further processing or transmission.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator, set visual parameters, and save the barcode to a stream. Developers often need to generate barcodes on the fly and transmit them without writing to disk, such as in web APIs or email attachments. The snippet shows typical usage of EncodeTypes, BarCodeImageFormat, and stream handling for in‑memory operations.
// Prompt: Use a MemoryStream to hold the barcode image for in‑memory processing and transmission.
// Tags: code128, barcode generation, memorystream, png, in-memory processing, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in a MemoryStream,
/// and performing in‑memory processing such as size reporting and Base64 conversion.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, saves it to a MemoryStream,
    /// and outputs image size and Base64 representation.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure visual appearance: white background and black bars
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Create a MemoryStream to hold the generated PNG image in memory
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for reading
                memoryStream.Position = 0;

                // Extract the image bytes from the stream for further processing or transmission
                byte[] imageBytes = memoryStream.ToArray();

                // Display the size of the generated image in bytes
                Console.WriteLine($"Generated barcode image size: {imageBytes.Length} bytes");

                // Example of converting the image bytes to a Base64 string and displaying it
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine($"Base64: {base64}");
            }
        }
    }
}