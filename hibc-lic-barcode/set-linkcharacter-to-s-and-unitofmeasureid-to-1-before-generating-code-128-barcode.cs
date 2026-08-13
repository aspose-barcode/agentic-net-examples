// Title: Generate HIBC Code 128 LIC barcodes with custom LinkCharacter and UnitOfMeasureID
// Description: Demonstrates how to set the LinkCharacter to 'S' for secondary data and UnitOfMeasureID to 1 for primary data when creating HIBC Code 128 LIC barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator with HIBCLICSecondaryAndAdditionalDataCodetext and HIBCLICPrimaryDataCodetext. Developers commonly need to customize secondary and primary data fields such as LinkCharacter and UnitOfMeasureID for HIBC compliance, and this snippet illustrates the typical API pattern for those scenarios.
// Prompt: Set LinkCharacter to 'S' and UnitOfMeasureID to 1 before generating a Code 128 barcode.
// Tags: barcode, hibc, code128, linkcharacter, unitofmeasure, complexbarcode, generation, png

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Contains examples for generating HIBC Code 128 LIC barcodes with specific secondary and primary data settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes:
    /// 1. A secondary data barcode with LinkCharacter set to 'S'.
    /// 2. A primary data barcode with UnitOfMeasureID set to 1.
    /// </summary>
    static void Main()
    {
        // Example 1: Configure secondary data with LinkCharacter = 'S' for a HIBC Code128 LIC barcode.
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = 'S',
            // At least one secondary data field must be populated; otherwise the generator throws an exception.
            Data = new SecondaryAndAdditionalData { LotNumber = "LOT123" }
        };

        // Generate and save the secondary data barcode image.
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            generator.Save("hibc_code128_link.png");
        }

        // Example 2: Configure primary data with UnitOfMeasureID = 1 for a HIBC Code128 LIC barcode.
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

        // Generate and save the primary data barcode image.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save("hibc_code128_uom.png");
        }

        Console.WriteLine("Barcodes generated successfully.");
    }
}