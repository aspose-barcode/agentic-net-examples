using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Configuration
        const int barcodeCount = 5;
        const float startHeight = 20f;      // initial BarHeight in pixels
        const float heightStep = 10f;       // increment step
        const string outputDir = "Barcodes";

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        for (int i = 0; i < barcodeCount; i++)
        {
            float currentHeight = startHeight + i * heightStep;

            // Create barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Use explicit size control
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set incremental BarHeight (pixels)
                generator.Parameters.Barcode.BarHeight.Pixels = currentHeight;

                // Optional: set image resolution if needed
                generator.Parameters.Resolution = 96f;

                // Build file name
                string fileName = Path.Combine(outputDir, $"barcode_{(int)currentHeight}.jpeg");

                // Save as JPEG
                generator.Save(fileName, BarCodeImageFormat.Jpeg);

                // Load saved image to log its dimensions
                using (var image = Image.FromFile(fileName))
                {
                    Console.WriteLine($"Saved '{Path.GetFileName(fileName)}' with BarHeight {currentHeight}px – Image size: {image.Width}x{image.Height} pixels");
                }
            }
        }
    }
}