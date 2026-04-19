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
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Paths for the generated images
        string defaultPath = Path.Combine(outputDir, "dotcode_default.png");
        string paddedPath = Path.Combine(outputDir, "dotcode_padded.png");

        // -----------------------------------------------------------------
        // Generate DotCode barcode with default padding
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Use default settings (padding = 5pt on each side)
            generator.Save(defaultPath, BarCodeImageFormat.Png);
        }

        // Load the default image to get its dimensions
        int defaultWidth, defaultHeight;
        using (var defaultImage = (Bitmap)Image.FromFile(defaultPath))
        {
            defaultWidth = defaultImage.Width;
            defaultHeight = defaultImage.Height;
        }

        // -----------------------------------------------------------------
        // Generate DotCode barcode with increased padding (margin)
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Increase padding to 20 points on each side
            generator.Parameters.Barcode.Padding.Left.Point = 20f;
            generator.Parameters.Barcode.Padding.Top.Point = 20f;
            generator.Parameters.Barcode.Padding.Right.Point = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 20f;

            generator.Save(paddedPath, BarCodeImageFormat.Png);
        }

        // Load the padded image to get its dimensions
        int paddedWidth, paddedHeight;
        using (var paddedImage = (Bitmap)Image.FromFile(paddedPath))
        {
            paddedWidth = paddedImage.Width;
            paddedHeight = paddedImage.Height;
        }

        // -----------------------------------------------------------------
        // Simple assertions to verify that padding increased the image size
        // -----------------------------------------------------------------
        bool widthIncreased = paddedWidth > defaultWidth;
        bool heightIncreased = paddedHeight > defaultHeight;

        if (widthIncreased && heightIncreased)
        {
            Console.WriteLine("PASS: Padding (margin) correctly increased the surrounding whitespace.");
            Console.WriteLine($"Default size: {defaultWidth}x{defaultHeight}");
            Console.WriteLine($"Padded size: {paddedWidth}x{paddedHeight}");
        }
        else
        {
            Console.WriteLine("FAIL: Padding did not affect the image size as expected.");
            Console.WriteLine($"Default size: {defaultWidth}x{defaultHeight}");
            Console.WriteLine($"Padded size: {paddedWidth}x{paddedHeight}");
        }
    }
}