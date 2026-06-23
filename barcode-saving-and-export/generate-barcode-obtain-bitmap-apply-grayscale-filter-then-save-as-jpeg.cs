using System;
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
    /// Entry point of the application. Generates a barcode, converts it to grayscale, and saves the result.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as a bitmap
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap with the same dimensions to store the grayscale version
                using (var grayBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                {
                    // Iterate over each pixel to compute its grayscale value
                    for (int y = 0; y < originalBitmap.Height; y++)
                    {
                        for (int x = 0; x < originalBitmap.Width; x++)
                        {
                            // Retrieve the original pixel color
                            Color pixel = originalBitmap.GetPixel(x, y);
                            // Compute average of RGB components for grayscale
                            int gray = (pixel.R + pixel.G + pixel.B) / 3;
                            // Preserve original alpha while setting RGB to the grayscale value
                            Color grayColor = Color.FromArgb(pixel.A, gray, gray, gray);
                            // Set the grayscale pixel in the new bitmap
                            grayBitmap.SetPixel(x, y, grayColor);
                        }
                    }

                    // Save the grayscale bitmap as a JPEG file named "barcode_gray.jpg"
                    grayBitmap.Save("barcode_gray.jpg", ImageFormat.Jpeg);
                }
            }
        }

        // Inform the user that the process completed successfully
        Console.WriteLine("Barcode generated and saved as barcode_gray.jpg");
    }
}