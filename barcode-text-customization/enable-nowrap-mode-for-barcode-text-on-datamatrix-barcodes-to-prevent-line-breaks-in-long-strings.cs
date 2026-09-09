// Title: DataMatrix barcode NoWrap demonstration
// Description: Shows how to disable text wrapping for long strings in DataMatrix barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text layout. Developers often need to generate DataMatrix barcodes with long human‑readable text and must prevent automatic line breaks; this snippet demonstrates the NoWrap property for that purpose.
// Prompt: Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long strings.
// Tags: datamatrix, nowrap, codetext, barcode generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates enabling and disabling NoWrap mode for DataMatrix barcode text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two DataMatrix barcodes: one with default wrapping and one with NoWrap enabled.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixNoWrapDemo");
        Directory.CreateDirectory(outputDir);

        // Long text that would normally wrap across multiple lines in the barcode display area
        string longText = "This is a very long text that would normally wrap into multiple lines in the barcode display area, but we want it in a single line.";

        // -------------------------------------------------
        // Generate barcode with default wrapping (NoWrap = false)
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Explicitly set NoWrap to false (default behavior)
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = false;

            // Save the barcode image as PNG
            generator.Save(Path.Combine(outputDir, "DataMatrixWrap.png"), BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Generate barcode with NoWrap mode enabled (NoWrap = true)
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Enable NoWrap to keep the entire text on a single line
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Save the barcode image as PNG
            generator.Save(Path.Combine(outputDir, "DataMatrixNoWrap.png"), BarCodeImageFormat.Png);
        }

        // Inform the user where the images have been saved
        Console.WriteLine($"Barcode images saved to: {outputDir}");
    }
}