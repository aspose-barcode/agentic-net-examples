// Title: Generate HIBC LIC barcode with custom colors
// Description: Demonstrates creating a HIBC QR LIC barcode with a blue foreground and light‑gray background, then saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext and EncodeTypes to produce HIBC LIC barcodes. Typical use cases include product labeling, inventory tracking, and brand‑specific visual styling where custom colors and image formats are required. Developers often need to configure barcode parameters such as colors, dimensions, and output format before saving the image.
// Prompt: Generate HIBC LIC barcodes with custom foreground color (blue) and background color (light gray) for branding.
// Tags: hibc, lic, barcode generation, color customization, png, complexbarcodegenerator, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a HIBC LIC barcode with custom foreground and background colors
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies color settings, and writes the file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLICDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        // Prepare the HIBC LIC primary data codetext
        var codetext = new HIBCLICPrimaryDataCodetext
        {
            // Set the barcode symbology to HIBC QR LIC
            BarcodeType = EncodeTypes.HIBCQRLIC,
            // Populate the required data fields
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Apply custom colors: blue bars on a light‑gray background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Set the X‑dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the barcode image as PNG
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}