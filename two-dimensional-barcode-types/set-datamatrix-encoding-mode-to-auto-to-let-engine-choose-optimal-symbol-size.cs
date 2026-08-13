// Title: Generate DataMatrix barcode with automatic encoding mode
// Description: Demonstrates how to set DataMatrix encoding mode to Auto, allowing the engine to choose the optimal symbol size for the given data, and saves the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DataMatrix and the DataMatrixEncodeMode enumeration. Typical use cases include creating DataMatrix barcodes where the optimal symbol size is not known in advance. Developers often need to configure encoding settings, specify output formats, and save barcodes to files, which this snippet showcases.
// Prompt: Set DataMatrix encoding mode to Auto to let the engine choose the optimal symbol size.
// Tags: datamatrix, encoding mode, auto, barcode generation, aspnet, aspose.barcode, png, file output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with automatic encoding mode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it as PNG, and writes the output path to console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "datamatrix_auto.png");

        // Text to be encoded into the DataMatrix barcode
        string codeText = "Sample DataMatrix";

        // Initialize the barcode generator for DataMatrix with the sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Configure the generator to let the engine automatically select the optimal symbol size
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}