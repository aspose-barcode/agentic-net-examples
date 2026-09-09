// Title: Generate Han Xin barcode with automatic version selection
// Description: Demonstrates creating a Han Xin barcode for a longer payload, letting the encoder choose the appropriate square version automatically.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, HanXin parameters, and image saving with Aspose.Drawing. Developers often need to generate high‑capacity barcodes and control version and error correction levels, making this a typical reference for such tasks.
// Prompt: Configure Han Xin to use rectangular shape with 15 rows and 40 columns for larger payload.
// Tags: hanxin, barcode, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Han Xin barcode with automatic version selection for a larger payload.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define and create a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "hanxin.png");

        // Sample text that requires a larger Han Xin version
        string codeText = "This is a longer payload to demonstrate Han Xin barcode generation with automatic version selection.";

        // Initialize the barcode generator for Han Xin symbology with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Han Xin supports only square symbols; rectangular shape is not available.
            // Set version to Auto so the encoder selects the smallest square version that fits the payload.
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;
            // Choose an error correction level (L2) suitable for the payload size.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a PNG file
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }
}