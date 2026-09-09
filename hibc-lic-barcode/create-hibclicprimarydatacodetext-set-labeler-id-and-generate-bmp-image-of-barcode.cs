// Title: Generate HIBC QRLIC Barcode with Primary Data and Save as BMP
// Description: Demonstrates how to create a HIBCLICPrimaryDataCodetext, set the labeler identification code, and generate a BMP image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of HIBCLICPrimaryDataCodetext, ComplexBarcodeGenerator, and related parameter settings to produce HIBC QRLIC barcodes. Developers working with healthcare or logistics labeling often need to embed primary product data and labeler IDs in HIBC barcodes, and this snippet illustrates the typical workflow for creating and exporting such barcodes as bitmap images.
// Prompt: Create a HIBCLICPrimaryDataCodetext, set labeler ID, and generate a BMP image of the barcode.
// Tags: hibc, lic, barcode generation, bmp, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a HIBC QRLIC barcode with primary data,
/// sets the labeler identification code, and saves the result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLICPrimary.bmp");

        // Initialize the primary data codetext for a HIBC QRLIC barcode.
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            // Select the HIBC QRLIC symbology.
            BarcodeType = EncodeTypes.HIBCQRLIC,
            // Populate the primary data fields.
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                // Set the labeler identification code as required.
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Create the barcode generator with the configured codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Adjust the X-dimension (module width) to 10 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Save the generated barcode as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}