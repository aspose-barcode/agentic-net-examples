// Title: Barcode Generation with Raw Pixel Matrix Output
// Description: Generates a Code128 barcode and prints its bitmap pixel matrix, where 1 represents black and 0 represents white, useful for debugging detection algorithms.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to create a barcode image using BarcodeGenerator, configure colors, and access the underlying bitmap. Developers often need to inspect raw pixel data for custom detection, image processing, or diagnostic purposes. Key API classes include BarcodeGenerator, EncodeTypes, and Aspose.Drawing.Bitmap.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode symbology, generation, debug, pixel matrix, aspose.barcode, code128, console output

using System;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and outputs the raw pixel matrix for diagnostic purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, prints its dimensions,
    /// and writes the binary pixel matrix (1 = black, 0 = white) to the console.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "1234567890";

        // Create a BarcodeGenerator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set barcode and background colors for a clear black‑on‑white image.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Generate the barcode as a bitmap image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Output image dimensions.
                Console.WriteLine($"Bitmap size: {width}x{height}");
                Console.WriteLine("Raw pixel matrix (1 = black, 0 = white):");

                // Iterate over each pixel row to build and display the binary matrix.
                for (int y = 0; y < height; y++)
                {
                    var line = new StringBuilder(width);
                    for (int x = 0; x < width; x++)
                    {
                        // Retrieve the pixel color at (x, y).
                        var pixelColor = bitmap.GetPixel(x, y);

                        // Append '1' for black pixels, otherwise '0'.
                        line.Append(pixelColor.ToArgb() == Aspose.Drawing.Color.Black.ToArgb() ? '1' : '0');
                    }
                    // Write the constructed line for the current row.
                    Console.WriteLine(line.ToString());
                }
            }
        }
    }
}