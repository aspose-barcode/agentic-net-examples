// Title: Generate HIBC LIC Barcodes with Custom Colors
// Description: Demonstrates creating HIBC Code 128 LIC barcodes with a blue foreground and light‑gray background for branding purposes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICCombinedCodetext and HIBCLICSecondaryAndAdditionalDataCodetext classes to encode primary, secondary, and additional data for HIBC LIC symbology. Typical use cases include product labeling, inventory tracking, and brand‑consistent barcode rendering. Developers often need to customize colors, output formats, and combine multiple data fields, which this snippet illustrates.
/// Prompt: Generate HIBC LIC barcodes with custom foreground color (blue) and background color (light gray) for branding.
/// Tags: hibc, lic, barcode, color, branding, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates HIBC LIC barcodes with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a combined HIBC LIC barcode and a secondary‑only HIBC LIC barcode,
    /// applies branding colors, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Example 1: Combined HIBC LIC (primary + secondary data)
        // --------------------------------------------------------------------
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            },
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                Quantity = 30,
                ExpiryDate = DateTime.Now.AddMonths(6),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                DateOfManufacture = DateTime.Now.AddMonths(-2)
            }
        };

        string combinedPath = Path.Combine(outputDir, "HIBC_LIC_Combined.png");
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Apply branding colors: blue bars on light gray background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Save the barcode image to file
            generator.Save(combinedPath);
        }

        Console.WriteLine($"Combined HIBC LIC barcode saved to: {combinedPath}");

        // --------------------------------------------------------------------
        // Example 2: Secondary‑only HIBC LIC (requires LinkCharacter)
        // --------------------------------------------------------------------
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+', // mandatory for secondary‑only codetext
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT456",
                SerialNumber = "SERIAL456",
                Quantity = 15,
                ExpiryDate = DateTime.Now.AddMonths(12),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                DateOfManufacture = DateTime.Now.AddMonths(-1)
            }
        };

        string secondaryPath = Path.Combine(outputDir, "HIBC_LIC_Secondary.png");
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            // Apply the same branding colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Save the barcode image to file
            generator.Save(secondaryPath);
        }

        Console.WriteLine($"Secondary‑only HIBC LIC barcode saved to: {secondaryPath}");
    }
}