// Title: Generate HIBC Code 128 Barcodes with LinkCharacter and UnitOfMeasureID
// Description: Demonstrates how to set the LinkCharacter to 'S' and the UnitOfMeasureID to 1 when creating HIBC Code 128 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on HIBC (Health Industry Bar Code) symbology. It showcases the use of ComplexBarcodeGenerator together with HIBCLICSecondaryAndAdditionalDataCodetext and HIBCLICPrimaryDataCodetext classes to customize barcode data fields. Developers working with healthcare labeling, inventory tracking, or regulatory compliance often need to adjust link characters and unit of measure identifiers in HIBC barcodes.
// Prompt: Set LinkCharacter to 'S' and UnitOfMeasureID to 1 before generating a Code 128 barcode.
// Tags: barcode, hibc, code128, linkcharacter, unitofmeasureid, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates HIBC Code 128 barcodes with specific LinkCharacter and UnitOfMeasureID settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images demonstrating the required settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the output images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "HIBC_Code128_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Example 1: Set LinkCharacter to 'S' for a secondary/additional data barcode.
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new SecondaryAndAdditionalData { LotNumber = "LOT123" },
            LinkCharacter = 'S'
        };

        // Generate and save the barcode image for the secondary data example.
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            string path = Path.Combine(outputFolder, "HIBC_Code128_LinkCharacter.png");
            generator.Save(path, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with LinkCharacter to {path}");
        }

        // Example 2: Set UnitOfMeasureID to 1 for a primary data barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate and save the barcode image for the primary data example.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            string path = Path.Combine(outputFolder, "HIBC_Code128_UnitOfMeasureID.png");
            generator.Save(path, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with UnitOfMeasureID to {path}");
        }
    }
}