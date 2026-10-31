// Title: Enable NoWrap Mode for DataMatrix Barcode Text
// Description: Demonstrates how to disable text wrapping for a DataMatrix barcode using Aspose.BarCode, ensuring long strings stay on a single line.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode text parameters such as NoWrap for various symbologies. It uses the BarcodeGenerator class with EncodeTypes and BarCodeImageFormat to create image files. Developers often need to control text layout to meet labeling requirements, avoid line breaks, and maintain readability in high‑density barcodes.
// Prompt: Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long strings.
// Tags: datamatrix, nowrap, barcode, text-parameters, image-output, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple demonstration of disabling text wrapping (NoWrap) for a DataMatrix barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with NoWrap enabled and saves it as a PNG image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixNoWrapDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputDir, "DataMatrix_NoWrap.png");

        // Example of a long text string that would normally wrap across multiple lines.
        string longText = "This is a very long text that would normally wrap into multiple lines in the barcode display";

        // Create a barcode generator for DataMatrix using the long text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Disable automatic line wrapping so the entire text stays on a single line.
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}