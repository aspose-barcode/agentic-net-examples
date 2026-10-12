// Title: Generate PDF417 Barcode with Automatic Font Sizing (FontMode.Auto)
// Description: Demonstrates how to generate a PDF417 barcode where the library automatically determines the optimal font size for the human‑readable text using FontMode.Auto.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to create barcodes with customizable text appearance. Developers often need to render barcodes with readable text while ensuring the font fits each symbol; FontMode.Auto lets the library calculate the best size automatically. Typical scenarios include labeling, inventory tracking, and document encoding where visual clarity of the code text is required.
// Prompt: Apply FontMode.Auto to barcode text so the library automatically calculates optimal font size for each symbol.
// Tags: pdf417, fontmode, autosize, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a PDF417 barcode with automatic font sizing using FontMode.Auto.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Determine a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "BarcodeFontModeAuto.png");

        // Initialize the barcode generator for PDF417 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample barcode text for FontMode.Auto"))
        {
            // Configure PDF417 specific parameters.
            generator.Parameters.Barcode.Pdf417.Rows = 12;               // Number of rows in the symbol.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;        // Module width in pixels.

            // Set up automatic font sizing for the human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Lucida Handwriting";
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Underline;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f; // Ignored when FontMode.Auto is used.

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode generated at: " + outputPath);
    }
}