// Title: Generate EAN13 barcode and save asynchronously to file
// Description: Demonstrates creating an EAN‑13 barcode with Aspose.BarCode and writing the PNG image directly to a FileStream using async I/O.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator (EncodeTypes) and BarCodeImageFormat to produce barcode images. Typical use cases include generating barcodes for inventory, shipping, or retail systems and saving them efficiently to storage. Developers often need asynchronous file operations to avoid blocking threads in high‑throughput applications.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, generation, async, filestream, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation and saving of an EAN‑13 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes it to disk asynchronously.
    /// </summary>
    static async Task Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ean13.png");

        try
        {
            // Generate the barcode image and save it asynchronously.
            await GenerateEan13Async(outputPath);
            Console.WriteLine($"EAN13 barcode saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation or saving.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates an EAN‑13 barcode, writes it to a memory stream, then copies it asynchronously to a file stream.
    /// </summary>
    /// <param name="outputPath">Full path where the PNG image will be saved.</param>
    static async Task GenerateEan13Async(string outputPath)
    {
        // Create a barcode generator for EAN13 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better resolution.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Render the barcode to a memory stream in PNG format.
            using (var memory = new MemoryStream())
            {
                generator.Save(memory, BarCodeImageFormat.Png);
                memory.Position = 0; // Reset stream position before copying.

                // Open a file stream with asynchronous support.
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                {
                    // Asynchronously copy the image data to the file.
                    await memory.CopyToAsync(fileStream);
                }
            }
        }
    }
}