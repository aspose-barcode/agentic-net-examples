// Title: Configure Image Size for DataMatrix HIBC LIC Barcode
// Description: Demonstrates how to set a fixed image size of 300 × 150 pixels when generating a DataMatrix HIBC LIC barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation with the ComplexBarcodeGenerator class. It shows how to configure image dimensions, resolution, and save the result in PNG format—common tasks for developers integrating barcode imaging into packaging, labeling, or inventory systems.
// Prompt: Configure barcode image size to 300 × 150 pixels before rendering a DataMatrix HIBC LIC barcode.
// Tags: datamatrix, hibc, barcode, image-size, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix HIBC LIC barcode with a fixed image size of 300 × 150 pixels and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures image dimensions, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrixHIBC.png");

        // Prepare primary data required for a HIBC LIC barcode.
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

        // Initialize the complex barcode generator with the prepared data.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Configure fixed image dimensions: 300 × 150 pixels.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Optionally set a higher resolution (default is 96 DPI).
            generator.Parameters.Resolution = 300f;

            // Render and save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}