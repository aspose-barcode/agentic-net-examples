// Title: Generate low‑resolution Code128 barcode (72 dpi) and verify image size
// Description: Demonstrates setting the BarcodeGenerator resolution to 72 dpi, creating a Code128 barcode, and saving it as a PNG image for low‑resolution display scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode rendering parameters such as resolution. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common for developers needing custom image quality, size, or DPI settings when producing barcodes for web or print.
// Prompt: Set BarcodeGenerator resolution to 72 dpi, test barcode generation meets low‑resolution display requirements.
// Tags: code128, barcode generation, png, resolution, aspose.barcodes, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a low‑resolution Code128 barcode and inspecting its image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, generates a 72 dpi Code128 barcode, saves it as PNG, and outputs the file path and image size.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeLowRes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define output file path
        string barcodePath = Path.Combine(outputFolder, "code128_72dpi.png");

        // Generate a Code128 barcode with 72 dpi resolution
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "LOWRES123"))
        {
            // Set low resolution (72 DPI)
            generator.Parameters.Resolution = 72f;
            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Load the generated image to verify dimensions
        using (Bitmap bitmap = new Bitmap(barcodePath))
        {
            Console.WriteLine($"Barcode saved to: {barcodePath}");
            Console.WriteLine($"Image dimensions (pixels): Width={bitmap.Width}, Height={bitmap.Height}");
        }

        // Clean up: optionally delete the temporary folder
        // Directory.Delete(outputFolder, true);
    }
}