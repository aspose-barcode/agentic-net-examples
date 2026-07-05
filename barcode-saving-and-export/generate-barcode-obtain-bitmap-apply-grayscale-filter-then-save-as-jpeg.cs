// Title: Generate Code128 barcode, convert to grayscale, and save as JPEG
// Description: Demonstrates creating a barcode image, applying a simple grayscale filter, and persisting the result as a JPEG file.
// Prompt: Generate a barcode, obtain a Bitmap, apply a grayscale filter, then save as JPEG.
// Tags: barcode, code128, grayscale, jpeg, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, grayscale conversion, and JPEG saving using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, converts it to grayscale, and writes it to a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final JPEG image
        string outputPath = "barcode.jpg";

        // Initialize a barcode generator for Code128 with the sample text "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Generate the barcode image as a bitmap (color)
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap with the same dimensions to hold the grayscale version
                using (Bitmap grayBitmap = new Bitmap(barcodeBitmap.Width, barcodeBitmap.Height))
                {
                    // Iterate over each pixel to compute its grayscale value
                    for (int y = 0; y < barcodeBitmap.Height; y++)
                    {
                        for (int x = 0; x < barcodeBitmap.Width; x++)
                        {
                            // Retrieve the original color of the current pixel
                            Color original = barcodeBitmap.GetPixel(x, y);
                            // Compute the average of the RGB components to obtain a gray intensity
                            int gray = (original.R + original.G + original.B) / 3;
                            // Create a new color where R, G, and B are all set to the gray intensity
                            Color grayColor = Color.FromArgb(gray, gray, gray);
                            // Set the pixel in the grayscale bitmap
                            grayBitmap.SetPixel(x, y, grayColor);
                        }
                    }

                    // Open a file stream to write the grayscale bitmap as a JPEG image
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        // Save the bitmap using the JPEG image format
                        grayBitmap.Save(fs, ImageFormat.Jpeg);
                    }
                }
            }
        }

        // Output the full path of the saved JPEG file for verification
        Console.WriteLine($"Barcode saved as JPEG at: {Path.GetFullPath(outputPath)}");
    }
}