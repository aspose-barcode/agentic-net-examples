// Title: Generate QR Code with custom margin padding
// Description: Demonstrates creating a QR Code barcode, adjusting module size, and adding a two‑module margin for visual padding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as XDimension and Padding using the BarcodeGenerator class. Typical use cases include creating QR codes for marketing, product labeling, or authentication where visual spacing is required. Developers often need to control module size and margins to meet design guidelines or scanning requirements.
// Prompt: Generate QR Code barcode and set margin to two modules for visual padding.
// Tags: qr code, barcode generation, margin, padding, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the QR Code generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code with a two‑module margin and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine a temporary output directory and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "qr_margin.png");

        // Create a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR"))
        {
            // Define the size of a single QR module (pixel per module).
            float moduleSize = 4f;
            generator.Parameters.Barcode.XDimension.Pixels = moduleSize;

            // Calculate margin equal to two modules and apply to all sides.
            float margin = 2 * moduleSize;
            generator.Parameters.Barcode.Padding.Left.Pixels = margin;
            generator.Parameters.Barcode.Padding.Top.Pixels = margin;
            generator.Parameters.Barcode.Padding.Right.Pixels = margin;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = margin;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}