// Title: Generate Code128 barcode, convert to grayscale bitmap, and save as JPEG
// Description: This example creates a Code128 barcode, converts the generated color bitmap to a grayscale image using a color matrix, and saves the result as a JPEG file.
// Category-Description: Barcode generation and image processing – demonstrates how to generate barcodes with Aspose.BarCode, manipulate the resulting bitmap using Aspose.Drawing, and export to common image formats. Typical use cases include creating printable barcode images, applying visual effects, and integrating barcode graphics into documents or UI. Developers often need to customize barcode appearance, apply filters, or convert formats, and this example shows the key classes such as BarcodeGenerator, Bitmap, Graphics, and ImageAttributes.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: code128, barcode, grayscale, jpeg, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, bitmap manipulation to grayscale, and saving as JPEG using Aspose APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, applies a grayscale filter, and writes the image to a temporary JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_grayscale.jpg");

        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode as a color bitmap
            using (Bitmap colorBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap that will hold the grayscale image (same size as the original)
                using (Bitmap grayBitmap = new Bitmap(colorBitmap.Width, colorBitmap.Height, PixelFormat.Format24bppRgb))
                {
                    // Define a grayscale color matrix (luminosity method)
                    var colorMatrix = new ColorMatrix(new float[][]
                    {
                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    });

                    // Apply the color matrix via image attributes
                    using (var imgAttr = new ImageAttributes())
                    {
                        imgAttr.SetColorMatrix(colorMatrix);

                        // Draw the original color bitmap onto the grayscale bitmap using the matrix
                        using (Graphics graphics = Graphics.FromImage(grayBitmap))
                        {
                            graphics.DrawImage(
                                colorBitmap,
                                new Rectangle(0, 0, grayBitmap.Width, grayBitmap.Height),
                                0,
                                0,
                                colorBitmap.Width,
                                colorBitmap.Height,
                                GraphicsUnit.Pixel,
                                imgAttr);
                        }
                    }

                    // Save the resulting grayscale bitmap as a JPEG file
                    grayBitmap.Save(outputPath, ImageFormat.Jpeg);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Grayscale barcode saved to: {outputPath}");
    }
}