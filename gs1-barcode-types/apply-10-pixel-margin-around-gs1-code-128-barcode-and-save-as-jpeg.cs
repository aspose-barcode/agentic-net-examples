// Title: Apply 10‑pixel margin to GS1 Code 128 barcode and save as JPEG
// Description: Demonstrates how to generate a GS1 Code 128 barcode with a uniform 10‑pixel margin on all sides and export it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode padding parameters. Typical use cases include creating printable barcodes with consistent whitespace for scanners, integrating barcode images into documents, or generating assets for web applications. Developers often need to adjust padding, size, and image format when embedding barcodes in UI or print layouts.
// Prompt: Apply a 10‑pixel margin around a GS1 Code 128 barcode and save as JPEG.
// Tags: barcode, gs1code128, padding, margin, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with a 10‑pixel margin and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, configures barcode padding, and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting JPEG file
        string outputPath = Path.Combine(outputDir, "GS1Code128.jpg");

        // Initialize the barcode generator with GS1 Code 128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(01)12345678901231"))
        {
            // Apply a uniform 10‑pixel margin on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}