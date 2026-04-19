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
        // Define file path
        string filePath = "barcode.png";

        // Define expected colors
        Color expectedBarColor = Color.FromArgb(255, 255, 0, 0); // Red
        Color expectedBackColor = Color.FromArgb(255, 255, 255, 255); // White

        // Create barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set colors
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Parameters.BackColor = expectedBackColor;

            // Save barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the saved PNG contains the exact RGB values
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: Barcode file was not created.");
            return;
        }

        using (var image = (Bitmap)Image.FromFile(filePath))
        {
            bool barColorFound = false;
            bool backColorFound = false;

            // Scan all pixels (small image, acceptable for verification)
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    if (pixel.ToArgb() == expectedBarColor.ToArgb())
                        barColorFound = true;
                    if (pixel.ToArgb() == expectedBackColor.ToArgb())
                        backColorFound = true;

                    // Early exit if both found
                    if (barColorFound && backColorFound)
                        break;
                }
                if (barColorFound && backColorFound)
                    break;
            }

            if (!barColorFound)
                Console.WriteLine("Verification failed: Bar color not found in the image.");
            else if (!backColorFound)
                Console.WriteLine("Verification failed: Background color not found in the image.");
            else
                Console.WriteLine("Verification succeeded: Both bar and background colors match the expected RGB values.");
        }
    }
}