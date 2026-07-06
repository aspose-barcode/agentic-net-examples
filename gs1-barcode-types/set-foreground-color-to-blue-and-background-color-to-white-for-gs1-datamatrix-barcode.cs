// Title: Set colors for GS1 DataMatrix barcode
// Description: Demonstrates how to set the foreground color to blue and background color to white when generating a GS1 DataMatrix barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization of barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and color properties to control appearance, a common requirement for branding and readability in applications that generate DataMatrix symbols.
// Prompt: Set foreground color to blue and background color to white for a GS1 DataMatrix barcode.
// Tags: datamatrix, gs1, color, foreground, background, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a GS1 DataMatrix barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a GS1 DataMatrix barcode, sets its colors, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the GS1 DataMatrix code text (GTIN example)
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 DataMatrix with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to white
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Define the output file path and format (PNG)
            string outputPath = "gs1datamatrix.png";

            // Save the generated barcode image to the file system
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}