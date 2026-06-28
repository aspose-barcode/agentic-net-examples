using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image with custom foreground and background colors,
    /// saves it to a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "custom_color_barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the color of the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image.
            generator.Parameters.BackColor = Color.LightYellow;

            // Increase the image resolution to 300 DPI for higher quality output.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to {outputPath}");
    }
}