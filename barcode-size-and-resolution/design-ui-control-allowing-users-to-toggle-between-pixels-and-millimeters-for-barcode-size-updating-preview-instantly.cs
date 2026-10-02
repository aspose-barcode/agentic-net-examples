// Title: Toggle Barcode Size Units Between Pixels and Millimeters
// Description: Demonstrates generating barcode images using Aspose.BarCode with XDimension set in pixels or millimeters, illustrating how a UI could let users switch units and instantly preview the result.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on configuring barcode dimensions via the XDimension property. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce PNG images. Developers often need to adjust barcode size in different measurement units (pixels, millimeters) for UI controls, print layouts, or responsive designs.
// Prompt: Design UI control allowing users to toggle between Pixels and Millimeters for barcode size, updating preview instantly.
// Tags: barcode symbology, generation, unit conversion, pixels, millimeters, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates toggling barcode size units between pixels and millimeters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcode images with different unit settings and outputs their file paths.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Simulate a UI control by creating two barcodes: one using pixels, the other using millimeters.
        // The images are saved to a temporary folder for quick preview.

        // Create a unique temporary directory to store the generated images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeToggleDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two output images.
        string pixelPath = Path.Combine(outputDir, "Barcode_Pixels.png");
        string millimeterPath = Path.Combine(outputDir, "Barcode_Millimeters.png");

        // Generate barcode with XDimension expressed in pixels.
        GenerateBarcode(pixelPath, usePixels: true);

        // Generate barcode with XDimension expressed in millimeters.
        GenerateBarcode(millimeterPath, usePixels: false);

        // Output the locations of the generated files (simulating a preview update in a UI).
        Console.WriteLine("Barcode generated with Pixels unit: " + pixelPath);
        Console.WriteLine("Barcode generated with Millimeters unit: " + millimeterPath);
    }

    /// <summary>
    /// Generates a Code128 barcode image with the XDimension set either in pixels or millimeters.
    /// </summary>
    /// <param name="filePath">Full path where the PNG image will be saved.</param>
    /// <param name="usePixels">If true, XDimension is set in pixels; otherwise, in millimeters.</param>
    static void GenerateBarcode(string filePath, bool usePixels)
    {
        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            if (usePixels)
            {
                // Set XDimension to 3 pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
            }
            else
            {
                // Set XDimension to 2 millimeters.
                generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            }

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}