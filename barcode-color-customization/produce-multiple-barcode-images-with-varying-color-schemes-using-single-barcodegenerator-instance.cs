// Title: Generate barcode images with multiple color schemes
// Description: Demonstrates using a single BarcodeGenerator to create several barcode PNG files, each with a different foreground and background color.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: barcode, code128, color, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates several barcode images, each using a distinct color scheme,
/// while reusing a single <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode PNG files with different foreground
    /// and background colors and saves them to disk.
    /// </summary>
    static void Main()
    {
        // Define a set of color schemes (foreground bar color, background color) and corresponding output file names.
        var colorSchemes = new (Color BarColor, Color BackColor, string FileName)[]
        {
            (Color.Blue,      Color.White,     "barcode_blue_white.png"),
            (Color.Red,       Color.Yellow,    "barcode_red_yellow.png"),
            (Color.Green,     Color.LightGray, "barcode_green_lightgray.png"),
            (Color.Black,     Color.Pink,      "barcode_black_pink.png"),
            (Color.Orange,    Color.LightBlue, "barcode_orange_lightblue.png")
        };

        // Create a single BarcodeGenerator instance for Code128 with a sample codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            // Optional: increase resolution for better quality output.
            generator.Parameters.Resolution = 300;

            // Iterate through each color scheme, apply the colors, and save the barcode image.
            foreach (var scheme in colorSchemes)
            {
                generator.Parameters.Barcode.BarColor = scheme.BarColor; // Set foreground bar color.
                generator.Parameters.BackColor = scheme.BackColor;      // Set background color.
                generator.Save(scheme.FileName);                       // Save image; file extension determines format (PNG).
            }
        }

        Console.WriteLine("Barcode images generated with varying color schemes.");
    }
}