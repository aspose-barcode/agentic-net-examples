// Title: Generate Code128 Barcode with Anti‑Aliasing and Export as PNG
// Description: Demonstrates creating a Code128 barcode, enabling anti‑aliasing, setting high resolution, and saving it as a PNG image for clear on‑screen rendering.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with its Parameters property to configure rendering options such as anti‑aliasing, resolution, and module size. Typical use cases include generating barcodes for web pages, mobile apps, or any UI where crisp, high‑resolution images are required. Developers often need to adjust these settings to meet visual quality standards across different display devices.
// Prompt: Create a barcode, apply anti‑aliasing settings, and export as PNG for crisp screen display.
// Tags: code128, barcode generation, anti-aliasing, png, resolution, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console example that creates a barcode, applies anti‑aliasing,
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and writes the image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable anti-aliasing for smoother appearance
            generator.Parameters.UseAntiAlias = true;

            // Set higher resolution for crisp screen display
            generator.Parameters.Resolution = 300f;

            // Define module (X) size in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}