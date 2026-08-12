// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: Demonstrates how to create a barcode image using Aspose.BarCode, applying custom bar and background colors for branding.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and color parameters. Typical use cases include branding, custom UI themes, and printing where specific colors are required. Developers often need to adjust BarColor and BackColor to match corporate identity while exporting to common image formats.
// Prompt: Implement method to generate barcode with custom foreground and background colors for branding purposes.
// Tags: barcode symbology, color customization, png output, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    // Generates a barcode image with custom foreground (bar) and background colors.
    // Parameters:
    //   codeText   - Text to encode in the barcode.
    //   outputPath - Full file path where the image will be saved.
    //   foreColor  - Color of the barcode bars.
    //   backColor  - Background color of the image.
    static void GenerateBarcode(string codeText, string outputPath, Color foreColor, Color backColor)
    {
        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create the barcode generator for Code128 (you can change the symbology as needed).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = codeText;

            // Set custom colors.
            generator.Parameters.Barcode.BarColor = foreColor; // foreground (bars)
            generator.Parameters.BackColor = backColor;       // background

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Entry point that calls GenerateBarcode with sample data and reports success or errors.
    /// </summary>
    static void Main()
    {
        // Sample usage of the GenerateBarcode method.
        string sampleText = "123ABC";
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "custom_barcode.png");
        Color foreground = Color.DarkBlue;      // Custom bar color.
        Color background = Color.LightYellow;  // Custom background color.

        try
        {
            GenerateBarcode(sampleText, outputFile, foreground, background);
            Console.WriteLine($"Barcode generated successfully: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}