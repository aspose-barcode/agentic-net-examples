// Title: Create GS1 DataMatrix barcode with grayscale filter and save as TIFF
// Description: Demonstrates generating a GS1 DataMatrix barcode, converting it to grayscale, and saving the result as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image processing category. It showcases the use of BarcodeGenerator for GS1 DataMatrix symbology, Aspose.Drawing for bitmap manipulation, and applying a grayscale ColorMatrix via ImageAttributes. Developers often need to customize barcode appearance, apply filters, and export to various image formats such as TIFF for printing or archival purposes.
// Prompt: Create a GS1 DataMatrix barcode, apply a grayscale filter, and save the result as a TIFF image.
// Tags: gs1datamatrix, barcode, grayscale, tiff, imageprocessing, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program demonstrating GS1 DataMatrix barcode generation, grayscale conversion, and TIFF output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies grayscale filter, and saves as TIFF.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the TIFF image
        string outputPath = "gs1datamatrix.tiff";

        // Initialize a barcode generator for GS1 DataMatrix with sample GS1 data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, "(01)12345678901231"))
        {
            // Generate the barcode as a bitmap image
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap to hold the grayscale version
                using (Bitmap grayBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                {
                    // Obtain a graphics object for drawing onto the grayscale bitmap
                    using (Graphics graphics = Graphics.FromImage(grayBitmap))
                    {
                        // Define a grayscale color matrix (luminosity method)
                        var grayMatrix = new ColorMatrix(new float[][]
                        {
                            new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
                            new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
                            new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
                            new float[] { 0, 0, 0, 1, 0 },
                            new float[] { 0, 0, 0, 0, 1 }
                        });

                        // Apply the grayscale matrix using ImageAttributes
                        using (ImageAttributes attributes = new ImageAttributes())
                        {
                            attributes.SetColorMatrix(grayMatrix);
                            // Draw the original bitmap onto the grayscale bitmap using the color matrix
                            graphics.DrawImage(
                                originalBitmap,
                                new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                0, 0, originalBitmap.Width, originalBitmap.Height,
                                GraphicsUnit.Pixel,
                                attributes);
                        }
                    }

                    // Save the resulting grayscale bitmap as a TIFF file
                    grayBitmap.Save(outputPath, ImageFormat.Tiff);
                }
            }
        }

        // Output the full path of the saved barcode image
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}