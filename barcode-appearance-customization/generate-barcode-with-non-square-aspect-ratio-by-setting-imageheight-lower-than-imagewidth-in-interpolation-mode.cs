// Title: Generate non‑square barcode using interpolation scaling
// Description: Demonstrates how to create a barcode image with a rectangular aspect ratio by setting ImageHeight lower than ImageWidth and using the Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control barcode dimensions. Developers often need to produce barcodes with custom sizes for UI layouts, printed labels, or web graphics, and this snippet shows the typical API calls for adjusting width, height, and scaling behavior.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: code128, barcode generation, image scaling, interpolation, png, aspnet.barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a non‑square aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates the barcode, saves as PNG, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "NonSquareBarcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use Interpolation mode to allow custom scaling of the image
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a rectangular size: width larger than height
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}