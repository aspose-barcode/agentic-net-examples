// Title: Generate MaxiCode barcode asynchronously and save as PNG
// Description: Demonstrates using async/await to create a MaxiCode barcode image without blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode symbologies such as MaxiCode. It showcases the ComplexBarcodeGenerator and MaxiCodeCodetextMode2 classes for creating high‑density 2‑D barcodes, a common requirement in logistics and shipping applications. Developers often need to generate these barcodes in background tasks to keep UI responsive.
// Prompt: Use async methods to generate a MaxiCode image and write it to a file without blocking the UI.
// Tags: maxicode, barcode, async, generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates asynchronous generation of a MaxiCode barcode image and saving it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Calls the asynchronous barcode generation method.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated PNG image
        string outputPath = "maxicode.png";

        // Generate the MaxiCode barcode asynchronously
        await GenerateMaxiCodeAsync(outputPath);

        // Inform the user that the image has been saved
        Console.WriteLine($"MaxiCode image saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a MaxiCode barcode using the ComplexBarcodeGenerator and saves it to the specified path.
    /// The operation runs on a background thread to avoid blocking the UI.
    /// </summary>
    /// <param name="path">File system path where the PNG image will be saved.</param>
    static async Task GenerateMaxiCodeAsync(string path)
    {
        // Execute the barcode generation on a thread‑pool thread
        await Task.Run(() =>
        {
            // Prepare MaxiCode codetext (Mode 2 with a standard second message)
            var maxiCode = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",
                CountryCode = 56,
                ServiceCategory = 999
            };

            // Create the optional second message for the MaxiCode
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Sample message"
            };
            maxiCode.SecondMessage = secondMessage;

            // Generate and save the barcode image using the ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                generator.Save(path, BarCodeImageFormat.Png);
            }
        });
    }
}