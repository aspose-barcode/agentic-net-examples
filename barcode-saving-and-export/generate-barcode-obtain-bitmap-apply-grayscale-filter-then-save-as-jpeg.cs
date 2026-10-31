// Title: Generate a Code128 barcode and convert it to a grayscale JPEG image
// Description: This example creates a Code128 barcode, converts the generated bitmap to grayscale, and saves it as a JPEG file.
// Category-Description: Demonstrates basic Aspose.BarCode generation and image processing using Aspose.Drawing. It covers creating a BarcodeGenerator, rendering a bitmap, applying pixel‑level manipulation, and saving the result. Developers working with barcode rendering, image filters, or custom export formats will find this pattern useful for quick prototyping and automation tasks.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: code128, barcode generation, grayscale, jpeg, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, converts it to grayscale,
/// and saves the result as a JPEG image using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_gray.jpg");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as a bitmap image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Iterate over each pixel to apply a grayscale conversion.
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Retrieve the original pixel color.
                        Color original = bitmap.GetPixel(x, y);

                        // Compute the luminance using standard NTSC coefficients.
                        int gray = (int)(0.3f * original.R + 0.59f * original.G + 0.11f * original.B);

                        // Create a new grayscale color preserving the original alpha.
                        Color grayColor = Color.FromArgb(original.A, gray, gray, gray);

                        // Set the pixel to the new grayscale color.
                        bitmap.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed bitmap as a JPEG file.
                bitmap.Save(outputPath, ImageFormat.Jpeg);
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Grayscale barcode saved to: {outputPath}");
    }
}