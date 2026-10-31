// Title: Asynchronous MaxiCode barcode generation and file saving
// Description: Demonstrates generating a MaxiCode (Mode 3) barcode asynchronously and writing it to a PNG file without blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related classes to create high‑density 2‑D barcodes. Developers often need to generate such barcodes in responsive applications, so async patterns are demonstrated for non‑blocking execution.
// Prompt: Use async methods to generate a MaxiCode image and write it to a file without blocking the UI.
// Tags: maxicode, async, barcode generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates asynchronous generation of a MaxiCode barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes it to disk asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode3StandardSecondMessage.png");

        // Generate the MaxiCode barcode and save it without blocking the UI thread.
        await GenerateMaxiCodeAsync(outputPath);

        // Inform the user where the file was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Asynchronously creates a MaxiCode (Mode 3) barcode with an unstructured second message and saves it as a PNG file.
    /// </summary>
    /// <param name="filePath">The full file path where the PNG image will be saved.</param>
    private static async Task GenerateMaxiCodeAsync(string filePath)
    {
        // Configure the MaxiCode codetext for mode 3 (standard) with postal, country, and service data.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Add an unstructured second message to the barcode.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Perform the barcode generation on a background thread to avoid UI blocking.
        await Task.Run(() =>
        {
            // Initialize the generator with the prepared codetext.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the generated barcode as a PNG image to the specified path.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        });
    }
}