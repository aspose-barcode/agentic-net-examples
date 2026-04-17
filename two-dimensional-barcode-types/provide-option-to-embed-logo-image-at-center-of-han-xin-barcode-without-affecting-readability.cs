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
        // Paths for the output barcode and the logo image.
        string outputPath = "HanXinWithLogo.png";
        string logoPath = "logo.png";

        // Verify that the logo file exists.
        if (!File.Exists(logoPath))
        {
            Console.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Create a Han Xin barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "SampleHanXinCode"))
        {
            // Optional visual settings.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Generate the barcode image.
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Load the logo image.
                using (Bitmap logoBitmap = (Bitmap)Image.FromFile(logoPath))
                {
                    // Determine the size of the logo (e.g., 20% of barcode width).
                    float logoScale = 0.2f;
                    int logoWidth = (int)(barcodeBitmap.Width * logoScale);
                    int logoHeight = (int)(logoBitmap.Height * logoScale);

                    // Calculate the position to center the logo.
                    int logoX = (barcodeBitmap.Width - logoWidth) / 2;
                    int logoY = (barcodeBitmap.Height - logoHeight) / 2;

                    // Draw the logo onto the barcode.
                    using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                    {
                        // High quality rendering.
                        graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = Aspose.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        // Draw the logo with scaling.
                        graphics.DrawImage(logoBitmap, logoX, logoY, logoWidth, logoHeight);
                    }

                    // Save the combined image.
                    barcodeBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Barcode with embedded logo saved to: {outputPath}");
    }
}