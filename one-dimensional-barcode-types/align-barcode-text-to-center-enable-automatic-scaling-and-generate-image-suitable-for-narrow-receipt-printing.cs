// Title: Center-aligned Code128 barcode with auto-scaling for receipt printing
// Description: Demonstrates how to generate a Code128 barcode with centered human‑readable text, automatic scaling, and a small image size suitable for narrow receipt printers.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image format classes to create customized barcodes. Typical use cases include generating compact barcodes for point‑of‑sale receipts, tickets, or labels where space is limited. Developers often need to adjust text alignment, scaling modes, and dimensions to fit specific hardware constraints.
// Prompt: Align barcode text to center, enable automatic scaling, and generate image suitable for narrow receipt printing.
// Tags: code128, barcode generation, autoscaling, receipt printing, image output, aspose.barcode, barcodegenerator, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with centered text, automatic scaling, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// and saves the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Determine and create the output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, "receipt_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Center the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic scaling to fit a narrow receipt width
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = 200f;   // Desired image width in pixels
            generator.Parameters.ImageHeight.Pixels = 100f;  // Desired image height in pixels

            // Use a small X‑dimension for higher barcode density
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Save the generated barcode as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}