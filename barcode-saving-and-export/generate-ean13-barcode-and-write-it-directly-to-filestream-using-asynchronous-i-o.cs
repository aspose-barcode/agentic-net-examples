// Title: Generate EAN13 barcode and save asynchronously to file
// Description: Creates an EAN13 barcode image and writes it directly to a PNG file using asynchronous file I/O.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator and related parameter classes to produce barcodes. Typical use cases include creating product labels, inventory tags, or any scenario requiring EAN13 symbology. Developers often need to generate barcode images and store them efficiently, leveraging asynchronous I/O for better performance in server or cloud environments.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, asynchronous, fileio, aspose.barcode, png, generation

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
    /// Asynchronously generates the barcode and writes it to disk.
    /// </summary>
    static async Task Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "ean13.png";

        // Initialize a BarcodeGenerator for the EAN13 symbology.
        // The provided 12‑digit string will have its checksum calculated automatically.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
        {
            // Suppress exceptions for minor code‑text inaccuracies.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Open a FileStream with asynchronous I/O enabled.
            using (var fileStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Save the barcode image directly to the stream (synchronous write to the stream).
                generator.Save(fileStream, BarCodeImageFormat.Png);

                // Flush any buffered data to the underlying file asynchronously.
                await fileStream.FlushAsync();
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}