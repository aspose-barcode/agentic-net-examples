using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeColorComparison
{
    class Program
    {
        static void Main()
        {
            // Define barcode settings
            const string codeText = "1234567890";
            const string fileBlack = "barcode_black.png";
            const string fileBlue = "barcode_blue.png";

            // Generate first barcode with default colors (black on white)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Ensure default colors (optional, shown for clarity)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save image
                generator.Save(fileBlack, BarCodeImageFormat.Png);
            }

            // Generate second barcode with custom colors (blue bars on yellow background)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;
                generator.Save(fileBlue, BarCodeImageFormat.Png);
            }

            // Verify that both files exist before comparison
            if (!File.Exists(fileBlack) || !File.Exists(fileBlue))
            {
                Console.WriteLine("One or both barcode images were not created.");
                return;
            }

            // Load images for pixel‑wise comparison
            using (var imgBlack = (Bitmap)Image.FromFile(fileBlack))
            using (var imgBlue = (Bitmap)Image.FromFile(fileBlue))
            {
                // Ensure images have the same dimensions
                if (imgBlack.Width != imgBlue.Width || imgBlack.Height != imgBlue.Height)
                {
                    Console.WriteLine("Images have different dimensions and cannot be compared directly.");
                    return;
                }

                int diffCount = 0;
                for (int y = 0; y < imgBlack.Height; y++)
                {
                    for (int x = 0; x < imgBlack.Width; x++)
                    {
                        // Compare ARGB values of each pixel
                        if (imgBlack.GetPixel(x, y).ToArgb() != imgBlue.GetPixel(x, y).ToArgb())
                        {
                            diffCount++;
                        }
                    }
                }

                Console.WriteLine($"Total differing pixels: {diffCount}");
                if (diffCount > 0)
                {
                    Console.WriteLine("The barcodes differ visually due to the custom colors applied.");
                }
                else
                {
                    Console.WriteLine("The barcodes are identical pixel‑wise (unexpected for different colors).");
                }
            }
        }
    }
}