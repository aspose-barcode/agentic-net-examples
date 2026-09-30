// Title: Generate Code128 barcode and modify background color
// Description: Demonstrates creating a Code128 barcode with default colors, then changing the background to white and saving both images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set visual parameters, and export to PNG. Developers commonly need to customize barcode appearance such as background and foreground colors for branding or readability, and this snippet shows the typical workflow.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: code128, barcode generation, png, background color, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it with default colors,
/// then changing the background to white and saving again.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates barcode images, and writes file paths to console.
    /// </summary>
    static void Main(string[] args)
    {
        // Define and create the output folder for generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // File paths for the two barcode images
        string defaultPath = Path.Combine(outputDir, "barcode_default.png");
        string whiteBgPath = Path.Combine(outputDir, "barcode_whitebg.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode using the generator's default foreground/background colors
            generator.Save(defaultPath, BarCodeImageFormat.Png);

            // Change the background color to white to verify default behavior
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode again with the updated background color
            generator.Save(whiteBgPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the saved barcode images
        Console.WriteLine($"Default barcode saved to: {defaultPath}");
        Console.WriteLine($"White background barcode saved to: {whiteBgPath}");
    }
}