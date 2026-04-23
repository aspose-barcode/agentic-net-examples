using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define output file
        string outputPath = "barcode.png";

        // Create barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set foreground (bars) color to dark red
            generator.Parameters.Barcode.BarColor = Color.FromArgb(139, 0, 0); // DarkRed

            // Save barcode as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved image contains the expected dark red color
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        using (var bitmap = new Bitmap(outputPath))
        {
            // Choose a pixel that is likely part of the barcode (avoid pure white background)
            // Here we sample a few points until we find a non‑white pixel
            Color expectedColor = Color.FromArgb(139, 0, 0);
            bool colorMatched = false;

            for (int y = 0; y < bitmap.Height && !colorMatched; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorMatched; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Skip white background pixels
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        colorMatched = pixel.ToArgb() == expectedColor.ToArgb();
                    }
                }
            }

            if (colorMatched)
                Console.WriteLine("Color verification passed: dark red bars detected.");
            else
                Console.WriteLine("Color verification failed: expected dark red bars not found.");
        }
    }
}