// Title: Add quiet zone to HIBC DataMatrix LIC barcode
// Description: Demonstrates how to generate a HIBC DataMatrix LIC barcode with a ten‑module quiet zone using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC DataMatrix LIC. It showcases the use of ComplexBarcodeGenerator and related parameter settings (XDimension, Padding) to meet printing standards. Developers creating healthcare or logistics labels often need to adjust quiet zones for compliance.
// Prompt: Add a quiet zone of ten modules around a DataMatrix HIBC LIC barcode to meet printing standards.
// Tags: datamatrix, hibc, quiet zone, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a HIBC DataMatrix LIC barcode with a ten‑module quiet zone.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies quiet zone, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLICDataMatrixDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLICDataMatrix.png");

        // Create primary data codetext for HIBC DataMatrix LIC
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

        // Generate barcode with a quiet zone of ten modules
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            float xDimension = 5f; // Module size in pixels
            gen.Parameters.Barcode.XDimension.Pixels = xDimension;

            // Calculate quiet zone size (ten modules) and apply to all sides
            float quietZone = xDimension * 10f;
            gen.Parameters.Barcode.Padding.Left.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Top.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Right.Pixels = quietZone;
            gen.Parameters.Barcode.Padding.Bottom.Pixels = quietZone;

            // Save the generated barcode image to the specified path
            gen.Save(outputPath);
        }

        Console.WriteLine($"HIBC DataMatrix LIC barcode saved to: {outputPath}");
    }
}