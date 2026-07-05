// Title: Async TIFF Barcode Generation with Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using an asynchronous FileStream.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: barcode, code128, async, tiff, aspose.barcode, fileio

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode and writes it to a TIFF file asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it as a TIFF image using async/await.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the barcode content and the output file path.
        string codeText = "1234567890";
        string outputPath = "barcode.tiff";

        // Ensure the directory for the output file exists.
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Open a FileStream with async support to write the TIFF image.
            using (var stream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Perform the save operation on a background thread to avoid blocking.
                await Task.Run(() => generator.Save(stream, BarCodeImageFormat.Tiff));

                // Ensure all buffered data is flushed to the file.
                await stream.FlushAsync();
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}