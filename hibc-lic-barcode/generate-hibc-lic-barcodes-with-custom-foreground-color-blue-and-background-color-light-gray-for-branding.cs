// Title: Generate HIBC LIC Barcode with Custom Colors
// Description: Demonstrates creating a HIBC LIC QR barcode with a blue foreground and light‑gray background, then saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext to produce HIBC LIC (Health Industry Bar Code) QR codes. Typical scenarios include medical device labeling, pharmaceutical packaging, and any application requiring standardized product identification with brand‑specific colors. Developers often need to customize visual appearance (colors, dimensions) while preserving the required data structure.
// Prompt: Generate HIBC LIC barcodes with custom foreground color (blue) and background color (light gray) for branding.
// Tags: hibc, lic, color, png, generation, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC LIC barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a HIBC LIC QR barcode, applies custom colors, and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare output folder in the system temporary directory
        string outputFolder = Path.Combine(Path.GetTempPath(), "HIBCLICDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "HIBCLICPrimary.png");

        // Build the primary data codetext required for a HIBC LIC QR barcode
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

        // Generate the barcode with custom visual settings
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Optional: set the module (pixel) size for higher resolution
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        Console.WriteLine($"HIBC LIC barcode saved to: {outputPath}");
    }
}