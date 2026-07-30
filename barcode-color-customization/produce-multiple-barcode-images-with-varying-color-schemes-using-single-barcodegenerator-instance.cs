// Title: Generate barcode images with different color schemes using a single generator
// Description: Demonstrates how to produce multiple PNG barcode files, each with distinct bar and background colors, by reusing one BarcodeGenerator instance.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and color customization. Developers often need to create batches of barcodes with varying visual styles for branding or UI integration, and this snippet shows the typical workflow for setting bar color, background color, and saving images.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: code128, barcode, color, png, aspose.barcode, generation, generator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating multiple barcode images with different color schemes using a single <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Creates the output directory, configures a barcode generator, iterates through predefined color schemes,
    /// and saves each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the folder where barcode images will be saved.
        string outputFolder = "Barcodes";

        // Ensure the output directory exists.
        if (!System.IO.Directory.Exists(outputFolder))
        {
            System.IO.Directory.CreateDirectory(outputFolder);
        }

        // Initialize a BarcodeGenerator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional common setting: fill the bars completely.
            generator.Parameters.Barcode.FilledBars = true;

            // Array of color schemes: bar color, background color, and target file name.
            var colorSchemes = new (Color BarColor, Color BackColor, string FileName)[]
            {
                (Color.Black,  Color.White,  "barcode_black_on_white.png"),
                (Color.Red,    Color.White,  "barcode_red_on_white.png"),
                (Color.White,  Color.Black,  "barcode_white_on_black.png"),
                (Color.Blue,   Color.Yellow, "barcode_blue_on_yellow.png")
            };

            // Generate and save a barcode for each color scheme using the same generator instance.
            foreach (var scheme in colorSchemes)
            {
                // Apply the current scheme's bar and background colors.
                generator.Parameters.Barcode.BarColor = scheme.BarColor;
                generator.Parameters.BackColor = scheme.BackColor;

                // Build the full file path and save the image.
                string filePath = System.IO.Path.Combine(outputFolder, scheme.FileName);
                generator.Save(filePath);

                // Inform the user about the saved file.
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}