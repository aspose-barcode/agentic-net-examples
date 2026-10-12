// Title: DataMatrix Barcode NoWrap Text Demonstration
// Description: Shows how to generate a DataMatrix barcode with and without the NoWrap setting to keep long code text on a single line.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text wrapping. Developers often need to prevent line breaks in long strings for DataMatrix and other 2D barcodes, especially when the visual layout must remain compact. The snippet demonstrates default behavior and the NoWrap property for practical barcode rendering scenarios.
// Prompt: Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long strings.
// Tags: datamatrix, barcode, nowrap, codetext, c#, aspose.barcode, image, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating DataMatrix barcodes with default text wrapping and with NoWrap enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary output folder, generates two barcode images,
    /// and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output images
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixNoWrapDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Long code text that would normally wrap in the barcode image
        string longText = "This is a very long codetext that should be displayed in a single line without wrapping in the DataMatrix barcode image.";

        // Paths for the generated images
        string wrapPath = Path.Combine(outputDir, "DataMatrix_Wrap.png");
        string noWrapPath = Path.Combine(outputDir, "DataMatrix_NoWrap.png");

        // Generate barcode with default wrapping (NoWrap = false)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            generator.Save(wrapPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with NoWrap mode enabled to keep text on a single line
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;
            generator.Save(noWrapPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated barcode images
        Console.WriteLine("Generated barcode images:");
        Console.WriteLine(wrapPath);
        Console.WriteLine(noWrapPath);
    }
}