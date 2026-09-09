// Title: Generate Code128 Barcode, Convert to Grayscale Bitmap, and Save as JPEG
// Description: This example creates a Code128 barcode, converts the generated image to a grayscale bitmap, and saves it as a JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing image processing. Shows how to use BarcodeGenerator, Bitmap, pixel manipulation, and ImageFormat to produce a grayscale barcode image, a common requirement for printing or embedding barcodes in documents where color is not desired.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: code128, barcode, grayscale, jpeg, aspose.barcode, aspose.drawing, generation, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an entry point that generates a barcode, applies a grayscale filter, and saves the result as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the barcode generation, grayscale conversion, and file saving process.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_grayscale.jpg");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Apply a grayscale filter by processing each pixel.
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color original = bitmap.GetPixel(x, y);
                        int gray = (int)(original.R * 0.3 + original.G * 0.59 + original.B * 0.11);
                        Color grayColor = Color.FromArgb(original.A, gray, gray, gray);
                        bitmap.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed bitmap as a JPEG file.
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Jpeg);
                }
            }
        }

        // Inform the user where the grayscale barcode image was saved.
        Console.WriteLine($"Grayscale barcode saved to: {outputPath}");
    }
}