// Title: Generate HIBC Code 128 LIC barcodes with LinkCharacter and UnitOfMeasureID settings
// Description: Demonstrates how to set the LinkCharacter to 'S' and UnitOfMeasureID to 1 when creating HIBC Code 128 LIC barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of HIBCLICSecondaryAndAdditionalDataCodetext and HIBCLICPrimaryDataCodetext classes. It illustrates typical scenarios where developers need to embed secondary data (e.g., lot numbers) and primary product information (e.g., unit of measure) into HIBC Code 128 LIC barcodes, a common requirement in healthcare and logistics labeling.
// Prompt: Set LinkCharacter to 'S' and UnitOfMeasureID to 1 before generating a Code 128 barcode.
// Tags: barcode, code128, hibc, linkcharacter, unitofmeasureid, aspose.barcode, image generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates HIBC Code 128 LIC barcodes with specific LinkCharacter and UnitOfMeasureID settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images:
    /// 1. Uses secondary data codetext with LinkCharacter set to 'S'.
    /// 2. Uses primary data codetext with UnitOfMeasureID set to 1.
    /// </summary>
    static void Main()
    {
        // Example 1: Set LinkCharacter to 'S' using secondary data codetext
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = 'S',
            Data = new SecondaryAndAdditionalData
            {
                // Populate at least one secondary field; otherwise generation may fail
                LotNumber = "LOT123"
            }
        };

        // Generate barcode image for secondary data and save as PNG
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                bitmap.Save("hibc_secondary.png", ImageFormat.Png);
            }
        }

        // Example 2: Set UnitOfMeasureID to 1 using primary data codetext
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

        // Generate barcode image for primary data and save as PNG
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                bitmap.Save("hibc_primary.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("Barcode images generated successfully.");
    }
}