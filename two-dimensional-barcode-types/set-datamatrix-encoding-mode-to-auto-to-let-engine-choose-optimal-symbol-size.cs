// Title: Generate DataMatrix Barcode with Auto Encoding Mode
// Description: Demonstrates how to create a DataMatrix barcode using Aspose.BarCode and let the engine automatically select the optimal symbol size by setting the encoding mode to Auto.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, DataMatrixEncodeMode, and BarCodeImageFormat to produce a PNG image. Typical scenarios include generating compact, machine‑readable DataMatrix codes for inventory, packaging, or tracking where optimal symbol size is desired. Developers often need to adjust dimensions and encoding settings to meet specific layout or scanning requirements.
// Prompt: Set DataMatrix encoding mode to Auto to let the engine choose the optimal symbol size.
// Tags: datamatrix, barcode, generation, auto, encoding, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode image with automatic encoding mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path where the barcode image will be saved.
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrixAuto.png");

        // Create a BarcodeGenerator for DataMatrix symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Aspose常に先を行く"))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Enable automatic encoding mode so the engine selects the optimal symbol size.
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}