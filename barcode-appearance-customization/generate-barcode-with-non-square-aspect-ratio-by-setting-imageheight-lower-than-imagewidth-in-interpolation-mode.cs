// Title: Generate non‑square barcode using interpolation mode
// Description: Demonstrates how to create a barcode image with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth while using the Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control image dimensions. Developers often need to customize barcode size for UI layouts, printing, or embedding in documents, and this snippet shows typical API usage for adjusting width, height, and X‑dimension.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: code128, barcode generation, image size, interpolation, aspnet, aspose.barcode, png

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
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder for output and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputFile = Path.Combine(outputDir, "NonSquareBarcode.png");

        // Initialize the generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use interpolation to allow custom image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a wider width and a lower height to achieve a non‑square shape
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f; // lower height for non‑square aspect ratio

            // Adjust the X‑dimension (module width) of the barcode bars
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the generated barcode as a PNG file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}