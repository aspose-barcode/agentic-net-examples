// Title: Generate a Code128 barcode with 40 mm bar height within image bounds
// Description: Demonstrates creating a Code128 barcode, setting the bar height to 40 mm, and saving it as a PNG while keeping the bars inside the image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode dimensions using the BarcodeGenerator class. Typical use cases include producing high‑resolution barcodes for printing or labeling where precise bar height is required. Developers often need to control size parameters without enabling auto‑sizing to ensure the barcode fits within a predefined image canvas.
// Prompt: Create a barcode, set Height to 40 mm, and ensure bars remain within image bounds.
// Tags: code128, set-height, png, barcodgenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode with a specific bar height
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates the output directory, configures the barcode generator,
    /// sets the bar height to 40 mm, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output folder path relative to the current directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputFolder, "Barcode40mm.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the bar height to 40 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 40f;

            // Keep the default AutoSizeMode (None) so the barcode fits within the image bounds
            // No additional width/height settings are required

            // Save the barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}