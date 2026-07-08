// Title: Create DataBar Expanded Stacked Barcode and Save as BMP
// Description: Demonstrates generating a DataBar Expanded Stacked barcode with three columns and an aspect ratio of eight, then saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure DataBar-specific parameters using the BarcodeGenerator class. Typical use cases include creating high‑density retail barcodes for packaging and point‑of‑sale systems. Developers often need to adjust columns, aspect ratios, and output formats when working with GS1 DataBar symbologies.
// Prompt: Create DataBar Expanded Stacked barcode with three columns, aspect ratio eight, save BMP image.
// Tags: databar, expanded stacked, barcode, bmp, generation, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DataBar Expanded Stacked barcode and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the DataBar Expanded Stacked symbology
        // and provide a sample GTIN code as the barcode text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "(01)12345678901231"))
        {
            // Configure DataBar-specific settings:
            // - Set the number of columns to three.
            // - Set the aspect ratio to eight (wide barcode).
            generator.Parameters.Barcode.DataBar.Columns = 3;          // three columns
            generator.Parameters.Barcode.DataBar.AspectRatio = 8f;    // aspect ratio of eight

            // Save the generated barcode image in BMP format.
            generator.Save("databar_expanded_stacked.bmp");
        }
    }
}