using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder to store generated barcodes
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Random generator for background colors
        Random rnd = new Random();

        // Generate a set of barcodes (reduced to 10 for safe execution)
        for (int i = 1; i <= 10; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"Code{i:D3}";

            // Generate random background color
            int r = rnd.Next(0, 256);
            int g = rnd.Next(0, 256);
            int b = rnd.Next(0, 256);
            Color bgColor = Color.FromArgb(r, g, b);

            // Use Code128 symbology as an example
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the random background color
                generator.Parameters.BackColor = bgColor;

                // Optional: set a fixed foreground color for visibility
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Save the barcode image
                string filePath = Path.Combine(outputDir, $"barcode_{i:D3}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }
}