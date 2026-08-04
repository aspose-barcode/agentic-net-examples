// Title: Generate HIBC Aztec LIC Barcode with ComplexBarcodeGenerator
// Description: Demonstrates how to create a HIBC LIC barcode encoded as an Aztec symbol and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and PrimaryData classes. Typical use cases include generating HIBC (Health Industry Bar Code) Aztec symbols for product labeling in healthcare and logistics. Developers often need to configure barcode type, set primary data fields, and customize visual appearance before saving the image.
// Prompt: Set the BarcodeType property to Aztec before assigning a HIBCLICPrimaryDataCodetext for generation.
// Tags: aztec, hibc, lic, barcode generation, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a HIBC Aztec LIC barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares primary data, configures the barcode, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare primary data for the HIBC LIC barcode (product number, labeler ID, unit of measure)
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Create HIBCLICPrimaryDataCodetext and set the barcode type to Aztec
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCAztecLIC,
            Data = primaryData
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Optional visual settings: black bars on white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define output file name and save the barcode as PNG
            string outputFile = "hibc_aztec.png";
            generator.Save(outputFile, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputFile)}");
        }
    }
}