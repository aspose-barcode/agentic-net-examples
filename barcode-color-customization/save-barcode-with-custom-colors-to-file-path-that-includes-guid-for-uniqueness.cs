// Title: Save barcode with custom colors and unique GUID filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it to a uniquely named PNG file using a GUID.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class, set color properties, and output to common image formats. Developers often need to create distinct barcode files for batch processing, reporting, or inventory systems, and this pattern shows the typical steps for color customization and unique file naming.
// Prompt: Save a barcode with custom colors to a file path that includes a GUID for uniqueness.
// Tags: barcode, code128, custom colors, png, guid, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom colors
/// and saves it to a uniquely named PNG file using a GUID.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies color settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Build a unique file name using a new GUID and combine it with the output path
        string filePath = Path.Combine(outputDir, $"{Guid.NewGuid()}.png");

        // Initialize the barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode (foreground) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image to yellow
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode as a PNG file at the specified path
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}