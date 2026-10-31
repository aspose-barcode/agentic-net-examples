// Title: Generate MaxiCode barcode with transparent background using ComplexBarcodeGenerator
// Description: Demonstrates how to create a MaxiCode barcode (Mode 2) with a transparent background and save it as a PNG image, suitable for overlaying on UI components.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2. It shows how to configure barcode parameters such as background color and export to common image formats. Developers working with advanced barcode symbologies, custom rendering, or UI integration often need to generate transparent images for seamless overlay.
// Prompt: Configure ComplexBarcodeGenerator to output a barcode image with transparent background for UI component overlay.
// Tags: maxicode, complexbarcode, transparent background, png, aspnet.barcode, generation, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a MaxiCode barcode,
    /// saves it as a PNG with a transparent background, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "ComplexBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "maxicode_transparent.png");

        // Create a simple MaxiCode codetext (Mode 2) with required fields
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit postal code
            CountryCode = 56,         // Numeric country identifier
            ServiceCategory = 999     // Service category value
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Set the background to transparent for UI overlay scenarios
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG image preserving transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}