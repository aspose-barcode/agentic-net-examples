// Title: High‑Density DataMatrix Barcode Generation
// Description: Demonstrates creating a DataMatrix barcode with reduced XDimension and enabled BarWidthReduction to increase barcode density while maintaining readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology configuration. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, illustrating how to adjust module size and bar width reduction for high‑density output. Developers often need to fine‑tune these parameters when generating compact barcodes for limited‑space applications.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, highdensity, xdimension, barwidthreduction, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑density DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode with reduced XDimension
    /// and bar width reduction, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string folder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        // Define the full path for the generated PNG file
        string outputPath = Path.Combine(folder, "DataMatrixHighDensity.png");

        // Initialize the barcode generator for DataMatrix symbology with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "HighDensityData"))
        {
            // Reduce the module size to 2 pixels for higher density
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable bar width reduction of 4 pixels to further compact the barcode
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 4f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}