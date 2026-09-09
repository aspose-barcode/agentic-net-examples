// Title: Set PDF417 barcode text location below
// Description: Demonstrates how to generate a PDF417 barcode with the human‑readable text placed below the symbol using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeLocation classes. Typical scenarios include creating barcodes for documents, labels, or tickets where the readable text needs to appear beneath the barcode. Developers often need to adjust text placement, dimensions, and other visual properties to meet layout requirements.
// Prompt: Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Below setting.
// Tags: pdf417, textlocation, below, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting the text location to below for a PDF417 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a PDF417 barcode image with text displayed below the barcode.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "Pdf417_Below.png");

        // Initialize the barcode generator for PDF417 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCodeText"))
        {
            // Set the human‑readable text location to below the barcode (default value)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Optional: configure additional PDF417 specific settings
            generator.Parameters.Barcode.Pdf417.Rows = 12;          // Number of rows in the symbol
            generator.Parameters.Barcode.XDimension.Pixels = 2;    // Module width in pixels

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}