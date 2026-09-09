// Title: Save barcode with custom colors and timestamped filename
// Description: Demonstrates generating a Code128 barcode, applying custom foreground and background colors, and saving it as a PNG file with a unique timestamped name.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and its Parameters properties. Typical use cases include branding, UI integration, and batch processing where distinct file names are required. Developers often need to set colors, choose symbology, and manage output paths for automated workflows.
// Prompt: Save a barcode with custom colors to a file path that includes a timestamp for uniqueness.
// Tags: code128, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with custom colors and saves it to a uniquely named PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output directory, builds a timestamped file name,
    /// configures barcode colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where barcode images will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Build a unique file name using the current timestamp to avoid overwriting existing files.
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        string filePath = Path.Combine(outputDir, $"barcode_{timestamp}.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the foreground (bar) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow.
            generator.Parameters.BackColor = Color.Yellow;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}