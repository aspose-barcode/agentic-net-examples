// Title: Generate Code128 barcode with specific height and bounded image
// Description: Demonstrates creating a Code128 barcode, setting its bar height to 40 mm, and adjusting the image height so the bars stay within the image bounds.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes, AutoSizeMode, and BarCodeImageFormat to produce barcode images. Typical use cases include creating printable barcodes for inventory, shipping, or retail, where precise dimensions and image boundaries are required. Developers often need to control bar height, image size, and output format, making this a common reference for barcode rendering tasks.
// Prompt: Create a barcode, set Height to 40 mm, and ensure bars remain within image bounds.
// Tags: code128, barcode generation, height, image bounds, aspose.barcode, png, barcodegenerator, autosizemode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with a specific bar height
/// and ensures the barcode fits within the image bounds.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create a BarcodeGenerator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing so the explicit BarHeight is applied
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the bar height to 40 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 40f;

            // Increase the image height slightly to keep all bars within the image bounds
            generator.Parameters.ImageHeight.Millimeters = 50f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}