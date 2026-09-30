// Title: Change DataMatrix barcode foreground color to orange
// Description: Demonstrates how to set the text (foreground) color of a DataMatrix barcode to orange while preserving the default white background.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator and its Parameters to customize visual appearance. Typical use cases include branding, UI integration, and printing where specific foreground colors are required. Developers often need to adjust BarColor, BackColor, and other visual settings without altering the encoded data.
// Prompt: Change only the text color of a DataMatrix barcode to orange while keeping default background.
// Tags: datamatrix, color, foreground, png, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode with an orange foreground color and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// sets the foreground color, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrix_Orange.png");

        // Initialize a DataMatrix barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set only the barcode (foreground) color to orange
            generator.Parameters.Barcode.BarColor = Color.Orange;

            // Background remains the default (white); no explicit BackColor assignment needed

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}