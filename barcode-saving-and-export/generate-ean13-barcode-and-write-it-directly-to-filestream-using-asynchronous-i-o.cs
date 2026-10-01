// Title: Generate EAN13 Barcode and Save Asynchronously to File
// Description: Demonstrates creating an EAN13 barcode with Aspose.BarCode, storing it in a memory stream, and writing the PNG image to disk using async file I/O.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce product barcodes such as EAN13. Typical scenarios include generating printable barcodes for inventory, retail, or shipping labels and persisting them to image files. Developers often need to combine barcode creation with asynchronous file operations for scalable, non‑blocking applications.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, generation, async, filestream, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an EAN13 barcode and writes it to a file using asynchronous I/O.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates an EAN13 barcode image and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the barcode data (12 digits; checksum is added automatically).
        string codeText = "123456789012";

        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ean13.png");

        // Initialize the barcode generator for the EAN13 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            // Render the barcode into a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading.

                // Open a FileStream with async support to write the image to disk.
                using (var fileStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    // Asynchronously copy the image data from memory to the file.
                    await memoryStream.CopyToAsync(fileStream);
                }
            }
        }

        Console.WriteLine($"EAN13 barcode saved to: {outputPath}");
    }
}