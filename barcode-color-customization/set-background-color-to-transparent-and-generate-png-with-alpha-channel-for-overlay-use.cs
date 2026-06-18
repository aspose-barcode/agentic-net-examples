using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a PNG with a transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, configures its appearance, and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode_overlay.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the image background to transparent so the PNG can be overlaid on other graphics.
            generator.Parameters.BackColor = Color.Transparent;

            // Set the barcode (foreground) color to black. Adjust as needed.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image in PNG format, which supports an alpha channel for transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}