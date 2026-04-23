using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths for the base image and the final output
        const string baseImagePath = "baseImage.png";
        const string outputPath = "output.png";

        // Ensure a base image exists; if not, create a blank one
        if (!File.Exists(baseImagePath))
        {
            using (var blank = new Bitmap(400, 200))
            {
                using (var g = Graphics.FromImage(blank))
                {
                    g.Clear(Aspose.Drawing.Color.LightGray);
                }
                blank.Save(baseImagePath, ImageFormat.Png);
            }
        }

        // Create a barcode generator with transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set barcode colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // Transparent background for overlay
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Generate the barcode image
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Load the base image
                using (var baseImage = new Bitmap(baseImagePath))
                {
                    // Draw the barcode onto the base image
                    using (var graphics = Graphics.FromImage(baseImage))
                    {
                        // Position the barcode at (10,10)
                        graphics.DrawImage(barcodeImage, 10, 10, barcodeImage.Width, barcodeImage.Height);
                    }

                    // Save the combined image
                    baseImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }
    }
}