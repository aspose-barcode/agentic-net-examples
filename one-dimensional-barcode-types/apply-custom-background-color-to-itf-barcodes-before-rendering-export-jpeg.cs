// Title: Apply custom background color to ITF14 barcode and export as JPEG
// Description: Demonstrates how to generate an ITF14 barcode with a custom background color using Aspose.BarCode and save it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize visual appearance (background and bar colors) of generated barcodes. It uses the BarcodeGenerator class with EncodeTypes.ITF14, modifies rendering parameters, and exports the result via BarCodeImageFormat. Developers often need such examples when creating product labels, packaging, or inventory tags that require specific branding colors.
// Prompt: Apply custom background color to ITF barcodes before rendering, export JPEG.
// Tags: itf, background-color, jpeg, barcode-generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates an ITF14 barcode with a custom background color and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// applies visual customizations, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // ITF14 barcode data (14 digits, including the check digit).
        string codeText = "12345678901231";

        // Full path for the resulting JPEG image.
        string outputPath = Path.Combine(outputDir, "ITF14_CustomBackground.jpg");

        // Initialize the barcode generator with ITF14 symbology and the provided data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set the module (X) dimension to control barcode size (2 pixels per module).
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply a light blue background color to the entire image.
            generator.Parameters.BackColor = Color.LightBlue;

            // Ensure the bars themselves are rendered in black (default, but set explicitly).
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Render and save the barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}