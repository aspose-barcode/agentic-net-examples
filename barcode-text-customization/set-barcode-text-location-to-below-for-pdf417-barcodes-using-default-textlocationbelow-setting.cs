// Title: Set PDF417 barcode text location below
// Description: Demonstrates how to generate a PDF417 barcode with the human‑readable text positioned below the symbol using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on configuring text placement for 2‑D symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control the CodeLocation property. Developers often need to adjust human‑readable text positions for scanning applications, documentation, or labeling, and this snippet illustrates the default Below setting for PDF417 barcodes.
// Prompt: Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Below setting.
// Tags: pdf417, barcode, textlocation, below, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a PDF417 barcode with the human‑readable text placed below the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures text location, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "pdf417_below.png";

        // Initialize a PDF417 barcode generator within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            // Assign the data that will be encoded into the barcode
            generator.CodeText = "Sample PDF417 Text";

            // Configure the human‑readable text to appear below the barcode (default setting)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image for verification
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}