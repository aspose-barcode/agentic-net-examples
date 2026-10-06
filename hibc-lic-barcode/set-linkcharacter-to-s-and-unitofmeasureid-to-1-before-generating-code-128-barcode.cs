// Title: Generate HIBC Code128 LIC barcodes with LinkCharacter and UnitOfMeasure settings
// Description: Demonstrates how to set the LinkCharacter to 'S' and the UnitOfMeasureID to 1 when generating HIBC Code128 LIC barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICSecondaryAndAdditionalDataCodetext and HIBCLICPrimaryDataCodetext classes to create HIBC Code128 LIC symbology. Developers often need to customize secondary data (e.g., LinkCharacter) or primary data (e.g., UnitOfMeasureID) for regulatory labeling, and this snippet illustrates the typical API calls and output handling for such scenarios.
// Prompt: Set LinkCharacter to 'S' and UnitOfMeasureID to 1 before generating a Code 128 barcode.
// Tags: barcode, code128, hibc, linkcharacter, unitofmeasure, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates HIBC Code128 LIC barcodes with customized LinkCharacter and UnitOfMeasureID settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images demonstrating the required settings.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Example 1: Set LinkCharacter to 'S' for HIBC Code128 LIC (secondary data)
        // ------------------------------------------------------------
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = 'S',
            Data = secondaryData
        };
        string linkCharPath = Path.Combine(outputDir, "Code128_LinkCharacter.png");
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            // Save the barcode image as PNG.
            generator.Save(linkCharPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Barcode with LinkCharacter saved to: {linkCharPath}");

        // ------------------------------------------------------------
        // Example 2: Set UnitOfMeasureID to 1 for HIBC Code128 LIC (primary data)
        // ------------------------------------------------------------
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };
        string unitPath = Path.Combine(outputDir, "Code128_UnitOfMeasure.png");
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Save the barcode image as PNG.
            generator.Save(unitPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Barcode with UnitOfMeasureID saved to: {unitPath}");
    }
}