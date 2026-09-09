// Title: Generate HIBC Combined Code39 Barcode with Lot Number and Unit of Measure
// Description: Demonstrates how to create a HIBCLICCombinedCodetext, set the lot number and unit of measure, and generate a Code 39 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes with ComplexBarcodeGenerator to produce HIBC Code 39 LIC barcodes, a common requirement for medical device labeling and inventory tracking.
// Prompt: Create a HIBCLICCombinedCodetext, set lot number and unit of measure, then generate a Code 39 barcode.
// Tags: code39, hibc, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC Code 39 LIC barcode with lot number and unit of measure information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the combined codetext, configures data fields, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLICCombined_Code39.png");

        // Instantiate the combined codetext object for HIBC Code 39 LIC.
        HIBCLICCombinedCodetext combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC // Specify the barcode symbology.
        };

        // Populate required primary data fields.
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",   // Example product number.
            LabelerIdentificationCode = "A999", // Example labeler ID.
            UnitOfMeasureID = 1                 // Set unit of measure identifier.
        };

        // Populate secondary data with the lot number.
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123" // Example lot number.
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Optionally adjust the module (X) dimension for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"HIBCLIC Combined Code39 barcode saved to: {outputPath}");
    }
}