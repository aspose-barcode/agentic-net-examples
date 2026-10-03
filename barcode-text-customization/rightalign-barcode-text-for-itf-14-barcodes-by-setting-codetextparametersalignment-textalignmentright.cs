// Title: Right-align text for ITF‑14 barcode using Aspose.BarCode
// Description: Demonstrates how to generate an ITF‑14 barcode and align its human‑readable text to the right side of the image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters, especially CodeTextParameters. Developers often need to adjust text alignment, size, and other visual properties when creating barcodes for packaging, inventory, or labeling solutions.
// Prompt: Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = TextAlignment.Right.
// Tags: itf-14, text-alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates an ITF‑14 barcode image with the human‑readable text right‑aligned.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates the barcode,
    /// saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "ITF14Demo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputDir, "ITF14_RightAligned.png");

        // Create a BarcodeGenerator for ITF‑14 with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Align the human‑readable text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Optional: increase X‑dimension for better visual clarity
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}