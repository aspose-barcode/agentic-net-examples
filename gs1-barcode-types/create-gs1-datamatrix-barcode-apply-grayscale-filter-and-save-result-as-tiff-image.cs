using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a GS1 DataMatrix barcode and conversion to a grayscale TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 DataMatrix barcode, converts it to grayscale, and saves it as a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text using AI (01) for GTIN.
        string codeText = "(01)12345678901231";

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1datamatrix.tiff");

        // Initialize the barcode generator for GS1 DataMatrix with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Generate the original colored bitmap.
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap with the same dimensions to hold the grayscale image.
                using (Bitmap grayBitmap = new Bitmap(original.Width, original.Height))
                {
                    // Iterate over each pixel to compute its grayscale value.
                    for (int y = 0; y < original.Height; y++)
                    {
                        for (int x = 0; x < original.Width; x++)
                        {
                            // Retrieve the original pixel color.
                            Color pixelColor = original.GetPixel(x, y);

                            // Compute luminance using the simple average method.
                            int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                            // Preserve the original alpha channel while setting RGB to the grayscale value.
                            Color grayColor = Color.FromArgb(pixelColor.A, gray, gray, gray);

                            // Assign the grayscale color to the new bitmap.
                            grayBitmap.SetPixel(x, y, grayColor);
                        }
                    }

                    // Save the grayscale bitmap as a TIFF file at the specified path.
                    grayBitmap.Save(outputPath, ImageFormat.Tiff);
                }
            }
        }

        // Inform the user where the TIFF file has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved as TIFF at: {outputPath}");
    }
}