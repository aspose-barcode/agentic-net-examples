// Title: Apply semi‑transparent background color to a Code128 barcode
// Description: Demonstrates how to set a custom ARGB background color (255,255,255,0) on a Code128 barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode appearance. Developers often need to modify visual properties such as background color, foreground color, or fonts to match branding or UI requirements. The snippet shows a typical workflow for creating a barcode with a semi‑transparent background and exporting it to a common image format.
// Prompt: Apply a custom background color using ARGB value (255,255,255,0) to create a semi‑transparent effect.
// Tags: code128, background-color, png, barcodelibrary, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a semi‑transparent background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// applies a custom ARGB background color, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory inside the system temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "custom_bg_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a semi‑transparent background color using ARGB (alpha=255, red=255, green=255, blue=0).
            generator.Parameters.BackColor = Color.FromArgb(255, 255, 255, 0);

            // Save the barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}