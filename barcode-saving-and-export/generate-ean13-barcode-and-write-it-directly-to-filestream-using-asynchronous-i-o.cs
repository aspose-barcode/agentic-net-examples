// Title: Generate EAN13 Barcode and Save Asynchronously to FileStream
// Description: This example creates an EAN‑13 barcode image and writes it directly to a file using asynchronous I/O.
// Category-Description: Demonstrates Aspose.BarCode generation of a specific symbology (EAN13) and saving the result to a stream. It highlights the use of BarcodeGenerator, setting barcode parameters, and leveraging async file operations. Ideal for developers needing high‑performance barcode creation in web services, batch processing, or any scenario where non‑blocking I/O is required.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, async, filestream, aspose.barcode, generation, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates an EAN13 barcode and saves it asynchronously to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates an EAN13 barcode image and writes it to a file using a FileStream.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "ean13.png");

        // Create an asynchronous FileStream for writing the barcode image.
        using (FileStream fileStream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true))
        {
            // Initialize the barcode generator with EAN13 symbology and the data to encode.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                // Adjust the X‑dimension (module width) of the barcode in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Save the generated barcode directly to the asynchronous FileStream in PNG format.
                generator.Save(fileStream, BarCodeImageFormat.Png);
            }

            // Ensure all buffered data is flushed to the underlying file.
            await fileStream.FlushAsync();
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}