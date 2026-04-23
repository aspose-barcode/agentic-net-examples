using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define file paths
        const string backgroundPath = "background.png";
        const string outputPath = "composite.png";

        // Verify background image exists
        if (!File.Exists(backgroundPath))
        {
            Console.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        // Create barcode generator with desired symbology and text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set barcode background to transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set foreground color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Generate barcode bitmap
            using (var barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Load background image
                using (var backgroundImage = (Bitmap)Image.FromFile(backgroundPath))
                {
                    // Composite barcode onto background
                    using (var graphics = Graphics.FromImage(backgroundImage))
                    {
                        // Draw barcode at position (0,0) – adjust as needed
                        graphics.DrawImage(barcodeBitmap, 0, 0);
                    }

                    // Save the composited image
                    backgroundImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Composite image saved to: {outputPath}");
    }
}