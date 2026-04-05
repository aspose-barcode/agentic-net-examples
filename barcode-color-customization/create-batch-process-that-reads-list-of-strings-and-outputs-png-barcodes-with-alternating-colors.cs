using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Input strings for which barcodes will be generated
        var values = new List<string>
        {
            "ABC123",
            "XYZ789",
            "HELLO",
            "WORLD"
        };

        // Colors to alternate between
        var colors = new[]
        {
            Color.Red,
            Color.Blue
        };

        // Output directory
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir);

        for (int i = 0; i < values.Count; i++)
        {
            string text = values[i];
            Color barColor = colors[i % colors.Length];

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set alternating bar color
                generator.Parameters.Barcode.BarColor = barColor;

                // Save as PNG
                string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }
}