// Title: Generate HIBC QR‑LIC barcode with custom foreground and background colors
// Description: Demonstrates creating a HIBC QR‑LIC barcode and applying a blue foreground and light‑gray background for branding.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext to produce healthcare‑industry barcodes (HIBC). Typical use cases include labeling medical devices, pharmaceuticals, and lab samples where custom colors are required for brand consistency. Developers often need to set barcode symbology, populate primary data fields, and adjust visual appearance via the Parameters API.
// Prompt: Generate HIBC LIC barcodes with custom foreground color (blue) and background color (light gray) for branding.
// Tags: hibc, lic, barcode, color, aspose.barcode, complexbarcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC QR‑LIC barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, builds the barcode data, applies custom colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLICDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        // Prepare primary data for the HIBC QR‑LIC barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode with custom colors using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the barcode (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to light gray
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Save the barcode image as PNG
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"HIBC LIC barcode saved to: {outPath}");
    }
}