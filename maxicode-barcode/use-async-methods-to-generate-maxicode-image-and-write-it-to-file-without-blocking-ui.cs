// Title: Asynchronous MaxiCode barcode generation and file saving
// Description: Demonstrates generating a MaxiCode barcode image using Aspose.BarCode and saving it asynchronously to avoid UI blocking.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator with EncodeTypes.MaxiCode, configure barcode parameters, and employ async file I/O. Developers often need to create barcode images in background tasks for responsive applications, and this snippet illustrates the key classes (BarcodeGenerator, BarCodeImageFormat) and async stream handling.
// Prompt: Use async methods to generate a MaxiCode image and write it to a file without blocking the UI.
// Tags: maxicode, barcode generation, async, file io, aspose.barcode, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous generation of a MaxiCode barcode image and saving it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file path
        string outputPath = "maxicode.png";

        // Generate the MaxiCode image and save it asynchronously
        await GenerateMaxiCodeAsync(outputPath);

        // Inform the user that the operation completed
        Console.WriteLine($"MaxiCode image saved to {outputPath}");
    }

    /// <summary>
    /// Generates a MaxiCode barcode, renders it to a memory stream, and writes the image to disk using async I/O.
    /// </summary>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    private static async Task GenerateMaxiCodeAsync(string outputPath)
    {
        // Initialize the barcode generator for MaxiCode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Configure visual appearance: set module size and bar color
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Render the barcode into a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading

                // Asynchronously copy the memory stream to a file stream
                using (var fileStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await memoryStream.CopyToAsync(fileStream);
                }
            }
        }
    }
}