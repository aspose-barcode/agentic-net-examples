// Title: Generate barcode with auto-sized height using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode where BarCodeHeight is left at zero, allowing the library to auto‑size the barcode based on its content.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include creating barcodes for product labels, shipping documents, and receipts where automatic sizing simplifies layout. Developers often need to generate barcodes quickly without manually calculating dimensions, and this pattern provides a concise solution.
// Prompt: Generate barcode with BarCodeHeight zero to enable auto‑size based on content, using default units.
// Tags: barcode, code128, autosize, height, aspose.barcode, image, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with automatic height sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates a barcode, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Build a unique temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the output PNG image.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // BarCodeHeight is left at its default value (zero), so the library auto‑sizes the height based on the content.
            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}