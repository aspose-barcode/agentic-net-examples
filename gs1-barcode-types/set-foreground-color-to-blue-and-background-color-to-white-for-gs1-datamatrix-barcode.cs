// Title: Generate GS1 DataMatrix barcode with custom colors
// Description: Demonstrates how to create a GS1 DataMatrix barcode, set its foreground color to blue and background color to white, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and rendering parameters. Typical scenarios include product labeling, packaging, and inventory systems where GS1 DataMatrix codes are required. Developers often need to customize visual appearance (colors, size) before exporting to common image formats.
// Prompt: Set foreground color to blue and background color to white for a GS1 DataMatrix barcode.
// Tags: gs1, datamatrix, barcode, color, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies colors, saves to file, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "GS1DataMatrix.png");

        // Sample GS1 DataMatrix code text (includes Application Identifier 01)
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}