// Title: Generate DataMatrix barcode with top caption, padding, and centered alignment
// Description: Demonstrates how to create a DataMatrix barcode, add a visible top caption, apply 10‑pixel padding, and center‑align the caption text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters to customize barcode appearance. Developers often need to add descriptive text, adjust layout, and export barcodes as images for labeling, packaging, or inventory systems. The snippet shows typical steps: setting symbology, configuring caption visibility, alignment, padding, and saving to PNG.
// Prompt: Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and centered alignment.
// Tags: datamatrix, caption, padding, alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with a top caption,
/// applying 10‑pixel padding and centering the caption text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, configures the barcode generator,
    /// and saves the resulting PNG image.
    /// </summary>
    static void Main()
    {
        // Determine output folder path relative to current directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full file path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "DataMatrix_With_TopCaption.png");

        // Initialize barcode generator for DataMatrix symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Enable and set the top caption text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center-align the caption text
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Apply 10‑pixel padding on all sides of the caption
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 10f;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}