// Title: Generate DataMatrix HIBC LIC barcode with 600 DPI resolution
// Description: Demonstrates creating a DataMatrix HIBC LIC barcode at 600 DPI for high‑density label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It shows how to configure resolution, X‑dimension, and save the image using the ComplexBarcodeGenerator and related parameter classes. Developers working with pharmaceutical or logistics labeling often need high‑resolution DataMatrix barcodes for small‑format labels.
// Prompt: Adjust the image resolution to 600 DPI when generating a DataMatrix HIBC LIC barcode for high‑density labels.
// Tags: datamatrix, hibc, lic, resolution, barcode generation, complexbarcode, aspnet, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix HIBC LIC barcode with a 600 DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, "DataMatrix_HIBC_LIC.png");

        // Prepare the primary data required for the HIBC LIC barcode
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Build the complex codetext object specifying the barcode type and data
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = primaryData
        };

        // Generate the barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set image resolution to 600 DPI for high‑density printing
            generator.Parameters.Resolution = 600f;

            // Define the X‑dimension (module size) in millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}