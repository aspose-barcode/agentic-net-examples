using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode.png";
        const string codeText = "123ABC";
        // Expected foreground color #FF00FF (magenta)
        Color expectedColor = Color.FromArgb(255, 0, 255);

        // Create barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = expectedColor;

            // Save barcode as PNG
            generator.Save(outputPath);
        }

        // Verify that the saved image contains the expected foreground color
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{outputPath}'.");
            return;
        }

        using (var bitmap = new Bitmap(outputPath))
        {
            // Sample a pixel that is likely part of a bar (e.g., near the center)
            int sampleX = bitmap.Width / 2;
            int sampleY = bitmap.Height / 2;
            Color pixelColor = bitmap.GetPixel(sampleX, sampleY);

            bool colorsMatch = pixelColor.ToArgb() == expectedColor.ToArgb();

            Console.WriteLine($"Sampled pixel at ({sampleX},{sampleY}) color: #{pixelColor.ToArgb():X8}");
            Console.WriteLine(colorsMatch
                ? "Foreground color matches the expected custom color."
                : "Foreground color does NOT match the expected custom color.");
        }
    }
}