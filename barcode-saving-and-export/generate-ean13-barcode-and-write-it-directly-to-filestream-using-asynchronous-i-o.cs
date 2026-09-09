// Title: Generate and save an EAN13 barcode using async file I/O
// Description: Demonstrates creating an EAN13 barcode with Aspose.BarCode and writing the PNG image directly to a file using asynchronous streams.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set barcode parameters, and employ async I/O for efficient file output. Developers often need to generate barcodes on the fly and store them without blocking the thread, especially in web or service applications.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, generation, async, filestream, aspose.barcode, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an EAN13 barcode and saving it asynchronously to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, writes it to a memory stream, then copies it asynchronously to a file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "ean13.png";

        // Create a BarcodeGenerator for the EAN13 symbology with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Adjust the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Render the barcode into a memory stream in PNG format.
            using (var memory = new MemoryStream())
            {
                generator.Save(memory, BarCodeImageFormat.Png);
                // Reset the stream position to the beginning before reading.
                memory.Position = 0;

                // Open a file stream with asynchronous support to write the image.
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                {
                    // Asynchronously copy the PNG data from memory to the file.
                    await memory.CopyToAsync(fileStream);
                }
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}