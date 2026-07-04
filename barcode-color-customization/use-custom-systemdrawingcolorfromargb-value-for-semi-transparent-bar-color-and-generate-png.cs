// Title: Generate a semi‑transparent Code128 barcode and save as PNG
// Description: Demonstrates setting a custom semi‑transparent bar color using Color.FromArgb and exporting the barcode to a PNG file.
// Prompt: Use a custom System.Drawing.Color.FromArgb value for semi‑transparent bar color and generate PNG.
// Tags: barcode, code128, color, transparency, png, aspose.barcode, aspose.drawing, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a semi‑transparent bar color and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        const string outputPath = "barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a semi‑transparent blue bar color (alpha 128, red 0, green 0, blue 255)
            generator.Parameters.Barcode.BarColor = Color.FromArgb(128, 0, 0, 255);

            // Save the barcode image as a PNG file at the specified path
            generator.Save(outputPath);
        }

        // Output a confirmation message with the location of the saved file
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}