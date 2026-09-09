// Title: Generate DataMatrix barcode with top caption, padding, and centered alignment
// Description: Demonstrates how to create a DataMatrix barcode, add a visible caption above it, apply 10‑pixel padding, and center‑align the caption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Typical use cases include adding descriptive text to barcodes for packaging, inventory, or labeling applications. Developers often need to customize caption visibility, padding, and alignment to meet design requirements.
// Prompt: Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and centered alignment.
// Tags: datamatrix, barcode, caption, padding, alignment, aspnet, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with a top caption, padding, and centered alignment.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures caption settings, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define and create a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrix.png");

        // Initialize the barcode generator for DataMatrix symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleCodeText"))
        {
            // Make the caption above the barcode visible and set its text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Apply 10‑pixel padding on all sides of the caption
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 10;
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 10;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 10;

            // Center the caption horizontally relative to the barcode
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the configured barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"DataMatrix barcode with caption saved to: {outputPath}");
    }
}