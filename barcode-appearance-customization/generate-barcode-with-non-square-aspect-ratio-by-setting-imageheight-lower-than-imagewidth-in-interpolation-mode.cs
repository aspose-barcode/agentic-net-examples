// Title: Generate non‑square barcode using Interpolation mode
// Description: Demonstrates creating a barcode image where the height is smaller than the width by configuring ImageHeight and ImageWidth in Interpolation auto‑size mode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control barcode dimensions with AutoSizeMode.Interpolation. It showcases key classes like BarcodeGenerator, EncodeTypes, and the Parameters property to adjust size, colors, and output format. Developers often need to produce barcodes with custom aspect ratios for UI layouts, printed labels, or integration into graphics where non‑square dimensions are required.
// Prompt: Generate a barcode with a non‑square aspect ratio by setting ImageHeight lower than ImageWidth in Interpolation mode.
// Tags: code128, barcode generation, image size, interpolation, aspose.barcode, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a non‑square aspect ratio
/// using the Interpolation auto‑size mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "non_square_barcode.png";

        // Initialize a BarcodeGenerator for Code128 with the sample text "123456789".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the auto‑size mode to Interpolation so we can specify exact dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Configure a non‑square aspect ratio: width larger than height.
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points.
            generator.Parameters.ImageHeight.Point = 100f;  // Height in points (lower than width).

            // Optional: set background and barcode colors.
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}