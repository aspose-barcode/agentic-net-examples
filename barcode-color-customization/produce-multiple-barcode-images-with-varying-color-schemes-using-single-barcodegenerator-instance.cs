// Title: Generate Multiple Barcode Images with Different Color Schemes Using a Single Generator
// Description: Demonstrates how to create several barcode PNG files, each with a distinct bar and background color, by reusing one BarcodeGenerator instance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure visual properties such as BarColor and BackColor on the BarcodeGenerator. Developers often need to produce multiple barcode variations for branding, UI themes, or printing requirements, and this pattern shows the efficient reuse of a single generator to avoid repeated object creation. Key classes used are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Produce multiple barcode images with varying color schemes using a single BarcodeGenerator instance.
// Tags: barcode, color scheme, code128, png, aspose.barcode, generation, barcolor, backcolor, reuse instance

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcode images with various color combinations
/// using a single <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, defines barcode data,
    /// iterates through a list of color schemes, and saves each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where barcode images will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the barcode content and symbology
        string codeText = "Aspose123";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // List of color schemes: bar color, background color, and target file name
        var colorSchemes = new List<(Color barColor, Color backColor, string fileName)>
        {
            (Color.Black, Color.White, "Barcode_BlackOnWhite.png"),
            (Color.White, Color.Black, "Barcode_WhiteOnBlack.png"),
            (Color.Red, Color.Yellow, "Barcode_RedOnYellow.png"),
            (Color.Blue, Color.LightGray, "Barcode_BlueOnLightGray.png")
        };

        // Reuse a single BarcodeGenerator instance for all color variations
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a common parameter (optional) – pixel width of each barcode module
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Iterate through each color scheme, apply colors, and save the image
            foreach (var scheme in colorSchemes)
            {
                generator.Parameters.Barcode.BarColor = scheme.barColor;
                generator.Parameters.BackColor = scheme.backColor;

                string filePath = Path.Combine(outputDir, scheme.fileName);
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode to {filePath}");
            }
        }
    }
}