// Title: Generate QR HIBC LIC Barcode with White Background and Black Foreground
// Description: Demonstrates how to create a HIBC LIC QR barcode using Aspose.BarCode, applying a white background and black foreground for high‑contrast printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC QR. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and barcode parameter settings (background color, bar color, module size). Developers often need to customize appearance for readability and printing standards, and this snippet provides a concise reference for those scenarios.
/// Prompt: Apply a white background and black foreground to a QR HIBC LIC barcode for high‑contrast printing.
/// Tags: barcode, hibc, lic, qr, background-color, foreground-color, c#, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR HIBC LIC barcode with a white background and black foreground.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLIC_QR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLIC_QR.png");

        // Set up primary data required for a HIBC LIC QR barcode
        HIBCLICPrimaryDataCodetext codetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode with the desired visual settings
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Apply white background and black foreground for high contrast
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Optional: set module size (pixel dimension of a single QR element)
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("HIBC LIC QR barcode generated at:");
        Console.WriteLine(outputPath);
    }
}