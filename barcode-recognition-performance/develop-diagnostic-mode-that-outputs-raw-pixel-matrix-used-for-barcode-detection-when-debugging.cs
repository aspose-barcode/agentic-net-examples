// Title: Barcode generation with diagnostic pixel matrix output
// Description: Demonstrates generating a Code128 barcode, printing its raw pixel matrix for debugging, and recognizing the barcode.
// Prompt: Develop a diagnostic mode that outputs the raw pixel matrix used for barcode detection when debugging.
// Tags: barcode symbology, generation, recognition, diagnostic, pixel matrix, console output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, prints its raw pixel matrix for diagnostic purposes,
/// and then reads the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, outputs its pixel matrix, and performs recognition.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Create the barcode image as a Bitmap
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Output the raw pixel matrix (0 = white, 1 = black) for diagnostic debugging
                PrintPixelMatrix(barcodeImage);

                // Perform barcode recognition on the same image
                using (var reader = new BarCodeReader(barcodeImage))
                {
                    // Iterate through all detected barcodes
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Detected Barcode Type: " + result.CodeTypeName);
                        Console.WriteLine("Detected CodeText: " + result.CodeText);
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                        Console.WriteLine($"Angle: {result.Region.Angle}");
                    }
                }
            }
        }
    }

    // Prints the bitmap pixel matrix to the console.
    // Uses a simple threshold to convert color to binary (black/white).
    static void PrintPixelMatrix(Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        Console.WriteLine("Pixel Matrix (1 = black, 0 = white):");
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Retrieve the pixel color at (x, y)
                Color pixel = bitmap.GetPixel(x, y);
                // Compute luminance as average of RGB components
                int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                // Threshold at mid-point (128) to obtain binary value
                int binary = luminance < 128 ? 1 : 0;
                Console.Write(binary);
            }
            Console.WriteLine();
        }
    }
}