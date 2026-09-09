// Title: Generate HIBC Aztec LIC barcode using Aspose.BarCode
// Description: Demonstrates how to create a HIBC Aztec LIC barcode image with primary data using Aspose.BarCode. The example saves the barcode as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC Aztec LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related parameter settings to customize appearance. Developers working with healthcare or logistics labeling often need to generate HIBC barcodes with specific data fields, and this snippet provides a quick reference for those scenarios.
// Prompt: Set the BarcodeType property to Aztec before assigning a HIBCLICPrimaryDataCodetext for generation.
// Tags: barcode, aztec, hibc, lic, generation, png, complexbarcodegenerator, hibclicprimarydatacodetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC Aztec LIC barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBC_Aztec_Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "HIBC_Aztec.png");

        // Create primary data for the HIBC Aztec LIC barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            // Set the barcode symbology to HIBC Aztec LIC (Aztec type).
            BarcodeType = EncodeTypes.HIBCAztecLIC,
            // Populate required data fields.
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Initialize the complex barcode generator with the primary codetext.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Configure visual appearance of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Set module size.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Set bar color.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;        // Set background color.

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }
}