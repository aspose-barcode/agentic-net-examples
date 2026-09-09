// Title: Generate a GS1 DataMatrix barcode, convert to grayscale, and save as TIFF
// Description: This example creates a GS1 DataMatrix barcode, applies a grayscale color matrix, and writes the result to a TIFF file. It demonstrates how to manipulate barcode images using Aspose.BarCode and Aspose.Drawing.
// Category-Description: The sample belongs to the Aspose.BarCode image processing category, illustrating barcode generation (BarcodeGenerator, EncodeTypes) and post‑render image manipulation (Bitmap, Graphics, ImageAttributes, ColorMatrix). Developers often need to customize barcode appearance for printing or archival purposes, such as applying filters or converting formats. This snippet shows a typical workflow for generating a barcode, applying a visual effect, and saving in a lossless format.
// Prompt: Create a GS1 DataMatrix barcode, apply a grayscale filter, and save the result as a TIFF image.
// Tags: gs1datamatrix, grayscale, tiff, barcode, generation, aspose.barcode, imaging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode, applying a grayscale filter,
/// and saving the result as a TIFF image using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, processes the image, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix_grayscale.tiff");

        // Initialize the barcode generator for GS1 DataMatrix with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, "(01)12345678901231"))
        {
            // Optional: set the module (pixel) size for higher resolution
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Generate the barcode as a bitmap image
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap to hold the grayscale version
                using (Bitmap grayBitmap = new Bitmap(original.Width, original.Height))
                {
                    // Obtain a graphics object for drawing onto the grayscale bitmap
                    using (Graphics graphics = Graphics.FromImage(grayBitmap))
                    {
                        // Define the grayscale color matrix (luminosity method)
                        float[][] matrixElements = new float[][]
                        {
                            new float[] {0.3f, 0.3f, 0.3f, 0f, 0f},
                            new float[] {0.59f, 0.59f, 0.59f, 0f, 0f},
                            new float[] {0.11f, 0.11f, 0.11f, 0f, 0f},
                            new float[] {0f, 0f, 0f, 1f, 0f},
                            new float[] {0f, 0f, 0f, 0f, 1f}
                        };

                        // Apply the color matrix using ImageAttributes
                        using (var attr = new ImageAttributes())
                        {
                            var colorMatrix = new ColorMatrix(matrixElements);
                            attr.SetColorMatrix(colorMatrix);

                            // Draw the original image onto the grayscale bitmap with the color matrix applied
                            graphics.DrawImage(
                                original,
                                new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height,
                                GraphicsUnit.Pixel,
                                attr);
                        }
                    }

                    // Save the resulting grayscale bitmap as a TIFF file
                    grayBitmap.Save(outputPath, ImageFormat.Tiff);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved as grayscale TIFF at: {outputPath}");
    }
}