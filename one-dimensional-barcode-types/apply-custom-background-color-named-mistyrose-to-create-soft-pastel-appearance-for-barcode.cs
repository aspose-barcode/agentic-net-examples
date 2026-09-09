// Title: Apply MistyRose Background Color to Barcode
// Description: Demonstrates how to set a custom background color (MistyRose) for a barcode image using Aspose.BarCode, producing a soft pastel appearance.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and its Parameters to customize visual properties such as background and bar colors. Typical use cases include branding, UI design, and creating visually distinct barcodes for printed or digital media. Developers often need to adjust colors, sizes, and formats to match corporate style guides.
// Prompt: Apply a custom background color named “MistyRose” to create a soft pastel appearance for the barcode.
// Tags: barcode, background color, mistyrose, code128, png, aspose.barcode, generation

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
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// applies the MistyRose background, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary folder to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "barcode_mistyrose.png");

        // Initialize the barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "MistyRoseDemo"))
        {
            // Set the background color to MistyRose for a soft pastel look.
            generator.Parameters.BackColor = Color.MistyRose;

            // Set the barcode (bars) color to black for contrast.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}