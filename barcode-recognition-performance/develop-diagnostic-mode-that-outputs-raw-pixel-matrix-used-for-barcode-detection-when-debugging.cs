using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Output the raw pixel matrix to the console
                PrintPixelMatrix(barcodeBitmap);
            }
        }
    }

    static void PrintPixelMatrix(Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Retrieve the pixel color
                Aspose.Drawing.Color color = bitmap.GetPixel(x, y);
                // Determine if the pixel is dark (black) or light (white)
                bool isBlack = color.GetBrightness() < 0.5f;
                Console.Write(isBlack ? "1" : "0");
            }
            Console.WriteLine();
        }
    }
}