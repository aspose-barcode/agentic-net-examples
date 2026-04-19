using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample list of strings to encode
        List<string> codes = new List<string>
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Colors to alternate between
        Color[] colors = new Color[] { Color.Blue, Color.Red };

        for (int i = 0; i < codes.Count; i++)
        {
            string codeText = codes[i];
            Color barColor = colors[i % colors.Length];

            // Create a barcode generator for Code128 (you can change the symbology if needed)
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set alternating bar color
                generator.Parameters.Barcode.BarColor = barColor;

                // Save as PNG
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine($"Generated {codes.Count} barcode images in '{outputDir}'.");
    }
}