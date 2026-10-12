// Title: Generate High‑Resolution Code128 Barcode for Label Printing
// Description: Creates a Code128 barcode image with 300 dpi resolution and appropriate padding, suitable for high‑resolution label printing.
// Category-Description: This example demonstrates how to configure Aspose.BarCode's BarcodeGenerator for high‑resolution output. It covers setting the image resolution, module size, padding, and colors using the Parameters property. Developers working on label printing, packaging, or any scenario requiring crisp, printable barcodes can use these settings as a reference.
// Prompt: Adjust generator settings to produce a barcode image suitable for high‑resolution printing on labels.
// Tags: code128, high resolution, barcode generation, png, aspose.barcode, printing, label

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑resolution Code128 barcode image suitable for label printing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "highres_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution to 300 dpi for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Configure the X‑dimension (module size) to 0.5 mm for fine detail.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Add uniform padding of 2 mm on all sides to ensure clear whitespace.
            generator.Parameters.Barcode.Padding.Left.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Top.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Right.Millimeters = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Millimeters = 2f;

            // Set the barcode foreground color to black and background to white.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}