// Title: Apply custom foreground color to barcode using hexadecimal value
// Description: Demonstrates how to set a barcode's foreground color to a specific hex value (#FF6600) using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with color settings. It uses the BarcodeGenerator class and its Parameters.Barcode.BarColor property to apply branding colors. Developers often need to match corporate visual identity when generating barcodes for packaging, labels, or documents, and this snippet shows the typical steps for setting colors and exporting the image.
// Prompt: Apply custom foreground color using hexadecimal value #FF6600 to match corporate branding.
// Tags: barcode, color, hex, code128, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting a custom foreground color for a barcode and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with a corporate orange color and writes the file path.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode's foreground (bar) color to the corporate orange #FF6600 (RGB 255,102,0)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 102, 0);

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image for verification
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}