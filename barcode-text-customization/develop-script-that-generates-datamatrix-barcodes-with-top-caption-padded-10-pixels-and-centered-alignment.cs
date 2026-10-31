// Title: Generate DataMatrix barcode with top caption, padding and centered alignment
// Description: Creates a DataMatrix barcode image with a caption displayed above the barcode, padded by 10 pixels on all sides and centered horizontally.
// Category-Description: This example demonstrates how to generate barcodes using Aspose.BarCode, specifically customizing a DataMatrix symbology with a caption. It covers the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG output, and shows how to adjust caption visibility, text, alignment, padding, and font settings—common tasks for developers needing branded or annotated barcode images.
// Prompt: Develop a script that generates DataMatrix barcodes with top caption padded 10 pixels and centered alignment.
// Tags: datamatrix, barcode, caption, padding, alignment, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with a top caption,
/// applying uniform 10‑pixel padding and centering the caption text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode,
    /// saves it as a PNG file, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outPath = Path.Combine(outputDir, "DataMatrixWithCaption.png");

        // Initialize the barcode generator for DataMatrix with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
        {
            // Enable the caption above the barcode and set its text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center the caption horizontally.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Apply uniform 10‑pixel padding on all sides of the caption.
            generator.Parameters.CaptionAbove.Padding.Left.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Right.Pixels = 10f;
            generator.Parameters.CaptionAbove.Padding.Bottom.Pixels = 10f;

            // Set the caption font size.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode as a PNG image.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}