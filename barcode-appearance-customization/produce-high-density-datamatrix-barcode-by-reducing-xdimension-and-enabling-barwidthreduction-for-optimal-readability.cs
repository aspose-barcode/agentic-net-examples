// Title: High‑density DataMatrix barcode generation
// Description: Demonstrates creating a DataMatrix barcode with reduced XDimension and bar‑width reduction for optimal readability.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, barcode, highdensity, xdimension, barwidthreduction, imageoutput, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑density DataMatrix barcode
/// by adjusting XDimension and enabling BarWidthReduction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates and saves a DataMatrix barcode image.
    /// </summary>
    static void Main()
    {
        // Sample data to encode
        const string codeText = "HighDensityDataMatrix123";

        // Create a DataMatrix barcode generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Use interpolation mode to control size via image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set desired image dimensions (points) – adjust as needed
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Reduce XDimension for higher barcode density (smaller modules)
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // small module size

            // Enable bar width reduction to compensate for ink spread
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Optional: set barcode bar color (black by default)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode image to a file
            generator.Save("datamatrix_high_density.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("DataMatrix barcode generated: datamatrix_high_density.png");
    }
}