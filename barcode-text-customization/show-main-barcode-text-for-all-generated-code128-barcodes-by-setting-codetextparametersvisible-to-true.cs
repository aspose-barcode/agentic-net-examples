// Title: Generate Code128 Barcodes with Visible Text
// Description: Demonstrates creating multiple Code128 barcodes and saving them as PNG files while ensuring the barcode text is displayed.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code128. It shows setting CodeTextParameters.Location to control text visibility, a common requirement when developers need human‑readable data alongside the barcode in images, PDFs, or other outputs.
// Prompt: Show main barcode text for all generated Code128 barcodes by setting CodetextParameters.Visible to true.
// Tags: code128, barcode generation, text visibility, aspose.barcode, png output, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes with visible text and saving them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a set of Code128 barcodes, ensures the barcode text is displayed below each barcode,
    /// saves them to a temporary directory, and writes the file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code128Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the barcode texts to encode
        string[] texts = { "123456", "ABCDEF", "Code128Test" };

        // Iterate over each text, generate a barcode, and save it as a PNG file
        for (int i = 0; i < texts.Length; i++)
        {
            string text = texts[i];
            string filePath = Path.Combine(outputDir, $"Code128_{i + 1}.png");

            // Initialize the generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Ensure the main barcode text is visible (default location is Below)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the generated barcode image to the specified file path
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Output the location of the generated file
            Console.WriteLine($"Generated barcode saved to: {filePath}");
        }

        // Indicate that all barcodes have been generated successfully
        Console.WriteLine("All Code128 barcodes generated successfully.");
    }
}