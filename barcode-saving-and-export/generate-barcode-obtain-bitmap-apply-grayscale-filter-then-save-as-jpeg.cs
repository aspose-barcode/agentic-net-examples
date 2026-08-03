// Title: Generate Code128 barcode, apply grayscale filter, and save as JPEG
// Description: This example creates a Code128 barcode, converts it to a bitmap, applies a grayscale filter, and saves the result as a JPEG file.
// Category-Description: Demonstrates Aspose.BarCode image generation and manipulation using Aspose.Drawing. It shows how to generate a barcode with BarcodeGenerator, obtain a Bitmap, process pixel data, and save in a common image format. Developers working with barcode rendering, image post‑processing, or custom graphics pipelines often need these steps.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: code128, barcode, grayscale, jpeg, bitmap, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, converts it to a grayscale bitmap,
/// and saves the image as a JPEG file using Aspose.BarCode and Aspose.Drawing APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the JPEG image.
        string outputPath = "barcode.jpg";

        // Ensure the target directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a barcode generator for Code128 with the sample text "Sample123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Iterate over each pixel to apply a simple grayscale filter.
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Retrieve the original pixel color.
                        Color original = bitmap.GetPixel(x, y);

                        // Compute the luminance as the average of the RGB components.
                        int gray = (original.R + original.G + original.B) / 3;

                        // Create a new color with full opacity and the computed gray value.
                        Color grayColor = Color.FromArgb(255, gray, gray, gray);

                        // Set the pixel to the new grayscale color.
                        bitmap.SetPixel(x, y, grayColor);
                    }
                }

                // Save the processed bitmap to the specified path in JPEG format.
                bitmap.Save(outputPath, ImageFormat.Jpeg);
            }
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}