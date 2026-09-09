// Title: Generate QR Code and Save Asynchronously to PNG
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, adjusting its module size, and writing the PNG image to disk using async I/O to avoid blocking the thread.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbology, configure visual parameters via the Parameters property, and persist the result in PNG format. Developers often need to generate barcodes on‑the‑fly and write them to storage without blocking, making async file streams a common pattern in high‑throughput or UI‑responsive applications.
// Prompt: Generate QR Code barcode and use async method to write image file without blocking thread.
// Tags: qr, barcode, generation, async, png, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it asynchronously as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code and writes it to disk without blocking the calling thread.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Optional: adjust the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Render the barcode into a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading.

                // Asynchronously copy the memory stream to a file stream.
                await using (var fileStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 81920,
                    useAsync: true))
                {
                    await memoryStream.CopyToAsync(fileStream);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}