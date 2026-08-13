// Title: Generate High‑Resolution DataMatrix HIBC LIC Barcode
// Description: Demonstrates how to create a DataMatrix HIBC LIC barcode with a 600 DPI image resolution for high‑density label printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator class. It shows typical usage of EncodeTypes, PrimaryData, and HIBCLICPrimaryDataCodetext to produce HIBC‑compliant DataMatrix barcodes, a common requirement for pharmaceutical and medical device labeling where high‑density, machine‑readable codes are needed. Developers often need to adjust image resolution, colors, and output formats for printing workflows.
/// Prompt: Adjust the image resolution to 600 DPI when generating a DataMatrix HIBC LIC barcode for high‑density labels.
// Tags: datamatrix, hibc, complexbarcode, resolution, png, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑resolution DataMatrix HIBC LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates and saves the barcode image at 600 DPI.
    /// </summary>
    static void Main()
    {
        // Prepare primary HIBC data (product number, labeler ID, unit of measure)
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Create HIBC DataMatrix LIC complex codetext
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = primaryData
        };

        // Generate the barcode with high resolution (600 DPI)
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set image resolution to 600 DPI
            generator.Parameters.Resolution = 600;

            // Optional: set foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image as PNG
            const string outputPath = "HIBC_DataMatrix_LIC.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputPath} at 600 DPI.");
        }
    }
}