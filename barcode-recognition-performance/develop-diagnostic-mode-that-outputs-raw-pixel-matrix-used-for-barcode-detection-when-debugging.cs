// Title: Barcode Generation, Pixel Matrix Dump, and Recognition Demo
// Description: Generates a Code128 barcode, prints its raw pixel matrix to the console, and then reads the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and demonstrates a diagnostic mode that outputs the raw pixel matrix for debugging. Developers often need to visualize barcode pixel data when troubleshooting detection issues, making this pattern useful for debugging and learning purposes.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode symbology, generation, recognition, pixel matrix, debugging, aspose.barcode, code128, console output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, raw pixel matrix output for debugging, and barcode recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, displays its pixel matrix, and decodes it.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator with Code128 symbology.
        var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);

        // Use a memory stream to hold the generated PNG image.
        using (var ms = new MemoryStream())
        {
            // Save the barcode image to the stream.
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for subsequent reads.

            // Load the image into a bitmap for pixel inspection.
            using (var bitmap = new Bitmap(ms))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;
                Console.WriteLine($"Bitmap size: {width}x{height}");
                Console.WriteLine("Raw pixel matrix (█ = dark, space = light):");

                // Iterate over each pixel to build a visual representation.
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                        char symbol = brightness < 128 ? '█' : ' ';
                        Console.Write(symbol);
                    }
                    Console.WriteLine();
                }
            }

            // Reset stream position again before reading with the recognizer.
            ms.Position = 0;

            // Set up the barcode reader to detect all supported types.
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                // Perform barcode detection.
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes detected: {results.Length}");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }
    }
}