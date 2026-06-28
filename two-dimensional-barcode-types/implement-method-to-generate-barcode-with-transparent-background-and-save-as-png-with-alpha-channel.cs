using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "transparent_barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure the barcode appearance:
            // Set the background color to transparent (preserves alpha channel).
            generator.Parameters.BackColor = Color.Transparent;

            // Set the foreground (bars) color to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG file, which supports transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}