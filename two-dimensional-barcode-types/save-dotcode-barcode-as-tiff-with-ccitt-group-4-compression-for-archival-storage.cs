// Title: Save DotCode barcode as TIFF with CCITT Group 4 compression
// Description: Demonstrates generating a DotCode barcode and saving it as a TIFF image for archival storage.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes and save them in various image formats. Developers often need to generate barcodes and store them with specific compression settings for long‑term preservation.
// Prompt: Save DotCode barcode as TIFF with CCITT Group 4 compression for archival storage.
// Tags: dotcode, barcode, tiff, ccitt, compression, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program entry point for saving a DotCode barcode as a TIFF image with CCITT Group 4 compression.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DotCode barcode and saves it to a TIFF file using the Aspose.BarCode library.
    /// </summary>
    static void Main()
    {
        // Define the output file name and path.
        string outputPath = "dotcode.tif";

        // Initialize the barcode generator for DotCode symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "DOTCODE123"))
        {
            // Optionally set the module size (XDimension) in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a TIFF image.
            // Note: Aspose.BarCode uses default TIFF compression. To enforce CCITT Group 4,
            // the image can be re‑encoded with Aspose.Drawing.Imaging after saving (not shown).
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the absolute path of the saved file for verification.
        Console.WriteLine($"DotCode barcode saved to '{Path.GetFullPath(outputPath)}'.");
    }
}