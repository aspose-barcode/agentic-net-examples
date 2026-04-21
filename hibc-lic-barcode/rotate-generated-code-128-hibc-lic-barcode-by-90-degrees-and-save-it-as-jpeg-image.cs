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
        // Output file path
        string outputPath = "HIBC_LIC_Rotated.jpg";

        // Create a HIBC Code128 LIC barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCCode128LIC, "A12345"))
        {
            // Generate the barcode image
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Rotate the image 90 degrees clockwise
                // After rotation width and height are swapped
                using (Bitmap rotated = new Bitmap(original.Height, original.Width))
                {
                    using (Graphics g = Graphics.FromImage(rotated))
                    {
                        // Move origin to the bottom-left corner before rotating
                        g.TranslateTransform(0, original.Height);
                        // Rotate -90 degrees (clockwise)
                        g.RotateTransform(-90);
                        // Draw the original image onto the rotated canvas
                        g.DrawImage(original, 0, 0, original.Width, original.Height);
                    }

                    // Save the rotated image as JPEG
                    rotated.Save(outputPath, ImageFormat.Jpeg);
                }
            }
        }

        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}