// Title: Generate High‑Resolution Code128 Barcode for Label Printing
// Description: Creates a Code128 barcode image with 300 dpi resolution and fine X‑dimension, suitable for high‑resolution label printing.
// Category-Description: This example demonstrates Aspose.BarCode generation features, focusing on resolution, X‑dimension, padding, and color settings. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce printable barcode images. Developers working on label design, packaging, or high‑quality print workflows often need to configure these parameters to meet printer specifications.
// Prompt: Adjust generator settings to produce a barcode image suitable for high‑resolution printing on labels.
// Tags: code128, barcode generation, high resolution, png, aspose.barcode, printing, label

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode image for label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "HighResLabel.png");

        // Initialize the barcode generator with Code128 symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HIGHRES12345"))
        {
            // Set printer‑friendly resolution (300 dpi)
            generator.Parameters.Resolution = 300f;

            // Configure a small X‑dimension for higher barcode density
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Add uniform padding around the barcode (2 mm on each side)
            generator.Parameters.Barcode.Padding.Left.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Top.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Right.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Millimeters = 2f;

            // Define foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}