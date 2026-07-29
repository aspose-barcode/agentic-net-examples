// Title: Generate High‑Resolution PNG Barcode with Interpolation AutoSizeMode
// Description: Demonstrates configuring AutoSizeMode to Interpolation, setting image dimensions, and saving a high‑resolution PNG barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, AutoSizeMode, and resolution settings. Typical use cases include creating printable barcodes for inventory, shipping, or product labeling where high‑resolution output is required. Developers often need to control image size, DPI, and rendering mode to meet quality standards.
// Prompt: Configure AutoSizeMode to Interpolation, set ImageWidth and ImageHeight, and generate a high‑resolution PNG barcode.
// Tags: code128, generation, png, autosizemode, resolution, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑resolution PNG barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures barcode parameters, generates the image, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "high_res_barcode.png");

        // Create a BarcodeGenerator for Code128 symbology with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired resolution (e.g., 300 DPI) for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Enable interpolation auto‑size mode and specify canvas dimensions in points.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 600f;   // Width in points.
            generator.Parameters.ImageHeight.Point = 300f;  // Height in points.

            // Optional: define barcode and background colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}