// Title: Barcode Generation with Raw Pixel Matrix Output
// Description: Generates a Code128 barcode, saves it as PNG, and prints the image dimensions followed by a binary pixel matrix where bright pixels are shown as 1 and dark pixels as 0.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It demonstrates using BarcodeGenerator (Aspose.BarCode.Generation) to create a barcode, saving it with BarCodeImageFormat, and loading the result with Aspose.Drawing.Bitmap for pixel-level analysis. Developers often need such diagnostics to verify barcode rendering, troubleshoot detection algorithms, or visualize raw image data during debugging.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode, symbology, generation, debug, pixel matrix, aspose.barcode, aspose.drawing, png, console

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode and outputting its raw pixel matrix for diagnostic purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, writes its size, and prints a binary representation of its pixel brightness.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the barcode text and symbology to use.
        string codeText = "HelloWorld";

        // Create a BarcodeGenerator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated barcode image into a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the PNG image as an Aspose.Drawing.Bitmap for pixel access.
                using (var bitmap = new Bitmap(ms))
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    // Output image dimensions.
                    Console.WriteLine($"Barcode image size: {width}x{height}");
                    Console.WriteLine("Raw pixel matrix (1 = bright, 0 = dark):");

                    // Iterate over each pixel row.
                    for (int y = 0; y < height; y++)
                    {
                        var line = new StringBuilder();

                        // Iterate over each pixel column.
                        for (int x = 0; x < width; x++)
                        {
                            // Retrieve the pixel color.
                            Color color = bitmap.GetPixel(x, y);

                            // Compute average brightness (simple luminance approximation).
                            int brightness = (color.R + color.G + color.B) / 3;

                            // Append '1' for bright pixels, '0' for dark pixels.
                            line.Append(brightness > 128 ? '1' : '0');
                        }

                        // Write the binary line representing the current row.
                        Console.WriteLine(line.ToString());
                    }
                }
            }
        }
    }
}