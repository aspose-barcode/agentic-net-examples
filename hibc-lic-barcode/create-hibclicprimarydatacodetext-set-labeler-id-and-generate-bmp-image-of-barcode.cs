// Title: Generate HIBC QRLIC Primary Data Barcode and Save as BMP
// Description: Demonstrates how to create a HIBCLICPrimaryDataCodetext, set labeler identification, and generate a BMP image of the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext and EncodeTypes to produce HIBC QRLIC barcodes. Typical use cases include encoding product information for healthcare and logistics, where developers need to set detailed data fields and output the barcode in common image formats such as BMP.
// Prompt: Create a HIBCLICPrimaryDataCodetext, set labeler ID, and generate a BMP image of the barcode.
// Tags: hibc,qrlic,primary-data,barcode,generation,bmp,aspose.barcode,complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a HIBC QRLIC primary data barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the barcode data, generates the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.bmp");

        // Create and configure the primary data codetext for a HIBC QRLIC barcode.
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData()
        };
        // Populate the required data fields.
        complexCodetext.Data.ProductOrCatalogNumber = "12345";
        complexCodetext.Data.LabelerIdentificationCode = "A999";
        complexCodetext.Data.UnitOfMeasureID = 1;

        // Generate the barcode using ComplexBarcodeGenerator and save it as a BMP image.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the X-dimension (module width) in pixels for better visual quality.
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}