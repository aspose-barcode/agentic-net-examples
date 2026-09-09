// Title: Barcode Generation with Automatic Bar Height Fallback
// Description: Demonstrates how to generate Code128 barcodes while automatically falling back to the library's default bar height when a zero value is supplied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It illustrates typical scenarios where developers need to control barcode dimensions programmatically but also want to rely on automatic sizing for certain cases, such as when a bar height of zero is provided. Ideal for learning how to apply conditional parameter settings in barcode creation workflows.
// Prompt: Implement fallback logic to use automatic bar height when BarHeight property is set to zero.
// Tags: barcode, code128, barheight, fallback, automatic, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes with optional custom bar height,
/// falling back to automatic height when the supplied value is zero.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample heights,
    /// and generates corresponding barcode images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarHeightDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Sample bar heights (0 triggers fallback to automatic height)
        float[] heights = new float[] { 0f, 40f, 80f };
        string codeText = "ASPOSE";

        // Generate a barcode for each height value
        foreach (float height in heights)
        {
            string fileName = $"Code128_Height{height}.png";
            string filePath = Path.Combine(outputFolder, fileName);
            GenerateBarcode(codeText, height, filePath);
            Console.WriteLine($"Generated barcode with requested height {height} -> {filePath}");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image using the specified text and bar height.
    /// If <paramref name="barHeight"/> is zero, the generator uses its automatic sizing.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="barHeight">Desired bar height in pixels; zero enables automatic height.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateBarcode(string codeText, float barHeight, string outputPath)
    {
        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set common parameters (e.g., X-dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply custom bar height only when a positive value is provided
            if (barHeight > 0f)
            {
                generator.Parameters.Barcode.BarHeight.Pixels = barHeight;
            }

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}