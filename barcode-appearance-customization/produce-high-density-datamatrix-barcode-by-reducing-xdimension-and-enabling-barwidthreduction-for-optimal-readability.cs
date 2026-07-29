// Title: High‑density DataMatrix barcode generation
// Description: Demonstrates creating a DataMatrix barcode with reduced XDimension and bar‑width reduction for compact, high‑density output.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and generation parameters (XDimension, BarWidthReduction). Typical use cases include printing small labels, packaging, or any scenario requiring dense encoding while maintaining readability. Developers often need to adjust module size and bar width to meet space constraints, and this snippet provides a concise reference.
// Prompt: Produce a high‑density DataMatrix barcode by reducing XDimension and enabling BarWidthReduction for optimal readability.
// Tags: datamatrix, barcode, generation, xdimension, barwidthreduction, aspnet, aspnetcore, aspnet5, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑density DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a DataMatrix barcode with reduced XDimension and zero bar‑width reduction, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "High‑density DataMatrix"))
        {
            // Reduce the module (X) dimension to increase barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // small XDimension

            // Set bar width reduction to zero for maximum compactness
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.0f;

            // Save the generated barcode image to a file
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
    }
}