// Title: Generate Code128 barcode and save as PNG (ASP.NET stream example)
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, writing it to a MemoryStream, and saving the image to a file, which in ASP.NET would be sent directly to Response.OutputStream for immediate download.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create barcode images. Typical use cases include creating barcodes for inventory, shipping labels, or ticketing systems, where developers need to produce images on the fly and deliver them via web responses. The snippet shows the common pattern of generating to a stream, resetting the position, and copying to a destination, a workflow frequently required in ASP.NET applications.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: code128, barcode generation, png, aspnet, memorystream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode, writes it to a memory stream,
/// and saves the image to a file. In an ASP.NET context the same stream would be
/// written to Response.OutputStream for immediate download by the client.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path for demonstration purposes.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // The text to encode in the barcode.
        string codeText = "12345678";

        // Create a memory stream to hold the generated barcode image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with Code128 symbology and the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Write the memory stream contents to a file (simulating Response.OutputStream in ASP.NET).
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated and saved to: {outputPath}");
    }
}