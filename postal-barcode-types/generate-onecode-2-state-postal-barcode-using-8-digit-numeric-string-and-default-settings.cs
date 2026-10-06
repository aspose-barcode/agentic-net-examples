// Title: Generate OneCode 2‑state postal barcode with 8‑digit numeric string
// Description: Demonstrates creating a OneCode 2‑state postal barcode from an 8‑digit numeric value using default settings and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.OneCode to produce postal barcodes. Typical use cases include encoding postal routing information for mail automation. Developers often need to generate barcode images for printing or digital workflows, and this snippet shows the basic steps: instantiate the generator, optionally configure properties, and save the image.
// Prompt: Generate a OneCode 2‑state postal barcode using an 8‑digit numeric string and default settings.
// Tags: onecode, postal barcode, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode 2‑state postal barcode from an 8‑digit numeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode image and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "OneCodeDemo");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Build the full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "OneCode8Digits.png");

        try
        {
            // Initialize the generator with OneCode symbology and an 8‑digit numeric string
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, "12345678"))
            {
                // No additional settings are changed; default configuration is used
                generator.Save(outputPath, BarCodeImageFormat.Png); // Save the barcode as PNG
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"OneCode barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation
            Console.WriteLine($"Error generating OneCode barcode: {ex.Message}");
        }
    }
}