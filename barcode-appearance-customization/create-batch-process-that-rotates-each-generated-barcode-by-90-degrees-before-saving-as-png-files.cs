using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output folder for generated barcodes
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample code texts to encode
        string[] codeTexts = { "12345", "ABCDEF", "987654321" };

        for (int i = 0; i < codeTexts.Length; i++)
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeTexts[i];
                // Rotate the barcode image by 90 degrees
                generator.Parameters.RotationAngle = 90f;
                // Save the rotated barcode as PNG
                string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved rotated barcode: {filePath}");
            }
        }
    }
}