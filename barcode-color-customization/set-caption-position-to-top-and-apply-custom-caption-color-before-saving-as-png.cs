// Title: Generate Code128 barcode with top caption and custom color
// Description: Demonstrates how to place a caption above a Code128 barcode, set its color, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting caption visibility, position, text, and color—common tasks when creating branded or informative barcodes for packaging, inventory, or marketing. Developers often need to adjust these visual elements to meet branding guidelines or improve scan reliability.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: barcode symbology, caption positioning, color customization, png output, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, adds a top caption with a custom color,
/// and saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "BarcodeWithTopCaption.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable and configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;          // Show the caption.
            generator.Parameters.CaptionAbove.Text = "Top Caption";   // Set caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Blue; // Apply custom caption color.

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}