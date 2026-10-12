// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating a barcode image asynchronously using Aspose.BarCode to avoid blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with async/await for non‑blocking operations. Developers often need to create barcodes in background tasks for desktop or web applications without freezing the UI, and this snippet shows the typical pattern of retrieving an EncodeTypes value via reflection and saving the image in PNG format.
// Prompt: Provide example code using async/await pattern to generate barcode without blocking UI thread.
// Tags: barcode, async, await, code128, png, aspose.barcode, generation, background-task

using System;
using System.IO;
using System.Reflection;
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
    /// Entry point. Parses arguments, invokes asynchronous barcode generation, and reports the output path.
    /// </summary>
    /// <param name="args">Command‑line arguments: optional symbology name and code text.</param>
    static async Task Main(string[] args)
    {
        // Determine symbology and code text from command‑line arguments or use defaults.
        string symbology = args.Length > 0 ? args[0] : "Code128";
        string codeText = args.Length > 1 ? args[1] : "123456789";

        // Build a temporary file path for the generated PNG barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate the barcode asynchronously.
        await GenerateBarcodeAsync(symbology, codeText, outputPath);

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image asynchronously based on the specified symbology and text.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static async Task GenerateBarcodeAsync(string symbologyName, string codeText, string outputPath)
    {
        // Use reflection to obtain the EncodeTypes field matching the symbology name.
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            // Symbology not found; report and exit.
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the reflected value to BaseEncodeType.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Run the barcode generation on a background thread to avoid blocking.
        await Task.Run(() =>
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}