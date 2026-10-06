// Title: Generate a Code128 barcode with 40 mm bar height and bounded image
// Description: Demonstrates creating a Code128 barcode, setting the bar height to 40 mm, and adjusting the image size so the bars stay within the image bounds.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class. Typical use cases include producing high‑resolution barcodes for printing or labeling where precise bar height and image sizing are required. Developers often need to control millimeter‑based measurements to meet printing specifications, and this snippet shows the essential API calls for that purpose.
// Prompt: Create a barcode, set Height to 40 mm, and ensure bars remain within image bounds.
// Tags: code128, barcode generation, height, millimeters, image bounds, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a specific bar height and image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Create the directory if it does not already exist
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting barcode image
        string outputPath = Path.Combine(outputDir, "Barcode40mm.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the bar height to 40 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 40f;

            // Ensure the image height is sufficient to contain the bars plus default padding
            generator.Parameters.ImageHeight.Millimeters = 50f;

            // Optional: set XDimension for better visual quality
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}