// Title: Generate High-Resolution HIBC LIC DataMatrix Barcode
// Description: Demonstrates creating a DataMatrix HIBC LIC barcode with 300 DPI resolution for clear rendering in medical reports.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext to produce HIBC‑LIC DataMatrix symbols, a common requirement in healthcare labeling. Developers often need to configure resolution, module size, and output format when integrating barcode creation into medical reporting systems.
// Prompt: Configure the barcode generator to use high DPI (300) for sharper DataMatrix HIBC LIC images in medical reports.
// Tags: barcode, datamatrix, hibc, lic, high dpi, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a high‑resolution HIBC LIC DataMatrix barcode
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures barcode data,
    /// sets a 300 DPI resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "HIBCLICDataMatrix.png");

        // Build the primary data required for a HIBC LIC barcode
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode with a high resolution (300 DPI) for sharper output
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            generator.Parameters.Resolution = 300f; // Set DPI
            generator.Parameters.Barcode.XDimension.Millimeters = 1; // Optional module size
            generator.Save(outputPath, BarCodeImageFormat.Png); // Save as PNG
        }

        Console.WriteLine($"HIBC LIC DataMatrix barcode saved to: {outputPath}");
    }
}