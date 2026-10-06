// Title: Generate Code128 barcode and save as grayscale JPEG
// Description: This example creates a Code128 barcode, converts the generated image to a grayscale bitmap, and saves it as a JPEG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing image processing. It shows how to use BarcodeGenerator, Bitmap, Graphics, and ColorMatrix to transform barcode images, a common task for developers needing custom visual effects before persisting barcodes in formats like JPEG.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: code128, barcode, grayscale, jpeg, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to grayscale, and saving as JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies grayscale filter, and writes the result to a file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the grayscale JPEG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_grayscale.jpg");

        // Initialize the barcode generator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as a bitmap image.
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                int width = original.Width;
                int height = original.Height;

                // Create a new bitmap that will hold the grayscale version.
                using (Bitmap grayBitmap = new Bitmap(width, height))
                {
                    // Obtain a graphics object to draw onto the grayscale bitmap.
                    using (Graphics graphics = Graphics.FromImage(grayBitmap))
                    {
                        // Define a color matrix that converts colors to grayscale.
                        float[][] matrixElements = new float[][]
                        {
                            new float[] {0.3f, 0.3f, 0.3f, 0f, 0f},
                            new float[] {0.59f, 0.59f, 0.59f, 0f, 0f},
                            new float[] {0.11f, 0.11f, 0.11f, 0f, 0f},
                            new float[] {0f, 0f, 0f, 1f, 0f},
                            new float[] {0f, 0f, 0f, 0f, 1f}
                        };
                        ColorMatrix colorMatrix = new ColorMatrix(matrixElements);
                        ImageAttributes imgAttr = new ImageAttributes();
                        imgAttr.SetColorMatrix(colorMatrix);

                        // Draw the original barcode onto the grayscale bitmap using the color matrix.
                        graphics.DrawImage(
                            original,
                            new Rectangle(0, 0, width, height),
                            0,
                            0,
                            width,
                            height,
                            GraphicsUnit.Pixel,
                            imgAttr);
                    }

                    // Save the grayscale bitmap to a JPEG file.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        grayBitmap.Save(fs, ImageFormat.Jpeg);
                    }
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Grayscale barcode saved to: {outputPath}");
    }
}