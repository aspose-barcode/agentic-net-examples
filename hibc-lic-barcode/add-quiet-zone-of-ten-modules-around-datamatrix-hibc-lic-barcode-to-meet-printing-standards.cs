// Title: Add Quiet Zone to DataMatrix HIBC LIC Barcode
// Description: Demonstrates generating a DataMatrix barcode that encodes a HIBC LIC codetext and adding a ten‑module quiet zone around the symbol to meet printing standards.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on HIBC (Health Industry Bar Code) symbologies and DataMatrix encoding. It showcases the use of BarcodeGenerator, EncodeTypes, and HIBCLICPrimaryDataCodetext classes to construct codetext, configure barcode parameters such as XDimension and padding, and output a PNG image. Developers working with healthcare labeling, inventory tracking, or any application requiring precise barcode quiet zones will find this pattern useful.
// Prompt: Add a quiet zone of ten modules around a DataMatrix HIBC LIC barcode to meet printing standards.
// Tags: datamatrix, hibc, lic, quietzone, barcode, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode that contains a HIBC LIC codetext and applies a ten‑module quiet zone around the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, builds the HIBC LIC codetext,
    /// configures the barcode generator, adds padding, and saves the resulting PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCDataMatrixDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCDataMatrix.png");

        // Construct HIBC LIC primary data codetext
        HIBCLICPrimaryDataCodetext hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            // Specify the HIBC type (QR LIC) used for building the codetext
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Retrieve the plain codetext string that will be encoded in the DataMatrix
        string plainCodeText = hibcCodetext.GetConstructedCodetext();

        // Initialize the barcode generator for DataMatrix with the constructed codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, plainCodeText))
        {
            // Set the module size (XDimension) – each module will be 2 pixels wide
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Calculate padding for a quiet zone of ten modules on each side
            // Quiet zone = 10 modules * XDimension
            float padding = generator.Parameters.Barcode.XDimension.Pixels * 10f;
            generator.Parameters.Barcode.Padding.Left.Pixels = padding;
            generator.Parameters.Barcode.Padding.Right.Pixels = padding;
            generator.Parameters.Barcode.Padding.Top.Pixels = padding;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = padding;

            // Optional: define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}