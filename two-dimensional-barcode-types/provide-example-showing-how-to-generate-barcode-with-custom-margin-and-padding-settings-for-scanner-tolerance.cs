// Title: Generate Code128 Barcode with Custom Margin and Padding
// Description: Demonstrates how to create a Code128 barcode image with custom padding to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical use cases include creating barcodes for product labeling, inventory tracking, and point‑of‑sale systems where scanner tolerance is critical. Developers often need to adjust padding and module size to meet scanner specifications.
// Prompt: Provide example showing how to generate barcode with custom margin and padding settings for scanner tolerance.
// Tags: code128, barcode generation, padding, margin, scanner tolerance, aspose.barcode, image output, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with custom margin and padding settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary directory, generates the barcode, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE123"))
        {
            // Set the module (X) dimension to control barcode size
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure custom padding (margin) on all sides to increase scanner tolerance
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode image saved to: {outPath}");
    }
}