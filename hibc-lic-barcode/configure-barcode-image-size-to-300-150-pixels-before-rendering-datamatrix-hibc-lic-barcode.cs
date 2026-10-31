// Title: Configure Image Size for HIBC DataMatrix LIC Barcode
// Description: Demonstrates how to set a 300 × 150 pixel image size before generating a DataMatrix HIBC LIC barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related parameter classes to create HIBC‑compliant DataMatrix barcodes. Typical scenarios include labeling medical devices, pharmaceuticals, and other regulated products where HIBC standards are required. Developers often need to control image dimensions, output formats, and encoding details, making this pattern a common reference for such tasks.
// Prompt: Configure barcode image size to 300 × 150 pixels before rendering a DataMatrix HIBC LIC barcode.
// Tags: barcode, datamatrix, hibc, image-size, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a HIBC DataMatrix LIC barcode with a fixed image size of 300 × 150 pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode data,
    /// sets the image dimensions, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory in the system temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCDataMatrixDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "HIBCDataMatrix.png");

        // Build the primary data codetext required for a HIBC DataMatrix LIC barcode.
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCDataMatrixLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode with the specified image size (300 × 150 pixels).
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Parameters.ImageWidth.Pixels = 300f;   // Set width in pixels.
            generator.Parameters.ImageHeight.Pixels = 150f;  // Set height in pixels.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest; // Adjust scaling mode.

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}