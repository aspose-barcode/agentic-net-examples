// Title: Generate Code128 barcode and log generation duration
// Description: This example creates a Code128 barcode, saves it as a PNG file, and logs the time taken for generation.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It covers setting barcode parameters, choosing image format, and measuring performance with System.Diagnostics.Stopwatch. Developers creating barcodes for packaging, inventory, or point‑of‑sale systems often need quick generation and logging of execution time, making this pattern useful in automated pipelines and logging frameworks.
// Prompt: Implement logging of barcode generation duration using Stopwatch and output to application log.
// Tags: barcode, code128, generation, png, stopwatch, logging, aspose.barcode, aspnet

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with Aspose.BarCode and logs the generation duration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, saves it as PNG, and logs the elapsed time.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define output file and barcode content
        string outputFile = "barcode.png";
        string codeText = "1234567890";

        // Choose the barcode symbology (Code128)
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Start measuring the generation time
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set visual appearance: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Stop the timer after generation completes
        stopwatch.Stop();

        // Log the elapsed time and the full path of the saved image
        Console.WriteLine($"Barcode generated in {stopwatch.ElapsedMilliseconds} ms.");
        Console.WriteLine($"Saved to: {Path.GetFullPath(outputFile)}");
    }
}