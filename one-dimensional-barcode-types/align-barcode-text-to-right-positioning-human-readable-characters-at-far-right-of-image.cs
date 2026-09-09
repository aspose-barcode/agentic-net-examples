// Title: Right-aligned human-readable text in a Code128 barcode
// Description: Demonstrates how to align the human-readable text of a barcode to the right edge of the image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and TextAlignment to control text placement. Developers often need to customize barcode appearance for labeling, packaging, or inventory systems, and right-aligning text is a common requirement for consistent layout across different barcode sizes.
// Prompt: Align barcode text to the right, positioning human‑readable characters at the far right of the image.
// Tags: code128, textalignment, rightalign, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with right‑aligned human‑readable text and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, configures the barcode generator,
    /// aligns the text to the right, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRightAlign_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "right_aligned.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text alignment to the right side of the image
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}