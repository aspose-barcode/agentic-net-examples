// Title: Apply MistyRose background color to a Code128 barcode
// Description: Demonstrates how to set a custom pastel background color for a Code128 barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters to customize visual appearance such as background color. Developers often need to adjust barcode styling for branding or UI integration, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Apply a custom background color named “MistyRose” to create a soft pastel appearance for the barcode.
// Tags: code128, background-color, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a MistyRose background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, configures the barcode generator,
    /// applies the custom background color, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output file.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the PNG image.
        string outputPath = Path.Combine(outputDir, "barcode_mistyrose.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "MistyRoseDemo"))
        {
            // Set the background color to MistyRose (soft pastel).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 228, 225);

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}