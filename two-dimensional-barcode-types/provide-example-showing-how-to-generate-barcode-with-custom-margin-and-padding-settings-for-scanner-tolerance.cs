// Title: Generate Code128 Barcode with Custom Padding (Quiet Zone)
// Description: Demonstrates how to create a Code128 barcode image with custom margin and padding settings to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include customizing the quiet zone (padding) around a barcode to meet scanner requirements, adjusting module size, and exporting to common image formats. Developers working with barcode creation often need to fine‑tune these parameters for reliable scanning in various environments.
// Prompt: Provide example showing how to generate barcode with custom margin and padding settings for scanner tolerance.
// Tags: barcode symbology, generation, padding, margin, scanner tolerance, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with custom padding (quiet zone) to enhance scanner tolerance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, configures barcode generation settings,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode_with_padding.png");

        // Generate a Code128 barcode with custom padding (quiet zone) for scanner tolerance
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set module size (pixel width of the smallest bar)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set padding (quiet zone) on all sides to 20 pixels
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}