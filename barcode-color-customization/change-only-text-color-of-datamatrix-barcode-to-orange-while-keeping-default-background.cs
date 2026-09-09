// Title: Change DataMatrix barcode text color to orange
// Description: Demonstrates how to set the code text color of a DataMatrix barcode to orange while preserving the default background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual aspects of barcodes such as text color. It uses the BarcodeGenerator class with EncodeTypes.DataMatrix and modifies the CodeTextParameters.Color property. Developers often need to match branding colors or improve readability, and this pattern shows the typical steps for color customization before saving the image.
// Prompt: Change only the text color of a DataMatrix barcode to orange while keeping default background.
// Tags: datamatrix, barcode, color, textcolor, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DataMatrix barcode with orange text color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixColorDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "DataMatrix_OrangeText.png");

        // Create a BarcodeGenerator for DataMatrix with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "123456"))
        {
            // Set only the code text (human‑readable) color to orange; background remains default
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

            // Save the generated barcode image as PNG to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}