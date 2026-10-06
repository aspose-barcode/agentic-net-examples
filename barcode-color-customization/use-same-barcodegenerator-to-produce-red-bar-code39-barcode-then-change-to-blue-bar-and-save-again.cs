// Title: Generate Code39 barcodes with different colors using Aspose.BarCode
// Description: Demonstrates creating a Code39 barcode, saving it with a red bar, then changing the bar color to blue and saving again.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include generating colored barcodes for branding or visual distinction in reports and documents. Developers often need to modify colors, symbology, and output formats, making this a common reference for quick color changes.
// Prompt: Use the same BarcodeGenerator to produce a red‑bar Code39 barcode, then change to blue‑bar and save again.
// Tags: code39, barcode generation, color, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode in two different colors (red and blue) and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output directory, generates the barcode, changes its color, and saves both versions.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build full file paths for the red and blue barcode images.
        string redPath = Path.Combine(outputDir, "Code39_Red.png");
        string bluePath = Path.Combine(outputDir, "Code39_Blue.png");

        // Initialize the BarcodeGenerator with Code39 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Set the bar color to red and save the first image.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;
            generator.Save(redPath, BarCodeImageFormat.Png);

            // Change the bar color to blue and save the second image.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Save(bluePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated barcode files.
        Console.WriteLine("Barcodes generated:");
        Console.WriteLine(redPath);
        Console.WriteLine(bluePath);
    }
}