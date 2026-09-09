// Title: Generate QR HIBC LIC Barcode with Custom Margin
// Description: Demonstrates how to generate a QR HIBC LIC barcode with a five‑pixel margin on each side, suitable for label printing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related parameter classes to create healthcare‑industry HIBC QR LIC barcodes. Developers often need to customize layout details such as margins for label printers, and this snippet provides a clear pattern for doing so.
/// Prompt: Use a custom barcode margin of five pixels when generating a QR HIBC LIC barcode for label printing.
/// Tags: barcode, hibc, qr, margin, png, aspose.barcode, label-printing, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a QR HIBC LIC barcode with a custom five‑pixel margin on all sides.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the barcode, applies margin settings, and saves the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory in the system temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBC_QR_Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        // Create primary data codetext for the HIBC QR LIC barcode.
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode and set a custom margin of 5 pixels on each side.
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            gen.Parameters.Barcode.Padding.Left.Pixels = 5f;
            gen.Parameters.Barcode.Padding.Top.Pixels = 5f;
            gen.Parameters.Barcode.Padding.Right.Pixels = 5f;
            gen.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Save the barcode image as PNG.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}