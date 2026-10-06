// Title: Set DataMatrix margin (padding) and module size
// Description: Demonstrates configuring the margin around a DataMatrix barcode and adjusting its module size, then saving the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix. It shows typical customization tasks such as setting padding, X‑dimension, and exporting to common image formats. Developers working with barcode rendering often need to fine‑tune visual parameters to improve scan reliability and meet branding requirements.
// Prompt: Provide configuration option to set DataMatrix margin size around the symbol for better scanning.
// Tags: datamatrix, padding, margin, barcode, generation, image, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting padding (margin) and module size for a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, configures a DataMatrix barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting image file
        string outPath = Path.Combine(outputDir, "DataMatrixWithMargin.png");

        // Initialize the barcode generator for DataMatrix with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // Set uniform margin (padding) of 20 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            // Optional: adjust the size of each DataMatrix module (pixel size)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outPath}");
    }
}