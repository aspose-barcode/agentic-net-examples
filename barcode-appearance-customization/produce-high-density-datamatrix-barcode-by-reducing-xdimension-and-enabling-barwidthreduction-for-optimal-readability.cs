// Title: High‑Density DataMatrix Barcode Generation
// Description: Demonstrates creating a DataMatrix barcode with reduced XDimension and enabled bar‑width reduction to achieve higher data density.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension and BarWidthReduction for DataMatrix symbology. Developers commonly use these APIs (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) to fine‑tune barcode size and readability for printing and scanning applications.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, barcode, high density, xdimension, barwidthreduction, aspnet, aspnetcore, aspose.barcode, image generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a high‑density DataMatrix barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures barcode parameters,
    /// generates the barcode, and writes the image file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "HighDensityDataMatrix.png");

        // Initialize the barcode generator for DataMatrix symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HighDensitySample"))
        {
            // Reduce XDimension to increase the barcode's data density
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable bar width reduction to improve readability after printing
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}