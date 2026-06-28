using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom colors and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "custom_barcode.png";

        // Choose the barcode symbology (Code128) and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Brand2023";

        // Create a BarcodeGenerator instance with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the color of the barcode bars (foreground).
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the image (light gray with full opacity).
            generator.Parameters.BackColor = Color.FromArgb(255, 200, 200, 200);

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}