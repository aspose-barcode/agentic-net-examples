// Title: Generate PDF417 barcode with custom gray colors
// Description: Demonstrates how to set a light gray background and dark gray bar color for a PDF417 barcode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize visual appearance of barcodes using the BarcodeGenerator class. It shows setting background and bar colors for common symbologies like PDF417, useful for developers needing branded or themed barcode images in applications or reports. Typical use cases include creating printable barcodes with specific color schemes for branding or accessibility.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, background, barcolor, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with custom gray colors and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, configures colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "pdf417_gray.png");

        // Initialize the barcode generator for PDF417 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "123456789"))
        {
            // Set the background color to light gray
            generator.Parameters.BackColor = Color.LightGray;

            // Set the bar (foreground) color to dark gray
            generator.Parameters.Barcode.BarColor = Color.DarkGray;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}