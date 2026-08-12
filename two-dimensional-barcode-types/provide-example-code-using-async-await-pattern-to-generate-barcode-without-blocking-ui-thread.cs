// Title: Asynchronous Barcode Generation with Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode asynchronously using async/await to avoid blocking the UI thread.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator in an asynchronous pattern. This example belongs to the barcode creation category, illustrating background processing with Task.Run, directory handling, and saving the image. Developers working on desktop or web applications often need to generate barcodes without freezing the UI, and this snippet provides a concise reference.
// Prompt: Provide example code using async/await pattern to generate barcode without blocking UI thread.
// Tags: barcode, code128, async, await, generation, image, aspose.barcode, task.run

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point and asynchronous barcode generation logic.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image asynchronously and saves it to the specified path.
    /// The actual generation runs on a background thread to keep the calling thread responsive.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The full file path where the barcode image will be saved.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task GenerateBarcodeAsync(string codeText, string outputPath)
    {
        // Execute the blocking generation code on a thread‑pool thread.
        await Task.Run(() =>
        {
            // Ensure the output directory exists before saving.
            string? directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and configure the barcode generator.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: adjust module size and visual style.
                generator.Parameters.Barcode.XDimension.Point = 2f; // module size
                generator.Parameters.Barcode.FilledBars = true;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode image to the file system.
                generator.Save(outputPath);
            }
        });
    }

    /// <summary>
    /// Asynchronous entry point of the console application.
    /// Parses command‑line arguments, invokes barcode generation, and reports status.
    /// </summary>
    /// <param name="args">Optional arguments: [0] = barcode text, [1] = output file path.</param>
    /// <returns>A task that represents the asynchronous execution of the program.</returns>
    static async Task Main(string[] args)
    {
        // Determine barcode text and output location, using defaults if not supplied.
        string codeText = args.Length > 0 ? args[0] : "123ABC";
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        try
        {
            Console.WriteLine($"Generating barcode for \"{codeText}\"...");
            await GenerateBarcodeAsync(codeText, outputPath);
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}