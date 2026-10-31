// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating a barcode image using Aspose.BarCode with async/await to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes asynchronously. It uses the BarcodeGenerator class and related parameters to configure and save a barcode image. Developers often need non‑blocking barcode creation in UI or server applications, making async patterns essential.
// Prompt: Provide example code using async/await pattern to generate barcode without blocking UI thread.
// Tags: barcode generation, async await, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode asynchronously and writes the output path.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is optional barcode text.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine barcode text: use first argument if provided, otherwise default.
        string codeText = args.Length > 0 ? args[0] : "1234567890";

        // Build a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_async.png");

        try
        {
            // Generate the barcode without blocking the calling thread.
            await GenerateBarcodeAsync(codeText, outputPath);
            Console.WriteLine($"Barcode generated asynchronously at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image on a background thread.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    /// <returns>A task representing the asynchronous generation operation.</returns>
    static async Task GenerateBarcodeAsync(string text, string outputPath)
    {
        // Offload the CPU‑bound barcode creation to a thread pool thread.
        await Task.Run(() =>
        {
            // Initialize the generator with Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, string.Empty))
            {
                // Set the barcode text using UTF‑8 encoding.
                generator.SetCodeText(text, Encoding.UTF8);

                // Adjust the X‑dimension (module width) for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}