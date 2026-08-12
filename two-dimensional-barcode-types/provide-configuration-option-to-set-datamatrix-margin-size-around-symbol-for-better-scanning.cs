// Title: Set DataMatrix Quiet Zone (Margin) Using Aspose.BarCode
// Description: Demonstrates how to increase the quiet zone around a DataMatrix barcode by configuring padding, improving scan reliability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating barcode appearance customization. It shows how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to modify visual parameters such as padding (quiet zone). Developers often need to adjust margins for better scanner detection or to meet printing specifications, and this snippet provides a concise reference.
// Prompt: Provide configuration option to set DataMatrix margin size around the symbol for better scanning.
// Tags: datamatrix, margin, quietzone, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting a custom margin (quiet zone) for a DataMatrix barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with increased padding and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine a temporary output file path.
        string outputPath = Path.Combine(Path.GetTempPath(), "datamatrix_margin.png");

        // Initialize the barcode generator for DataMatrix with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Configure the quiet zone (margin) around the symbol.
            // Each side is set to 10 points (approximately 3.5 mm).
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"DataMatrix barcode with increased margin saved to: {outputPath}");
    }
}