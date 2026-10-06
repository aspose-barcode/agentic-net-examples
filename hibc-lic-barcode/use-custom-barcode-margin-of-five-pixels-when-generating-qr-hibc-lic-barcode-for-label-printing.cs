// Title: Generate QR HIBC LIC Barcode with Custom Margin
// Description: Demonstrates how to generate a QR HIBC LIC barcode with a five‑pixel margin, suitable for label printing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and HIBCLICPrimaryDataCodetext to create healthcare‑industry HIBC LIC QR codes. Developers often need to customize barcode appearance, such as margins and module size, for printing on labels and packaging.
// Prompt: Use a custom barcode margin of five pixels when generating a QR HIBC LIC barcode for label printing.
// Tags: barcode, hibc, qr, margin, label, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Entry point for the QR HIBC LIC barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR HIBC LIC barcode with a five‑pixel margin and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a temporary output folder to store the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCLIC_QR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HIBCLIC_QR.png");

        // Build the primary data required for a HIBC LIC QR barcode
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

        // Initialize the complex barcode generator with the prepared data
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Apply a uniform margin of five pixels on all sides
            generator.Parameters.Barcode.Padding.Left.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Optional: define the size of a single QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}