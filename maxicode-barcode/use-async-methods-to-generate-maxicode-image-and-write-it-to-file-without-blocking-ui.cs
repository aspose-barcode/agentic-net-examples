// Title: Generate MaxiCode barcode image asynchronously
// Description: Demonstrates creating a MaxiCode barcode with Aspose.BarCode and saving it as a PNG file using async I/O to avoid UI blocking.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include generating shipping labels, inventory tags, or any application requiring MaxiCode symbology. Developers often need non‑blocking image creation for responsive UI or high‑throughput services.
// Prompt: Use async methods to generate a MaxiCode image and write it to a file without blocking the UI.
// Tags: maxicode, barcode, generation, async, png, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous generation of a MaxiCode barcode image and saving it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronous entry point that determines the output path, generates the barcode, and reports the result.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify the output file path.</param>
    static async Task Main(string[] args)
    {
        // Determine the output file path: use the first argument if provided, otherwise default to the current directory.
        string outputPath = args.Length > 0
            ? args[0]
            : Path.Combine(Directory.GetCurrentDirectory(), "MaxiCode.png");

        try
        {
            // Generate the MaxiCode barcode and save it asynchronously.
            await GenerateMaxiCodeAsync(outputPath);
            Console.WriteLine($"MaxiCode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode, writes it to a memory stream, and then copies it to a file using asynchronous I/O.
    /// </summary>
    /// <param name="filePath">The full path where the PNG image will be saved.</param>
    private static async Task GenerateMaxiCodeAsync(string filePath)
    {
        // Simple code text for demonstration.
        const string codeText = "Sample MaxiCode";

        // Initialize the barcode generator with MaxiCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 15f;          // Set module size.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1f;      // Optional: enforce a square aspect ratio.

            // Save the generated barcode to a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset stream position for reading.

                // Asynchronously copy the memory stream to the target file.
                using (var fileStream = new FileStream(
                    filePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await memoryStream.CopyToAsync(fileStream);
                }
            }
        }
    }
}