// Title: Generate QR HIBC LIC barcode with custom 5-pixel margin for label printing
// Description: Demonstrates how to create a HIBC QR LIC barcode using Aspose.BarCode and apply a uniform 5-pixel margin, suitable for label printing scenarios.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on HIBC symbology. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and barcode padding settings. Developers creating product labels, medical device tags, or inventory stickers often need to customize barcode margins for printer alignment and readability.
// Prompt: Use a custom barcode margin of five pixels when generating a QR HIBC LIC barcode for label printing.
// Tags: hibc, qr, lic, barcode, margin, padding, label printing, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating QR HIBC LIC barcode generation with custom margins.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR HIBC LIC barcode, applies a 5‑pixel margin on all sides, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for HIBC QR LIC barcode
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Create generator for the complex barcode
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Set custom margin (padding) of 5 pixels on all sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Define output file path
            string outputPath = "hibc_qr.png";

            // Save the barcode image to the specified path
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved
            Console.WriteLine($"QR HIBC LIC barcode saved to {outputPath}");
        }
    }
}