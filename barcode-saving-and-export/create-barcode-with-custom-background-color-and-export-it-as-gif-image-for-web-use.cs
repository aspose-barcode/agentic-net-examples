using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a GIF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures its appearance,
    /// saves it to a file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.gif";

        // Initialize a BarcodeGenerator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the background color of the image (light gray).
            generator.Parameters.BackColor = Color.LightGray;

            // Set the foreground (bar) color of the barcode (dark blue).
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;

            // Save the barcode as a GIF image, which is suitable for web usage.
            generator.Save(outputPath, BarCodeImageFormat.Gif);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}