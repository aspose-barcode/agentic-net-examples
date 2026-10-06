// Title: Generate QR HIBC LIC Barcode with White Background and Black Foreground
// Description: Demonstrates creating a QR HIBC LIC barcode using Aspose.BarCode, applying a white background and black foreground for high‑contrast printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation such as HIBC QR LIC. It showcases the use of key API classes like ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and EncodeTypes to configure barcode data, visual appearance, and output format. Developers working with healthcare or logistics labeling often need to generate high‑contrast QR barcodes for reliable scanning, and this snippet provides a concise reference for that scenario.
// Prompt: Apply a white background and black foreground to a QR HIBC LIC barcode for high‑contrast printing.
// Tags: barcode, hibc, qr, lic, white background, black foreground, aspose.barcode, complexbarcode, png, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a QR HIBC LIC barcode with a white background and black foreground,
/// then saves it as a PNG file to a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode data, configures visual parameters,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary output folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG file.
        string outputPath = Path.Combine(outputDir, "HIBCLICQR.png");

        // Prepare the complex HIBC LIC data structure.
        var complexCodetext = new HIBCLICPrimaryDataCodetext
        {
            // Set the barcode type to QR HIBC LIC.
            BarcodeType = EncodeTypes.HIBCQRLIC,
            // Populate the primary data fields required for HIBC LIC.
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode using the complex barcode generator.
        using (var gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Apply visual styling: white background, black bars.
            gen.Parameters.BackColor = Color.White;
            gen.Parameters.Barcode.BarColor = Color.Black;

            // Set the module size (pixel dimension) for the QR code.
            gen.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the barcode image to the specified path.
            gen.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}