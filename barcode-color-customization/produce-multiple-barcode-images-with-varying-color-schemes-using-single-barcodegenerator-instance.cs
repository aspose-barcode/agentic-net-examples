using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes with different color schemes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images with various bar and background colors.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Define an array of color schemes: bar color, background color, and output file name.
            var colorSchemes = new (Color BarColor, Color BackColor, string FileName)[]
            {
                (Color.Black, Color.White, "barcode_black_on_white.png"),
                (Color.White, Color.Black, "barcode_white_on_black.png"),
                (Color.Blue, Color.Yellow, "barcode_blue_on_yellow.png")
            };

            // Iterate over each scheme, apply colors, and save the barcode image.
            foreach (var scheme in colorSchemes)
            {
                // Set the bar (foreground) color.
                generator.Parameters.Barcode.BarColor = scheme.BarColor;
                // Set the background color.
                generator.Parameters.BackColor = scheme.BackColor;
                // Save the generated barcode to a PNG file.
                generator.Save(scheme.FileName, BarCodeImageFormat.Png);
            }
        }
    }
}