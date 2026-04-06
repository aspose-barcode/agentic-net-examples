using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeOverlayBatch
{
    class Program
    {
        static void Main()
        {
            string inputFolder = "InputImages";
            string outputFolder = "OutputImages";

            // Ensure input folder exists
            if (!Directory.Exists(inputFolder))
                Directory.CreateDirectory(inputFolder);

            // Seed a sample image if the folder is empty
            if (!Directory.GetFiles(inputFolder).Any())
            {
                using (var bmp = new Bitmap(200, 200))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    string samplePath = Path.Combine(inputFolder, "sample.png");
                    bmp.Save(samplePath, ImageFormat.Png);
                }
            }

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get image files to process
            var files = Directory.GetFiles(inputFolder, "*.*")
                .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

            foreach (var filePath in files)
            {
                // Load the original image
                using (var original = new Bitmap(filePath))
                {
                    // Create a barcode generator
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                    {
                        generator.CodeText = "Sample";

                        // Generate the barcode image
                        using (var barcodeBmp = generator.GenerateBarCodeImage())
                        {
                            // Position the barcode at the bottom‑right corner with a 10‑pixel margin
                            int x = original.Width - barcodeBmp.Width - 10;
                            int y = original.Height - barcodeBmp.Height - 10;
                            if (x < 0) x = 0;
                            if (y < 0) y = 0;

                            // Draw the barcode onto the original image
                            using (var g = Graphics.FromImage(original))
                            {
                                g.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                            }
                        }
                    }

                    // Save the combined image as BMP
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outPath = Path.Combine(outputFolder, fileName + "_barcode.bmp");
                    original.Save(outPath, ImageFormat.Bmp);
                }
            }

            Console.WriteLine("Processing completed.");
        }
    }
}