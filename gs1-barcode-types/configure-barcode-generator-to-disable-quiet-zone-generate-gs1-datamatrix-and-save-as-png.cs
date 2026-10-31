// Title: Generate a GS1 DataMatrix barcode without quiet zone and save as PNG
// Description: Demonstrates configuring Aspose.BarCode to produce a GS1 DataMatrix barcode, minimizing margins, and saving the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create GS1 DataMatrix barcodes, a common requirement for supply‑chain labeling where GS1 standards are required. Developers often need to adjust padding and quiet zone settings to meet specific layout constraints, making this pattern useful for many barcode‑related projects.
// Prompt: Configure the barcode generator to disable the quiet zone, generate a GS1 DataMatrix, and save as PNG.
// Tags: datamatrix, gs1, barcode generation, padding, png, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with minimized padding and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, configures the barcode generator,
    /// disables padding (quiet zone cannot be disabled for GS1 DataMatrix), and saves the image.
    /// </summary>
    static void Main()
    {
        // Determine output folder relative to current directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "gs1_datamatrix.png");

        // GS1 DataMatrix requires GS1 formatted code text (AI 01 with 14 digits).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Quiet zone cannot be disabled for GS1 DataMatrix; set padding to zero to reduce margins.
            generator.Parameters.Barcode.Padding.Left.Pixels = 0;
            generator.Parameters.Barcode.Padding.Right.Pixels = 0;
            generator.Parameters.Barcode.Padding.Top.Pixels = 0;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 0;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}