// Title: Generate HIBCLIC Combined Code39 Barcode with Lot Number and Unit of Measure
// Description: Demonstrates creating a HIBCLICCombinedCodetext, setting the lot number and unit of measure, and generating a Code 39 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICCombinedCodetext, PrimaryData, and SecondaryAndAdditionalData classes to build healthcare‑related barcodes. Typical use cases include labeling medical devices or pharmaceuticals where lot tracking and unit of measure are required. Developers often need to combine multiple data segments into a single barcode using the ComplexBarcodeGenerator API.
// Prompt: Create a HIBCLICCombinedCodetext, set lot number and unit of measure, then generate a Code 39 barcode.
// Tags: barcode, code39, hibclic, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBCLIC combined Code 39 barcode with lot number and unit of measure.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the combined codetext, configures data, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLICExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLICCombined_Code39.png");

        // Create combined HIBCLIC codetext and specify the barcode symbology
        var combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC
        };

        // Set primary data (product information and unit of measure)
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Set secondary data (lot number)
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123"
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Adjust X-dimension for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}