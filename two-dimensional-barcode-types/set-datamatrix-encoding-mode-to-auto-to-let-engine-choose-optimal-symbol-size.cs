// Title: Generate DataMatrix barcode with automatic encoding mode
// Description: Demonstrates how to create a DataMatrix barcode using Aspose.BarCode with the encoding mode set to Auto, allowing the engine to select the optimal symbol size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEncodeMode to control encoding behavior. Developers often need to generate DataMatrix barcodes that automatically adjust size based on content, useful in inventory, labeling, and tracking applications.
// Prompt: Set DataMatrix encoding mode to Auto to let the engine choose the optimal symbol size.
// Tags: datamatrix, encoding mode, auto, barcode generation, aspose.barcode, png output

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
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "DataMatrixAuto.png");

        // Initialize the barcode generator for DataMatrix symbology with the desired text
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DataMatrix, "Aspose"))
        {
            // Set the X-dimension (module size) in pixels
            gen.Parameters.Barcode.XDimension.Pixels = 4f;

            // Enable automatic encoding mode so the engine selects the optimal symbol size
            gen.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Save the generated barcode image as PNG
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("DataMatrix barcode saved to: " + outputPath);
    }
}