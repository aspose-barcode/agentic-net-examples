// Title: Automatic Barcode Size Adjustment Based on Code Text Length
// Description: Demonstrates how Aspose.BarCode automatically adjusts the barcode image dimensions according to the length of the CodeText without manual size settings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Code128. It shows default sizing behavior where the library calculates optimal image width and height based on the supplied CodeText. Developers working with dynamic barcode content often need to generate images that fit varying data lengths without manually calculating dimensions.
// Prompt: Enable automatic size adjustment so dimensions adapt based on the length of CodeText.
// Tags: barcode, code128, autosize, dimensions, aspnet, aspose.barcode, image generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes with automatic size adjustment based on the length of the supplied CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images for various CodeText lengths and displays their dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AutoSizeBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define a set of CodeText strings with varying lengths.
        string[] codeTexts = new string[]
        {
            "A",
            "ABC123",
            "LongerCodeTextExample1234567890"
        };

        // Iterate over each CodeText, generate a barcode, and save it as PNG.
        foreach (string text in codeTexts)
        {
            // Build the output file name using the length of the CodeText for easy identification.
            string filePath = Path.Combine(outputDir, $"barcode_{text.Length}.png");

            // Initialize the BarcodeGenerator with Code128 symbology and the current CodeText.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // No explicit size settings – default behavior adapts to CodeText length.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Load the saved image to retrieve its actual dimensions.
            using (var img = Image.FromFile(filePath))
            {
                Console.WriteLine($"Saved '{Path.GetFileName(filePath)}' for CodeText length {text.Length}: {img.Width}x{img.Height} pixels");
            }
        }

        // Inform the user where all generated barcode images are stored.
        Console.WriteLine($"All barcode images saved to: {outputDir}");
    }
}