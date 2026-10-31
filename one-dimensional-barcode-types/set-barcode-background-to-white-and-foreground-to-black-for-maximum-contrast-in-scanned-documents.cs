// Title: Generate a high-contrast Code128 barcode image
// Description: Demonstrates how to create a Code128 barcode with a white background and black bars, suitable for clear scanning.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust colors for optimal contrast in printed or scanned documents, and this snippet shows the typical steps for setting background and foreground colors.
// Prompt: Set barcode background to white and foreground to black for maximum contrast in scanned documents.
// Tags: barcode, code128, color, contrast, image generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with white background and black bars for maximum contrast.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Ensure the target directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background to white and the bar (foreground) color to black for high contrast
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}