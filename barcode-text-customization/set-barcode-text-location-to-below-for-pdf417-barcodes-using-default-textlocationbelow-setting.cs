// Title: Set PDF417 barcode text location below
// Description: Demonstrates how to position human‑readable text beneath a PDF417 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeLocation to customize barcode appearance. Developers often need to control text placement for readability in printed or digital media, and this snippet shows the default Below setting for PDF417 symbology.
// Prompt: Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Below setting.
// Tags: pdf417, textlocation, below, barcode generation, aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the PDF417 barcode text location example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a PDF417 barcode with the human‑readable text placed below the symbol and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with PDF417 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Configure the text location to appear below the barcode (default setting)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Define output file path
            string outputPath = "pdf417_below.png";

            // Render and save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}