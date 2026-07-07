// Title: Generate Code128 barcode with dark red foreground and verify PNG output
// Description: Demonstrates creating a barcode, setting its foreground color to dark red, saving as PNG, and checking the color in the saved image.
// Category-Description: This example belongs to the Aspose.BarCode generation and image verification category. It shows how to use BarcodeGenerator, set barcode parameters such as BarColor, and work with Aspose.Drawing to inspect pixel data. Developers often need to customize barcode appearance and programmatically validate rendered images for quality assurance or automated testing.
// Prompt: Create a barcode, set ForeColor to dark red, and verify color appears correctly in saved PNG.
// Tags: barcode, code128, forecolor, darkred, png, generation, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a dark red foreground,
/// saves it as a PNG file, and verifies that the saved image contains the expected color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path where the barcode image will be saved
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128, set the code text and dark red bar color, then save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkRed;
            generator.Save(outputPath);
        }

        // Ensure the file was created before attempting verification
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Scan the saved image pixel by pixel to find at least one dark red pixel
        bool darkRedFound = false;
        using (var bitmap = new Aspose.Drawing.Bitmap(outputPath))
        {
            for (int y = 0; y < bitmap.Height && !darkRedFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !darkRedFound; x++)
                {
                    if (bitmap.GetPixel(x, y).ToArgb() == Aspose.Drawing.Color.DarkRed.ToArgb())
                    {
                        darkRedFound = true;
                    }
                }
            }
        }

        // Output verification result
        Console.WriteLine(darkRedFound
            ? "Dark red color verified in barcode image."
            : "Dark red color not found in barcode image.");
    }
}