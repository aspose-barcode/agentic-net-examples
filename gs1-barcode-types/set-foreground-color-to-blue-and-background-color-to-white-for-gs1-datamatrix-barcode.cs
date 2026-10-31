// Title: Set Colors for GS1 DataMatrix Barcode using Aspose.BarCode
// Description: Demonstrates how to generate a GS1 DataMatrix barcode with a blue foreground and white background.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating color customization for generated barcodes. It uses the BarcodeGenerator class with EncodeTypes.GS1DataMatrix, showing typical use cases such as setting BarColor and BackColor properties to meet branding or readability requirements. Developers often need to adjust visual appearance of barcodes for print or screen display.
// Prompt: Set foreground color to blue and background color to white for a GS1 DataMatrix barcode.
// Tags: gs1datamatrix, color, foreground, background, png, aspose.barcode, barcodegeneration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 DataMatrix barcode with custom foreground and background colors and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "GS1DataMatrix.png");

        // Sample GS1 DataMatrix code text with Application Identifier (01).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the foreground (bars) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}