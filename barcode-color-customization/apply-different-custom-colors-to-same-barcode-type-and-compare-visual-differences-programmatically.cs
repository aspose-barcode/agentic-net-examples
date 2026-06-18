using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating two barcodes with different colors,
/// saving them as PNG files, and comparing the resulting images pixel by pixel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcodes, saves them, and reports pixel differences.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology (Code 128) and the text to encode.
        BaseEncodeType symbology = EncodeTypes.Code128;
        string codeText = "123456";

        // Generate the first barcode: red bars on a white background.
        string path1 = "barcode_red.png";
        GenerateAndSaveBarcode(symbology, codeText, Color.Red, Color.White, path1);

        // Generate the second barcode: green bars on a black background.
        string path2 = "barcode_green.png";
        GenerateAndSaveBarcode(symbology, codeText, Color.Green, Color.Black, path2);

        // Load the generated images for pixel-by-pixel comparison.
        using (var bmp1 = new Bitmap(path1))
        using (var bmp2 = new Bitmap(path2))
        {
            // Verify that both images share the same dimensions before comparing.
            if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            {
                Console.WriteLine("Images have different dimensions and cannot be compared.");
                return;
            }

            // Count the number of pixels that differ between the two images.
            int diffCount = 0;
            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    // Compare the color of each corresponding pixel.
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        diffCount++;
                    }
                }
            }

            // Output the comparison results.
            int totalPixels = bmp1.Width * bmp1.Height;
            Console.WriteLine($"Total pixels: {totalPixels}");
            Console.WriteLine($"Differing pixels: {diffCount}");
            Console.WriteLine($"Difference percentage: {(diffCount * 100.0 / totalPixels):F2}%");
        }
    }

    /// <summary>
    /// Generates a barcode image with specified colors and saves it to a file.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="barColor">The color of the barcode bars.</param>
    /// <param name="backColor">The background color of the image.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    static void GenerateAndSaveBarcode(BaseEncodeType type, string text, Color barColor, Color backColor, string outputPath)
    {
        // Create a barcode generator and configure its visual parameters.
        using (var generator = new BarcodeGenerator(type, text))
        {
            generator.Parameters.Barcode.BarColor = barColor; // Set bar color.
            generator.Parameters.BackColor = backColor;       // Set background color.

            // Save the generated barcode directly to a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}