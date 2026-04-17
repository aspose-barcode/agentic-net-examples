using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample EAN‑13 codes (12 digits, checksum will be added automatically)
        string[] ean13Codes = new string[]
        {
            "123456789012",
            "987654321098",
            "555555555555",
            "111111111111",
            "222222222222"
        };

        // Generate each barcode with centered human‑readable text
        for (int i = 0; i < ean13Codes.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, ean13Codes[i]))
            {
                // Center‑align the codetext
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Optional: set image size for consistency
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image
                string filePath = Path.Combine(outputDir, $"EAN13_{i + 1}.png");
                generator.Save(filePath);
            }
        }

        Console.WriteLine($"Generated {ean13Codes.Length} EAN‑13 barcodes in \"{outputDir}\".");
    }
}