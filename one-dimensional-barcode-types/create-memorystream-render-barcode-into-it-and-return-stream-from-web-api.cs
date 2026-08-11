// Title: Generate a Code128 barcode and return it as a MemoryStream
// Description: Demonstrates creating a Code128 barcode image in PNG format using Aspose.BarCode, storing it in a MemoryStream, and returning the stream for use in a web API response.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use the BarcodeGenerator class to encode data, save the result to a stream, and manage stream positioning. Developers building web services or APIs often need to produce barcode images on‑the‑fly without writing temporary files, and this pattern shows the typical workflow with key classes such as BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and MemoryStream.
// Prompt: Create a MemoryStream, render the barcode into it, and return the stream from a web API.
// Tags: code128, barcode generation, png, memorystream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simulated API class that provides barcode generation functionality.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image, writes it to a <see cref="MemoryStream"/> in PNG format,
    /// and returns the stream positioned at the beginning for reading.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the barcode image.</returns>
    static MemoryStream GetBarcodeStream(string codeText)
    {
        // Allocate a memory stream that will hold the generated PNG image.
        var barcodeStream = new MemoryStream();

        // Create a BarcodeGenerator for Code128 symbology with the supplied text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Persist the barcode image directly into the memory stream.
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
        }

        // Rewind the stream so callers can read from the start.
        barcodeStream.Position = 0;
        return barcodeStream;
    }

    /// <summary>
    /// Demonstrates the use of <see cref="GetBarcodeStream"/> and optionally writes the image to a file.
    /// In a real web API the returned stream would be sent as the HTTP response body.
    /// </summary>
    static void Main()
    {
        // Generate a barcode for the sample text "123ABC".
        using (MemoryStream stream = GetBarcodeStream("123ABC"))
        {
            // Output the size of the generated image for verification.
            Console.WriteLine($"Generated barcode image size: {stream.Length} bytes");

            // Optional: save the stream to a physical file to inspect the result.
            const string outputPath = "barcode.png";
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(file);
            }

            Console.WriteLine($"Barcode image saved to {outputPath}");
        }

        // Note: In production, the MemoryStream would be returned directly from a controller action.
    }
}