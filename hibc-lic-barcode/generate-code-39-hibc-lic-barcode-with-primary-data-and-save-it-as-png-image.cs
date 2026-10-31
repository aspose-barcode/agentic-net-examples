// Title: Generate Code 39 HIBC LIC Primary Data Barcode and Save as PNG
// Description: Demonstrates creating a HIBC Code 39 LIC barcode with primary data using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of key API classes such as ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and EncodeTypes to produce HIBC‑compliant barcodes. Typical scenarios include labeling medical devices, pharmaceuticals, and other regulated products where HIBC LIC symbology is required. Developers often need to configure primary data fields, adjust rendering parameters, and export the result to common image formats.
// Prompt: Generate a Code 39 HIBC LIC barcode with primary data and save it as a PNG image.
// Tags: code39, hibc, lic, barcode, generation, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a HIBC Code 39 LIC barcode with primary data
/// and saves it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary_Code39.png");

        // Create a codetext object for HIBC Code 39 LIC with primary data fields.
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode using the complex barcode generator.
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(codetext))
        {
            // Set the X-dimension (module width) in pixels for better readability.
            gen.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}