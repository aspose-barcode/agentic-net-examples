using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeColorComparison
{
    class Program
    {
        static void Main()
        {
            // Define barcode text and output file names
            const string barcodeText = "ABC12345";
            const string redFile = "barcode_red.png";
            const string blueFile = "barcode_blue.png";

            // Generate two barcodes with different foreground colors
            GenerateBarcode(barcodeText, Color.Red, redFile);
            GenerateBarcode(barcodeText, Color.Blue, blueFile);

            // Load the generated images
            using (var bmpRed = new Bitmap(redFile))
            using (var bmpBlue = new Bitmap(blueFile))
            {
                // Ensure both images have the same dimensions
                if (bmpRed.Width != bmpBlue.Width || bmpRed.Height != bmpBlue.Height)
                {
                    Console.WriteLine("Images have different dimensions and cannot be compared.");
                    return;
                }

                // Compare pixel by pixel
                int diffCount = 0;
                for (int y = 0; y < bmpRed.Height; y++)
                {
                    for (int x = 0; x < bmpRed.Width; x++)
                    {
                        Color pixelRed = bmpRed.GetPixel(x, y);
                        Color pixelBlue = bmpBlue.GetPixel(x, y);
                        if (pixelRed.ToArgb() != pixelBlue.ToArgb())
                        {
                            diffCount++;
                        }
                    }
                }

                int totalPixels = bmpRed.Width * bmpRed.Height;
                double diffPercentage = (double)diffCount / totalPixels * 100.0;

                Console.WriteLine($"Total pixels: {totalPixels}");
                Console.WriteLine($"Different pixels: {diffCount}");
                Console.WriteLine($"Difference percentage: {diffPercentage:F2}%");
            }
        }

        static void GenerateBarcode(string text, Color barColor, string outputFile)
        {
            // Create a barcode generator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set the foreground (bar) color
                generator.Parameters.Barcode.BarColor = barColor;

                // Optional: set background color (white)
                generator.Parameters.BackColor = Color.White;

                // Save the barcode image to a file (PNG based on extension)
                generator.Save(outputFile);
            }
        }
    }
}