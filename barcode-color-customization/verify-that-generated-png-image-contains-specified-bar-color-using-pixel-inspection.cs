using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "barcode.png";

        // Desired bar color
        Color expectedBarColor = Color.Red;

        // Create barcode generator, set code text and bar color, generate and save image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated barcode image
                barcodeBitmap.Save(outputPath);
            }
        }

        // Verify that the saved image contains the expected bar color
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: File '{outputPath}' was not created.");
            return;
        }

        bool colorFound = false;
        using (Bitmap loadedBitmap = new Bitmap(outputPath))
        {
            for (int y = 0; y < loadedBitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < loadedBitmap.Width && !colorFound; x++)
                {
                    Color pixelColor = loadedBitmap.GetPixel(x, y);
                    if (pixelColor.ToArgb() == expectedBarColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        Console.WriteLine(colorFound
            ? "Verification succeeded: bar color is present in the image."
            : "Verification failed: bar color not found in the image.");
    }
}