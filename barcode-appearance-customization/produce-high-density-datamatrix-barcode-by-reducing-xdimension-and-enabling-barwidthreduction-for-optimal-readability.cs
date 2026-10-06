// Title: Generate High‑Density DataMatrix Barcode with Reduced XDimension
// Description: Demonstrates creating a DataMatrix barcode with a smaller XDimension and enabled BarWidthReduction to achieve higher density and better readability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension and BarWidthReduction using the BarcodeGenerator class. Typical use cases include producing compact barcodes for limited space labels or improving scan reliability after printing. Developers often need to adjust these settings when optimizing barcode size and quality.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, highdensity, xdimension, barwidthreduction, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a high‑density DataMatrix barcode
/// by reducing the XDimension and applying BarWidthReduction for
/// improved readability after printing.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "HighDensityDataMatrix.png");

        // Create a BarcodeGenerator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HighDensity"))
        {
            // Reduce XDimension to increase barcode density.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable bar width reduction to improve readability after printing.
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}