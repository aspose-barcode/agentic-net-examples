// Title: Demonstrate Auto FontMode for PDF417 barcode text
// Description: Shows how to enable FontMode.Auto so Aspose.BarCode automatically determines the optimal font size for each barcode symbol.
// Category-Description: This example belongs to the Aspose.BarCode text formatting category, illustrating the use of FontMode, CodeTextParameters, and Font settings to control barcode text appearance. Developers often need to adjust font size dynamically for different symbologies and output formats; FontMode.Auto simplifies this by calculating the best size automatically. Ideal for generating PDF417 barcodes with readable text in image files.
// Prompt: Apply FontMode.Auto to barcode text so the library automatically calculates optimal font size for each symbol.
// Tags: pdf417, fontmode, auto, png, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a PDF417 barcode with automatic font sizing using FontMode.Auto.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, configures the barcode generator,
    /// saves the barcode image, and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAutoFont_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the generated PNG image.
        string outPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator for PDF417 with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text for Auto Font"))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable automatic font sizing; the library will compute the optimal size.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Specify a custom font family (size is ignored in Auto mode).
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";

            // This size value is retained for reference but has no effect when FontMode.Auto is used.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the barcode as a PNG image to the specified path.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outPath}");
    }
}