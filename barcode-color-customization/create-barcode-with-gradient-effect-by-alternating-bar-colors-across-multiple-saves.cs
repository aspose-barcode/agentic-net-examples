// Title: Gradient Barcode with Alternating Bar Colors
// Description: Demonstrates creating a Code128 barcode and saving multiple images, each with a different bar color to achieve a gradient effect.
// Prompt: Create a barcode with a gradient effect by alternating bar colors across multiple saves.
// Tags: barcode, code128, gradient, alternating colors, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with alternating bar colors and saving each variation as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images with different bar colors to simulate a gradient effect.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode
        const string codeText = "1234567890";

        // Define the set of colors that will be applied to the barcode bars
        Color[] barColors = new Color[]
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Orange,
            Color.Purple
        };

        // Initialize a barcode generator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure image size using interpolation mode for better scaling
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Loop through each color, apply it, and save the resulting image
            for (int i = 0; i < barColors.Length; i++)
            {
                // Set the current bar color
                generator.Parameters.Barcode.BarColor = barColors[i];

                // Build a unique file name for each image
                string fileName = $"barcode_{i + 1}.png";

                // Save the barcode image in PNG format
                generator.Save(fileName, BarCodeImageFormat.Png);

                // Output status to the console
                Console.WriteLine($"Saved {fileName} with bar color {barColors[i].Name}");
            }
        }

        // Indicate that all barcode images have been generated
        Console.WriteLine("Barcode generation with alternating colors completed.");
    }
}