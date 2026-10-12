// Title: Generate Code128 barcode with visible text positioned above the bars
// Description: Demonstrates how to create a Code128 barcode, enable the human‑readable text, and place that text above the barcode with a small vertical offset.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical use cases include labeling products, tickets, or documents where the encoded value must be displayed alongside the bars. Developers often need to adjust text location, spacing, and image format when integrating barcodes into applications.
// Prompt: Generate a barcode, set ShowCodeText to true, and position text above bars with a small vertical offset.
// Tags: code128, showcodetext, textposition, png, aspose.barcode, barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with visible text positioned above the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates the barcode, saves it as PNG, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Build a temporary folder path for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable the human‑readable text and position it above the bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;
            // Apply a small vertical offset (5 points) between the text and the bars
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}