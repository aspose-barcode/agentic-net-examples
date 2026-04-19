using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output folders (fallback to defaults)
        string inputFolder = args.Length > 0 ? args[0] : "InputImages";
        string outputFolder = args.Length > 1 ? args[1] : "OutputImages";

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample image if the input folder is empty (so the example runs end‑to‑end)
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var bmp = new Bitmap(200, 100))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Aspose.Drawing.Color.LightGray);
                }
                bmp.Save(samplePath, ImageFormat.Png);
            }
        }

        // Get up to 5 image files (common image extensions)
        var imageFiles = Directory.GetFiles(inputFolder)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            .Take(5)
            .ToArray();

        foreach (var filePath in imageFiles)
        {
            // Load the source image
            using (var sourceImage = new Bitmap(filePath))
            {
                // Generate a barcode image (Code128 with sample text)
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Optional: set barcode colors
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Generate the barcode bitmap
                    using (var barcodeImage = generator.GenerateBarCodeImage())
                    {
                        // Overlay the barcode onto the source image (top‑left corner)
                        using (var graphics = Graphics.FromImage(sourceImage))
                        {
                            graphics.DrawImage(barcodeImage, new Point(0, 0));
                        }
                    }
                }

                // Save the combined image as BMP
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_with_barcode.bmp";
                string outputPath = Path.Combine(outputFolder, outputFileName);
                sourceImage.Save(outputPath, ImageFormat.Bmp);
            }
        }
    }
}