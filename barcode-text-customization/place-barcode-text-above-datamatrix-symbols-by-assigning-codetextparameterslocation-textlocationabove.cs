// Title: Place Text Above DataMatrix Barcode
// Description: Demonstrates positioning the human‑readable text above a DataMatrix barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize CodeTextParameters such as location for various symbologies. It shows the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create and save a DataMatrix symbol with text placed above. Developers often need to adjust text placement for readability or branding in printed labels.
// Prompt: Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = TextLocation.Above.
// Tags: datamatrix, text location, barcode generation, png, aspose.barcode, codetextparameters, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode with the code text positioned above the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates the barcode, saves it as PNG, and writes the file path.
    /// </summary>
    static void Main()
    {
        // Determine temporary output folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixAbove");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "DataMatrixAbove.png");

        // Initialize generator for DataMatrix with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample Text"))
        {
            // Set the code text location to appear above the DataMatrix symbol
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}