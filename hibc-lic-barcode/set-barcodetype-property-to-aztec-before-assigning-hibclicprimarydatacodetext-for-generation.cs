// Title: Generate HIBC Aztec LIC barcode using ComplexBarcodeGenerator
// Description: Demonstrates how to create a HIBC Aztec LIC barcode by setting the BarcodeType to Aztec and providing primary data, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and EncodeTypes to produce specialized barcodes. Developers often need to generate industry‑specific barcodes (e.g., HIBC) for labeling and inventory systems, requiring precise data structures and visual customization.
// Prompt: Set the BarcodeType property to Aztec before assigning a HIBCLICPrimaryDataCodetext for generation.
// Tags: aztec, hibc, barcode, generation, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC Aztec LIC barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates primary data, configures the barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBC_Aztec.png");

        // Define primary data required for a HIBC LIC barcode
        var primaryData = new PrimaryData
        {
            LabelerIdentificationCode = "A999",
            ProductOrCatalogNumber = "12345",
            UnitOfMeasureID = 1
        };

        // Create the HIBCLICPrimaryDataCodetext and specify the Aztec barcode type
        var hibcPrimary = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCAztecLIC,
            Data = primaryData
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(hibcPrimary))
        {
            // Optional visual customizations
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}