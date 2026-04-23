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
        // Prepare temporary file paths
        string filledFile = Path.Combine(Path.GetTempPath(), "filled.png");
        string emptyFile = Path.Combine(Path.GetTempPath(), "empty.png");

        // Common barcode settings
        const string codeText = "123456";
        const float imageWidth = 200f;
        const float imageHeight = 100f;
        const float barHeight = 50f;

        // Generate barcode with default FilledBars (true)
        using (var generatorFilled = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorFilled.Parameters.AutoSizeMode = AutoSizeMode.None;
            generatorFilled.Parameters.ImageWidth.Point = imageWidth;
            generatorFilled.Parameters.ImageHeight.Point = imageHeight;
            generatorFilled.Parameters.Barcode.BarHeight.Point = barHeight;
            // FilledBars is true by default
            generatorFilled.Save(filledFile);
        }

        // Generate barcode with FilledBars set to false
        using (var generatorEmpty = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generatorEmpty.Parameters.AutoSizeMode = AutoSizeMode.None;
            generatorEmpty.Parameters.ImageWidth.Point = imageWidth;
            generatorEmpty.Parameters.ImageHeight.Point = imageHeight;
            generatorEmpty.Parameters.Barcode.BarHeight.Point = barHeight;
            generatorEmpty.Parameters.Barcode.FilledBars = false;
            generatorEmpty.Save(emptyFile);
        }

        // Load both images for verification
        using (var filledImage = new Bitmap(filledFile))
        using (var emptyImage = new Bitmap(emptyFile))
        {
            // Verify dimensions are preserved
            bool dimensionsMatch = filledImage.Width == emptyImage.Width &&
                                   filledImage.Height == emptyImage.Height &&
                                   filledImage.Width == (int)imageWidth &&
                                   filledImage.Height == (int)imageHeight;

            // Verify that the empty image contains only background color (white)
            bool isEmptyWhite = true;
            for (int y = 0; y < emptyImage.Height && isEmptyWhite; y++)
            {
                for (int x = 0; x < emptyImage.Width && isEmptyWhite; x++)
                {
                    Color pixel = emptyImage.GetPixel(x, y);
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        isEmptyWhite = false;
                    }
                }
            }

            // Output test result
            if (dimensionsMatch && isEmptyWhite)
            {
                Console.WriteLine("Test Passed: FilledBars=false produces empty bar shapes while preserving dimensions.");
            }
            else
            {
                Console.WriteLine("Test Failed:");
                if (!dimensionsMatch)
                    Console.WriteLine("- Image dimensions do not match expected values.");
                if (!isEmptyWhite)
                    Console.WriteLine("- Empty barcode image contains non‑white pixels.");
            }
        }

        // Clean up temporary files
        try { if (File.Exists(filledFile)) File.Delete(filledFile); } catch { }
        try { if (File.Exists(emptyFile)) File.Delete(emptyFile); } catch { }
    }
}