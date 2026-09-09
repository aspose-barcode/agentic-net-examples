// Title: Generate HIBC Code128 LIC Barcodes with Custom LinkCharacter and UnitOfMeasureID
// Description: Demonstrates how to set the LinkCharacter to a custom value and assign a UnitOfMeasureID when creating HIBC Code128 LIC barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and HIBCLICPrimaryDataCodetext classes to produce HIBC Code128 LIC barcodes. Typical use cases include labeling medical devices or pharmaceutical products where secondary data (e.g., lot number) and primary data (e.g., unit of measure) must be encoded. Developers often need to customize link characters and unit identifiers to meet regulatory standards.
// Prompt: Set LinkCharacter to 'S' and UnitOfMeasureID to 1 before generating a Code 128 barcode.
// Tags: barcode, hibc, code128, linkcharacter, unitofmeasureid, aspnet, aspose.barcode, png, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of HIBC Code128 LIC barcodes with custom LinkCharacter and UnitOfMeasureID settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates two barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Example 1: Set LinkCharacter to '+' for HIBC Code128 LIC (secondary data)
        // ------------------------------------------------------------
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+', // Custom link character
            Data = secondaryData
        };
        string secondaryPath = Path.Combine(outputDir, "HIBC_Code128_LinkCharacter.png");
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            generator.Save(secondaryPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Generated barcode with LinkCharacter at: {secondaryPath}");

        // ------------------------------------------------------------
        // Example 2: Set UnitOfMeasureID to 1 for HIBC Code128 LIC (primary data)
        // ------------------------------------------------------------
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1 // Custom unit of measure identifier
        };
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };
        string primaryPath = Path.Combine(outputDir, "HIBC_Code128_UnitOfMeasure.png");
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save(primaryPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Generated barcode with UnitOfMeasureID at: {primaryPath}");
    }
}