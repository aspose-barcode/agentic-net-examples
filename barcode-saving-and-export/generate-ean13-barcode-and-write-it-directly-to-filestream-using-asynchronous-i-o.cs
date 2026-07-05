// Title: Generate EAN13 barcode and save asynchronously to file
// Description: Demonstrates creating an EAN13 barcode with Aspose.BarCode and writing the PNG image directly to a FileStream using async I/O.
// Prompt: Generate an EAN13 barcode and write it directly to a FileStream using asynchronous I/O.
// Tags: ean13, barcode, async, filestream, aspose.barcode, png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates an EAN13 barcode and writes it to a PNG file using asynchronous I/O.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates an EAN13 barcode image and saves it to a file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated PNG image
        const string outputPath = "ean13.png";

        // EAN13 barcode requires exactly 12 numeric characters; the checksum digit is added automatically
        const string codeText = "123456789012";

        // Initialize the barcode generator for the EAN13 symbology with the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            // Configure the human‑readable text to appear below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Open a FileStream for writing the image; enable asynchronous operations
            using (var fileStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Save the generated barcode directly to the stream in PNG format
                generator.Save(fileStream, BarCodeImageFormat.Png);

                // Flush any buffered data to the underlying file asynchronously
                await fileStream.FlushAsync();
            }
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine($"EAN13 barcode saved to {outputPath}");
    }
}