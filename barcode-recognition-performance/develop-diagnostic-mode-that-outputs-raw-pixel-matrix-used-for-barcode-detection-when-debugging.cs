using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, rendering it to a bitmap,
/// printing a simple binary pixel matrix, and reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, displays its pixel matrix, and decodes it.
    /// </summary>
    static void Main()
    {
        // Create a Code128 barcode generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a high resolution (dots per inch) for better image quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the PNG image into an Aspose.Drawing Bitmap.
                using (var bitmap = new Bitmap(ms))
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    // Output image dimensions.
                    Console.WriteLine($"Barcode image size: {width}x{height}");
                    Console.WriteLine("Raw pixel matrix (1 = black, 0 = white):");

                    // Iterate over each pixel row to build a binary representation.
                    for (int y = 0; y < height; y++)
                    {
                        var line = new System.Text.StringBuilder();

                        for (int x = 0; x < width; x++)
                        {
                            // Retrieve the pixel color at (x, y).
                            Color pixel = bitmap.GetPixel(x, y);

                            // Determine if the pixel is considered "black" using a brightness threshold.
                            // Brightness < 0.5 is treated as black; otherwise white.
                            bool isBlack = pixel.GetBrightness() < 0.5f;

                            // Append '1' for black, '0' for white.
                            line.Append(isBlack ? '1' : '0');
                        }

                        // Write the binary line to the console.
                        Console.WriteLine(line.ToString());
                    }

                    // Use BarCodeReader to decode the barcode directly from the bitmap.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Output detected barcode type and decoded text.
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                            Console.WriteLine($"Detected code text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}