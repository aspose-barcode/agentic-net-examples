// Title: Generate a HIBCLICCombined Code 39 barcode with lot number and unit of measure
// Description: Demonstrates how to create a HIBCLICCombinedCodetext, assign lot number and unit of measure, and render a Code 39 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows usage of HIBCLICCombinedCodetext, PrimaryData, SecondaryAndAdditionalData, and ComplexBarcodeGenerator to produce HIBC Code 39 LIC barcodes. Developers creating healthcare or industrial labels often need to embed product identifiers, lot numbers, and measurement units in a single barcode; this snippet illustrates the typical workflow for such scenarios.
// Prompt: Create a HIBCLICCombinedCodetext, set lot number and unit of measure, then generate a Code 39 barcode.
// Tags: code39, hibc, combinedcodetext, lot-number, unit-of-measure, barcode-generation, aspnet, aspnet-core, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBCLICCombined Code 39 barcode with lot number and unit of measure.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the combined codetext, configures data, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICCombined.png");

        // Instantiate the combined HIBC LIC codetext object
        HIBCLICCombinedCodetext combinedCodetext = new HIBCLICCombinedCodetext
        {
            // Specify the barcode symbology (HIBC Code 39 LIC)
            BarcodeType = EncodeTypes.HIBCCode39LIC
        };

        // Populate primary data fields (product number, labeler ID, unit of measure)
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1 // unit of measure identifier
        };

        // Populate secondary data fields (lot number)
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123" // lot number identifier
        };

        // Generate the barcode using the complex barcode generator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Set the X-dimension (module width) in pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}