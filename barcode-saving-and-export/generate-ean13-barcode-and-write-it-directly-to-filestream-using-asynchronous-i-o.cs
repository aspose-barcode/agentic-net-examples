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
    /// Entry point of the application. Generates an EAN13 barcode and writes it to a file asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file name (relative to the executable's directory).
        const string outputPath = "ean13.png";

        // EAN13 barcode requires exactly 12 digits; the checksum digit is added automatically.
        const string codeText = "123456789012";

        // Initialize the barcode generator with the EAN13 symbology and the provided code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeText))
        {
            // Create a FileStream configured for asynchronous writing.
            using (var fileStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Save the generated barcode image directly into the file stream as PNG.
                generator.Save(fileStream, BarCodeImageFormat.Png);

                // Flush any buffered data to the underlying file asynchronously.
                await fileStream.FlushAsync();
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"EAN13 barcode saved to '{outputPath}'.");
    }
}