// Title: Change DataMatrix barcode text color to orange
// Description: Demonstrates how to set the human‑readable text color of a DataMatrix barcode to orange while preserving the default background.
// Prompt: Change only the text color of a DataMatrix barcode to orange while keeping default background.
// Tags: datamatrix, color, textcolor, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DataMatrix barcode with orange text color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode, sets the code text color to orange, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "datamatrix.png";

        // Initialize a DataMatrix barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample Text"))
        {
            // Set only the human‑readable text (code text) color to orange
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

            // Save the barcode image as PNG; background remains the default color
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output a confirmation message with the saved file location
        Console.WriteLine($"DataMatrix barcode saved to {outputPath}");
    }
}