// Title: Generate Code128 barcode with high‑contrast colors
// Description: Demonstrates creating a Code128 barcode image with a white background and black bars for optimal scanning contrast.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include embedding barcodes in documents, labels, or receipts where clear contrast improves scan reliability. Developers often need to customize colors, sizes, and formats to meet printing and scanning requirements.
/// Prompt: Set barcode background to white and foreground to black for maximum contrast in scanned documents.
/// Tags: barcode symbology, generation, png, background, foreground, contrast, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a white background and black bars,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures the barcode,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Combine directory and file name to get the full output path.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the background color to white for maximum contrast.
            generator.Parameters.BackColor = Color.White;

            // Set the barcode (foreground) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}