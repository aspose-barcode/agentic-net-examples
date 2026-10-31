// Title: Set barcode text font to Verdana bold 14pt
// Description: Demonstrates how to generate a Code128 barcode with custom text font settings for better readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include creating barcodes with specific font styles for labeling, packaging, and inventory systems. Developers often need to adjust font family, style, and size to match branding or readability requirements.
// Prompt: Set barcode text font to Verdana, bold style, size 14 pt for improved readability.
// Tags: code128, barcode generation, png output, font customization, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom text font settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory in the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Enable manual font mode to allow explicit font size specification
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;

            // Set the desired font family, style, and size (Verdana, bold, 14 pt)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}