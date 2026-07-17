// Title: Barcode generation with duration logging using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode, measuring generation time, and logging the duration.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set barcode parameters, and save images. Developers often need to generate barcodes programmatically, customize dimensions, and monitor performance; this snippet illustrates those common tasks and how to log execution time for diagnostics.
// Prompt: Implement logging of barcode generation duration using Stopwatch and output to application log.
// Tags: barcode, code128, generation, performance, logging, aspose.barcode, png

using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with performance logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, measures execution time, and logs the duration.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "barcode.png";

        // Start the stopwatch to measure barcode generation duration.
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create a BarcodeGenerator instance for Code128 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Adjust barcode visual parameters: X-dimension and bar height.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the stopwatch now that generation is complete.
        stopwatch.Stop();

        // Log the elapsed time to the application log (Trace listeners).
        Trace.WriteLine($"Barcode generated and saved to '{outputPath}' in {stopwatch.ElapsedMilliseconds} ms.");
    }
}