// Title: Generate a HIBC Code 39 LIC barcode with lot number and unit of measure
// Description: Demonstrates creating a HIBCLICCombinedCodetext, setting required primary fields and a lot number, then generating a Code 39 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use HIBCLICCombinedCodetext with ComplexBarcodeGenerator. Developers commonly use these APIs to create HIBC‑compliant barcodes for medical and pharmaceutical labeling, customizing primary and secondary data such as product numbers, unit of measure, and lot numbers.
// Prompt: Create a HIBCLICCombinedCodetext, set lot number and unit of measure, then generate a Code 39 barcode.
// Tags: code39, hibc, barcode generation, png, complexbarcode, hibliccombinedcodetext

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a HIBC Code 39 LIC barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the combined codetext, configures required fields, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create a combined HIBC LIC codetext object that holds both primary and secondary data.
        var combinedCodetext = new HIBCLICCombinedCodetext();

        // Specify the barcode symbology: HIBC Code 39 LIC.
        combinedCodetext.BarcodeType = EncodeTypes.HIBCCode39LIC;

        // Populate primary data (mandatory fields) such as product number, labeler ID, and unit of measure.
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1 // Unit of measure identifier.
        };

        // Populate secondary data with optional information, e.g., the lot number.
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };

        // Generate the barcode using ComplexBarcodeGenerator and save it as a PNG file.
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            generator.Save("hibc_code39.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode generated: hibc_code39.png");
    }
}