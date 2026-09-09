// Title: Asynchronous barcode generation using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode image asynchronously to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with async/await. It covers creating a temporary output folder, configuring barcode parameters, and saving the image in PNG format. Developers often need non‑blocking barcode creation in desktop or web applications where UI responsiveness is critical.
// Prompt: Provide example code using async/await pattern to generate barcode without blocking UI thread.
// Tags: barcode, async, await, code128, png, aspose.barcode, generation

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode asynchronously and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is optional barcode text.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Determine the barcode text: use first argument if provided, otherwise default.
        string codeText = args.Length > 0 ? args[0] : "1234567890";

        // Create a unique temporary folder for the output file.
        string folder = Path.Combine(Path.GetTempPath(), "AsyncBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        // Define the full path for the generated PNG image.
        string outputPath = Path.Combine(folder, "barcode.png");

        try
        {
            // Generate the barcode image asynchronously.
            string resultPath = await GenerateBarcodeAsync(codeText, outputPath);
            Console.WriteLine($"Barcode generated at: {resultPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image on a background thread and returns the path to the saved file.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    /// <returns>A task that resolves to the output file path.</returns>
    static Task<string> GenerateBarcodeAsync(string text, string outputPath)
    {
        // Run the barcode generation synchronously inside a Task to avoid blocking the caller.
        return Task.Run(() =>
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set the X-dimension (module width) for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Return the path of the generated image.
            return outputPath;
        });
    }
}