// Title: Place Text Above DataMatrix Barcode
// Description: Demonstrates how to position human‑readable text above a DataMatrix barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating printable labels where the text must appear above the symbol for better readability. Developers often need to adjust text location, spacing, and output format when integrating barcodes into documents or packaging.
// Prompt: Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = TextLocation.Above.
// Tags: datamatrix, text-above, barcode-generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with the human‑readable text placed above the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixAboveText");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "DataMatrix_Above.png");

        // Create a BarcodeGenerator for DataMatrix with initial code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample Text"))
        {
            // Position the human‑readable text above the DataMatrix symbol
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Optionally adjust the spacing between text and symbol (5 points in this case)
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}