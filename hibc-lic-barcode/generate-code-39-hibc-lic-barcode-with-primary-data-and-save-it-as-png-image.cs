// Title: Generate Code 39 HIBC LIC Barcode and Save as PNG
// Description: Demonstrates creating a HIBC Code 39 LIC barcode with primary data using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext to encode product information in a HIBC Code 39 LIC symbology. Typical use cases include labeling medical devices or pharmaceutical products where HIBC standards are required. Developers often need to set primary data fields, choose the appropriate EncodeTypes value, and export the result to common image formats such as PNG.
/// Prompt: Generate a Code 39 HIBC LIC barcode with primary data and save it as a PNG image.
// Tags: code39, hibc, lic, barcode, generation, png, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC Code 39 LIC barcode with primary data and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Define the primary data for the HIBC Code 39 LIC barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode39LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Initialize the ComplexBarcodeGenerator with the primary data codetext.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Specify the output file path and save the generated barcode as a PNG image.
            string outputPath = "HIBC_Code39_LIC.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}