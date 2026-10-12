// Title: PDF417 Barcode with Custom Gray Colors
// Description: Demonstrates how to generate a PDF417 barcode image with a light gray background and dark gray bars using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize the visual appearance of barcodes. It utilizes the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to set background and bar colors—common tasks for developers embedding barcodes in UI designs or printed documents.
// Prompt: Set the background color to light gray and bar color to dark gray for a PDF417 barcode.
// Tags: pdf417, barcode, color, background, barcolor, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode with a light gray background and dark gray bars,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies color settings,
    /// saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "pdf417.png");

        // Initialize the barcode generator for PDF417 symbology with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "1234567890"))
        {
            // Set the background color to light gray (RGB 211,211,211).
            generator.Parameters.BackColor = Color.FromArgb(211, 211, 211);

            // Set the bar (foreground) color to dark gray (RGB 169,169,169).
            generator.Parameters.Barcode.BarColor = Color.FromArgb(169, 169, 169);

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}