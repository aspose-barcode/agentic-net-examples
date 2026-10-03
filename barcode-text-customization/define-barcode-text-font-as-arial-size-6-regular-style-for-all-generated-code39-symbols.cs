// Title: Generate Code39 barcodes with custom Arial font
// Description: Demonstrates how to create Code39 barcodes using Aspose.BarCode, setting the barcode text font to Arial, size 6, regular style.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize text appearance. Typical use cases include generating machine-readable labels with specific font requirements for compliance or branding. Developers often need to adjust font family, size, and style when integrating barcode creation into reporting or packaging workflows.
// Prompt: Define barcode text font as Arial, size 6, regular style for all generated Code39 symbols.
// Tags: code39, barcode, font, arial, size6, regular, aspnet, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code39 barcodes with a manually specified Arial font.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Code39 barcodes with custom font settings and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample barcode texts to encode
        string[] samples = { "12345", "CODE39", "A1B2C3" };

        // Iterate over each sample, generate a barcode, and save it as a PNG file
        for (int i = 0; i < samples.Length; i++)
        {
            string text = samples[i];
            string filePath = Path.Combine(outputDir, $"Code39_{i + 1}.png");

            // Initialize the barcode generator with Code39 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, text))
            {
                // Set font mode to manual to allow custom font settings
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

                // Configure the barcode text font: Arial, regular style, size 6 points
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Regular;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 6f;

                // Save the generated barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine("Barcodes generated in: " + outputDir);
    }
}