// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: Demonstrates how to create a barcode image using Aspose.BarCode with brand-specific colors for the bars and background.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and color customization via Parameters.Barcode.BarColor and Parameters.BackColor. Typical use cases include branding, product labeling, and custom visual design where specific colors are required. Developers often need to adjust these properties to match corporate identity while exporting to common image formats.
// Prompt: Implement method to generate barcode with custom foreground and background colors for branding purposes.
// Tags: barcode, code128, custom colors, branding, image generation, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, defines colors, generates barcode, and reports the saved file path.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the generated PNG
        string filePath = Path.Combine(outputDir, "custom_color_barcode.png");

        // Text to encode in the barcode
        string codeText = "Brand123";

        // Custom foreground (bar) color
        Aspose.Drawing.Color barColor = Aspose.Drawing.Color.FromArgb(0, 128, 255);
        // Custom background color
        Aspose.Drawing.Color backColor = Aspose.Drawing.Color.FromArgb(255, 255, 200);

        // Generate and save the barcode image
        GenerateBarcode(codeText, filePath, barColor, backColor);

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }

    /// <summary>
    /// Generates a Code128 barcode image with specified foreground and background colors.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="filePath">The full path where the image will be saved.</param>
    /// <param name="barColor">The color of the barcode bars.</param>
    /// <param name="backColor">The background color of the image.</param>
    static void GenerateBarcode(string codeText, string filePath, Aspose.Drawing.Color barColor, Aspose.Drawing.Color backColor)
    {
        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply custom colors
            generator.Parameters.Barcode.BarColor = barColor;
            generator.Parameters.BackColor = backColor;

            // Save the barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}