using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create output directory
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        // Base text for barcodes
        string baseText = "Sample";

        // Generate 5 barcodes with increasing BarHeight
        for (int i = 1; i <= 5; i++)
        {
            // Incremental bar height in points
            float barHeightPoints = 20f * i;

            string filePath = Path.Combine(outputDir, $"barcode_{i}.jpeg");

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = $"{baseText}{i}";
                // Set BarHeight using the Point unit
                generator.Parameters.Barcode.BarHeight.Point = barHeightPoints;
                // Save as JPEG
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            // Load the saved image to log its dimensions
            using (var image = Image.FromFile(filePath))
            {
                Console.WriteLine($"Saved {Path.GetFileName(filePath)} - BarHeight: {barHeightPoints}pt, Width: {image.Width}px, Height: {image.Height}px");
            }
        }
    }
}