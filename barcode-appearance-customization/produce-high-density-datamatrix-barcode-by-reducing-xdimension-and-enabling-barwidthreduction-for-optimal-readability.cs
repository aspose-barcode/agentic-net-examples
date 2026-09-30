// Title: Generate High‑Density DataMatrix Barcode with Reduced XDimension and BarWidthReduction
// Description: Demonstrates how to create a high‑density DataMatrix barcode by decreasing the XDimension and disabling bar width reduction for optimal readability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension and BarWidthReduction using the BarcodeGenerator class. Typical use cases include producing compact barcodes for limited space applications, where developers need fine‑tuned control over module size and visual clarity. The snippet shows saving the barcode as a PNG image, a common requirement for web and print integration.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, barcode, high density, xdimension, barwidthreduction, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a high‑density DataMatrix barcode
/// with customized XDimension and BarWidthReduction settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.png");

        // Initialize a DataMatrix barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HighDensity123"))
        {
            // Reduce the module size (XDimension) to increase barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Disable bar width reduction to maintain readability at high density
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}