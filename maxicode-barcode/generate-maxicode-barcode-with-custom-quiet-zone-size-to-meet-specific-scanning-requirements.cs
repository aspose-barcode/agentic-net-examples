// Title: Generate MaxiCode barcode with custom quiet zone
// Description: Demonstrates creating a MaxiCode barcode and customizing its quiet zone (padding) before saving as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure barcode parameters such as module size, padding, and specific MaxiCode mode using the BarcodeGenerator class. Typical use cases include generating high‑density 2‑D barcodes for logistics and shipping labels where precise quiet zone dimensions are required. Developers often need to adjust these settings to meet scanner specifications and integrate barcode creation into automated workflows.
// Prompt: Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requirements.
// Tags: maxicode, quietzone, barcode generation, png, aspose.barcode, barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a custom quiet zone and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Creates and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCode.png");

        // Initialize a MaxiCode barcode generator with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "SampleData"))
        {
            // Set the module (pixel) size for the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Configure custom quiet zone (padding) on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 15f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Optional: specify the MaxiCode mode (e.g., Mode4 for arbitrary text).
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}