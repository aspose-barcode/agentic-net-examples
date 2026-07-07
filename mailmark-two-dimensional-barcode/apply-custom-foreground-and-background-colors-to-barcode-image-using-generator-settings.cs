// Title: Custom Foreground and Background Colors for Barcode Image
// Description: Demonstrates how to apply custom bar (foreground) and background colors when generating a barcode with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and the Parameters property to customize visual aspects of barcodes. Typical scenarios include branding, UI integration, and printing where specific color schemes are required. Developers often need to adjust bar and background colors to match corporate identity or improve readability on various media.
// Prompt: Apply custom foreground and background colors to the barcode image using generator settings.
// Tags: barcode, color, generation, png, aspose.barcode, csharp

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to set custom foreground (bar) and background colors for a generated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode with blue bars on a light‑gray background and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "custom_color_barcode.png";

        // Initialize a BarcodeGenerator for the Code128 symbology with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the foreground color (the color of the bars) to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode image to the specified file path (default format is PNG).
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}