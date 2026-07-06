// Title: Generate HIBC LIC Code 39 Barcode with Lot Number and Unit of Measure
// Description: Demonstrates creating a HIBCLICCombinedCodetext, setting the lot number and unit of measure, and generating a Code 39 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICCombinedCodetext, ComplexBarcodeGenerator, and related classes to produce HIBC‑LIC barcodes. Developers often need to encode product information, lot numbers, and measurement units for healthcare and logistics applications, and this snippet illustrates the typical workflow for such scenarios.
// Prompt: Create a HIBCLICCombinedCodetext, set lot number and unit of measure, then generate a Code 39 barcode.
// Tags: code39, barcode-generation, png, hibcliccombinedcodetext, complexbarcode, complexbarcodegenerator, bitmap

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that builds a HIBCLICCombinedCodetext with lot number and unit of measure,
/// then generates a Code 39 barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the combined codetext, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize combined HIBC LIC codetext with required fields
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            // Specify Code 39 LIC symbology (default) for clarity
            BarcodeType = EncodeTypes.HIBCCode39LIC,

            // Primary data includes product number, labeler ID, and unit of measure
            PrimaryData = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1 // Set unit of measure identifier
            },

            // Secondary data includes the lot number; other fields are optional
            SecondaryAndAdditionalData = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123" // Set lot number
                // Additional secondary fields can be left unset
            }
        };

        // Use ComplexBarcodeGenerator to create the barcode image from the codetext
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Generate the bitmap representation of the barcode
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap as a PNG file
                bitmap.Save("hibc_combined_code39.png", ImageFormat.Png);
            }
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("HIBC LIC Code 39 barcode generated: hibc_combined_code39.png");
    }
}