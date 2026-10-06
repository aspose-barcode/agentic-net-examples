// Title: Generate DataMatrix barcode PNG via console simulation of REST endpoint
// Description: Demonstrates creating a DataMatrix barcode image from input text and saving it as a PNG file, then outputting the file path and Base64 representation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to produce raster images. Typical use cases include generating machine‑readable codes for inventory, shipping, or authentication systems, where developers need to output PNG files or embed them in web responses. The snippet shows setting module size, saving to PNG, and converting the image to Base64 for easy transmission.
// Prompt: Expose a REST endpoint that accepts text and returns a DataMatrix barcode image in PNG format.
// Tags: datamatrix, barcode generation, png, aspnet, aspnetcore, aspnet-webapi, aspose.barcode, image, base64

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Simulates a REST endpoint that generates a DataMatrix barcode PNG from supplied text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that processes command‑line arguments, creates the barcode, and outputs the result.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the barcode text.</param>
    static void Main(string[] args)
    {
        // Simulate receiving text from a REST request. Use a default if none provided.
        string inputText = args.Length > 0 ? args[0] : "Sample Text";

        // Determine a temporary file path for the generated PNG.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Create a BarcodeGenerator for DataMatrix with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, inputText))
        {
            // Optional: adjust the size of each module (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated barcode directly as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the caller where the image was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");

        // If the file was successfully created, read it and output a Base64 string.
        if (File.Exists(outputPath))
        {
            byte[] imageBytes = File.ReadAllBytes(outputPath);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }
        else
        {
            // Indicate failure to generate the image.
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}