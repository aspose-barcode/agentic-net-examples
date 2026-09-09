// Title: Set barcode background to white and foreground to black for high contrast
// Description: Demonstrates how to configure a barcode's background and foreground colors using Aspose.BarCode to achieve maximum readability in scanned documents.
// Category-Description: This example belongs to the Aspose.BarCode color customization category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to modify visual properties of generated barcodes. Developers often need to adjust colors for printing or scanning scenarios, ensuring optimal contrast and compliance with document standards.
// Prompt: Set barcode background to white and foreground to black for maximum contrast in scanned documents.
// Tags: barcode, code128, color, contrast, png, aspose.barcodes, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a white background and black bars, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, configures barcode colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Set the full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "barcode_contrast.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the background color to white for a clean canvas
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Set the barcode (foreground) color to black for maximum contrast
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the barcode image as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}