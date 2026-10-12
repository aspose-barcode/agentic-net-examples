// Title: Export PDF417 Barcode with Custom Text to PNG
// Description: Demonstrates generating a PDF417 barcode with customized human‑readable text, alignment, and spacing, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as rows, module size, code‑text location, alignment, spacing, and padding using the BarcodeGenerator and its Parameters API. Typical use cases include creating printable barcodes with tailored captions for inventory, shipping, or ticketing systems. Developers often need to adjust visual layout to match branding or layout requirements.
// Prompt: Export barcodes with customized text to PNG format, preserving alignment and spacing settings in the image.
// Tags: pdf417, barcode, generation, png, custom text, alignment, spacing, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a PDF417 barcode with customized text settings
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures visual parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the barcode data and the target file path
        string codeText = "Custom Text Example";
        string outputPath = Path.Combine(outputDir, "CustomBarcode.png");

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the number of rows for the PDF417 barcode
            generator.Parameters.Barcode.Pdf417.Rows = 12;

            // Define the module (X) size in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the human‑readable text below the barcode
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Align the text to the right side
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Increase the space between the barcode and the text to 40 pixels
            generator.Parameters.Barcode.CodeTextParameters.Space.Pixels = 40f;

            // Optional: add uniform padding around the barcode (5 points on each side)
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}