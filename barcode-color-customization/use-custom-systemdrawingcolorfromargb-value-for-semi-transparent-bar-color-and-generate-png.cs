// Title: Generate semi‑transparent barcode with custom ARGB color and save as PNG
// Description: Demonstrates setting a semi‑transparent bar color using System.Drawing.Color.FromArgb and exporting the barcode to a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class. It shows typical use cases such as applying custom colors, adjusting transparency, and saving to common image formats. Developers often need these techniques when integrating barcodes into UI designs or reports that require visual styling.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: barcode symbology, color customization, png output, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating semi‑transparent barcode generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with a semi‑transparent red bar color and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "semiTransparentBarcode.png";

        // Initialize the barcode generator for Code128 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Apply a semi‑transparent red color to the bars (alpha 128 out of 255)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 255, 0, 0);

            // Set a white background to improve visibility of the semi‑transparent bars
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}