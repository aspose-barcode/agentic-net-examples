// Title: Async Barcode Generation Example
// Description: Demonstrates generating a Code128 barcode asynchronously using Aspose.BarCode to avoid UI thread blocking.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcode images without freezing the UI. It highlights the use of BarcodeGenerator, BaseEncodeType, and async/await patterns, which are common when integrating barcode creation into desktop or web applications that require responsive interfaces.
// Prompt: Provide example code using async/await pattern to generate barcode without blocking UI thread.
// Tags: code128, generation, png, async, await, aspose.barcode, barcodegenerator, baseencodetype

using System;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode asynchronously and writes a completion message.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Asynchronously generate a barcode without blocking the calling thread.
        await GenerateBarcodeAsync("async_barcode.png", EncodeTypes.Code128, "AsyncDemo");

        // Inform the user that the operation has finished.
        Console.WriteLine("Barcode generation completed.");
    }

    /// <summary>
    /// Generates a barcode image on a background thread.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static Task GenerateBarcodeAsync(string outputPath, BaseEncodeType encodeType, string codeText)
    {
        // Offload the generation to a background thread to keep the UI responsive.
        return Task.Run(() =>
        {
            // BarcodeGenerator implements IDisposable; ensure resources are released promptly.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode image to the specified file.
                generator.Save(outputPath);
            }
        });
    }
}