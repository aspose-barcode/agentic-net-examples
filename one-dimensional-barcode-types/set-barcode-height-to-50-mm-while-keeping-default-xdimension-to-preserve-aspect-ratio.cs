// Title: Set Barcode Height to 50 mm While Preserving Aspect Ratio
// Description: Demonstrates how to set the barcode height to 50 mm using Aspose.BarCode while keeping the default XDimension, ensuring the barcode maintains its aspect ratio.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create a barcode image with custom size settings. Developers often need to adjust barcode height or width for printing or UI requirements while preserving visual proportions.
// Prompt: Set barcode height to 50 mm while keeping default XDimension to preserve aspect ratio.
// Tags: barcode-height, dimension, aspose.barcode, code128, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of setting a barcode's height to 50 mm while retaining the default XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a height of 50 mm and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeHeightExample");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting barcode image.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a barcode generator for Code128 with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set the barcode height to 50 mm; XDimension remains at its default value.
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}