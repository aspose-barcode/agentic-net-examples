// Title: Asynchronous TIFF Barcode Generation to FileStream
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using async/await with a FileStream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to create barcode images. Typical use cases include creating printable barcode files in various formats, such as TIFF, for integration into document workflows. Developers often need to generate barcodes asynchronously to avoid blocking I/O operations.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: code128, barcode generation, tiff, async, filestream, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation and saving of a Code128 barcode as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode and saves it asynchronously to a TIFF file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.tiff";

        // Ensure the target directory exists; create it if necessary.
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Open a FileStream configured for asynchronous writing.
        using (FileStream stream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true))
        {
            // Initialize the barcode generator with Code128 symbology and sample data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the barcode as a TIFF image to the stream asynchronously.
                await Task.Run(() => generator.Save(stream, BarCodeImageFormat.Tiff));
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}