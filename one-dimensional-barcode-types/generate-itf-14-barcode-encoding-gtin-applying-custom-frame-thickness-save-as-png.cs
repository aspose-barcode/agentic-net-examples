// Title: Generate ITF-14 barcode with custom frame thickness and save as PNG
// Description: Demonstrates creating an ITF‑14 barcode that encodes a GTIN, applying a custom frame border, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce product barcodes. Typical use cases include packaging, inventory, and retail labeling where ITF‑14 (GTIN‑14) codes are required. Developers often need to customize visual aspects such as X‑dimension and border styling, which this snippet illustrates.
// Prompt: Generate ITF‑14 barcode encoding GTIN, applying custom frame thickness, save as PNG.
// Tags: itf14, barcode, generation, png, aspose.barcode, encode-types, border, frame-thickness

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an ITF‑14 barcode with a custom frame border and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// applies custom dimensions and border settings, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine and create the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the generated PNG image.
        string filePath = Path.Combine(outputDir, "ITF14_CustomFrame.png");

        // Initialize the barcode generator with ITF‑14 symbology and a sample GTIN value.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the X dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure the barcode to use a frame border and set its thickness.
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            generator.Parameters.Barcode.ITF.BorderThickness.Pixels = 5;

            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"ITF-14 barcode saved to: {filePath}");
    }
}