// Title: Generate Code 39 HIBC LIC Barcode with Primary Data and Save as PNG
// Description: Demonstrates creating a Code 39 HIBC LIC barcode using primary data fields and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext and PrimaryData classes to build HIBC‑LIC barcodes. Developers often need to embed product identifiers, labeler codes, and unit‑of‑measure information in a single barcode for healthcare and logistics applications.
// Prompt: Generate a Code 39 HIBC LIC barcode with primary data and save it as a PNG image.
// Tags: code39, hibc, lic, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Code 39 HIBC LIC barcode with primary data
/// and writes the result to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the barcode, configures dimensions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Environment.CurrentDirectory, "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary_Code39.png");

        // Create primary data codetext for HIBC LIC barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",   // Product or catalog number
                LabelerIdentificationCode = "A999", // Labeler identification code
                UnitOfMeasureID = 1                 // Unit of measure identifier
            }
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}