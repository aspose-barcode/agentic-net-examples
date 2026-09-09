// Title: Place Text Above DataMatrix Barcode Using Aspose.BarCode
// Description: Demonstrates how to position the human‑readable text above a DataMatrix symbol and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize CodeTextParameters such as location for various symbologies. Developers commonly use BarcodeGenerator, Parameters, and CodeTextParameters to control visual aspects of barcodes like text placement, font, and alignment when creating images, PDFs, or other outputs.
// Prompt: Place barcode text above DataMatrix symbols by assigning CodetextParameters.Location = TextLocation.Above.
// Tags: datamatrix, text placement, png, aspose.barcode, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DataMatrix barcode with the human‑readable text placed above the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode, sets the text location to above the symbol, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixAboveExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "DataMatrix_Above.png");

        // Create a BarcodeGenerator for DataMatrix with initial code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample Text"))
        {
            // Position the human‑readable text above the DataMatrix symbol.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}