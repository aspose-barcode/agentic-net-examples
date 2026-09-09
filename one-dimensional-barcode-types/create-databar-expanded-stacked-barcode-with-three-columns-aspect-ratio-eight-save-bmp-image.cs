// Title: Generate DataBar Expanded Stacked barcode with three columns and aspect ratio 8
// Description: Demonstrates creating a DataBar Expanded Stacked barcode, configuring columns and aspect ratio, and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DatabarExpandedStacked. Typical use cases include generating high‑density linear barcodes for retail and inventory systems where multiple data columns and specific aspect ratios are required. Developers often need to adjust X‑dimension, column count, and aspect ratio before exporting the barcode to various image formats.
// Prompt: Create DataBar Expanded Stacked barcode with three columns, aspect ratio eight, save BMP image.
// Tags: databar, expanded stacked, barcode generation, bmp, aspnet, aspose.barcode, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creation of a DataBar Expanded Stacked barcode and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode with specified parameters and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBarExpandedStacked.bmp");

        // Initialize barcode generator with DataBar Expanded Stacked symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, "Sample Text"))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure DataBar specific settings: three columns and aspect ratio of 8
            generator.Parameters.Barcode.DataBar.Columns = 3;
            generator.Parameters.Barcode.DataBar.AspectRatio = 8f;

            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform user of saved file location
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}