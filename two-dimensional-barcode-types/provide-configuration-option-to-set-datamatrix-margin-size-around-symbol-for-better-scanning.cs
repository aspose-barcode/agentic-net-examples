// Title: Set DataMatrix barcode margin (padding) using Aspose.BarCode
// Description: This example creates a DataMatrix barcode and configures custom margins around the symbol to improve scanning reliability.
// Category-Description: Demonstrates Aspose.BarCode generation for DataMatrix symbology, covering the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and Padding. Typical scenarios include customizing barcode appearance for print or display, where margin size affects scanner performance. Useful for developers needing fine‑tuned barcode layout in .NET applications.
// Prompt: Provide configuration option to set DataMatrix margin size around the symbol for better scanning.
// Tags: datamatrix, barcode, margin, padding, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with custom margin (padding) settings and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the system temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrixMargin.png");
        string outputDir = Path.GetDirectoryName(outputPath);

        // Ensure the output directory exists; create it if necessary.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a BarcodeGenerator for DataMatrix with the desired data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleDataMatrix"))
        {
            // Configure the size of a single module (pixel) in the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Set custom padding (margin) around the barcode symbol on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode with margins saved to: {outputPath}");
    }
}