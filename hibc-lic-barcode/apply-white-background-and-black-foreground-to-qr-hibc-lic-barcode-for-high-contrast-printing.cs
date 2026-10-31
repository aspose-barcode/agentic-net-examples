// Title: Generate QR HIBC LIC barcode with white background and black foreground
// Description: Demonstrates how to create a QR HIBC LIC barcode using Aspose.BarCode, setting a white background and black foreground for high‑contrast printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC QR LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and barcode parameter settings (colors, dimensions). Developers often need to customize appearance for readability and printing requirements, and this snippet illustrates typical steps for configuring colors and saving the image.
// Prompt: Apply a white background and black foreground to a QR HIBC LIC barcode for high‑contrast printing.
// Tags: barcode, hibc, qr, lic, color, background, foreground, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR HIBC LIC barcode with customized colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder, configures barcode data,
    /// applies white background and black foreground, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBC_QR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "HIBCLICPrimary.png");

        // Prepare HIBC LIC primary data codetext
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

        // Generate the complex barcode with the specified parameters
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(codetext))
        {
            // Set visual appearance: white background and black bars
            gen.Parameters.BackColor = Color.White;
            gen.Parameters.Barcode.BarColor = Color.Black;

            // Adjust module size (pixel dimension) for better readability
            gen.Parameters.Barcode.XDimension.Pixels = 5;

            // Save the barcode image to the defined path
            gen.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}