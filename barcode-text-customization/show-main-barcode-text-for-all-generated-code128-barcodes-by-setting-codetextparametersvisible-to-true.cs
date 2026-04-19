using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Directory to store generated barcodes
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample Code128 texts
        string[] texts = new string[]
        {
            "ABC123456",
            "CODE128TEST",
            "1234567890",
            "HELLO-WORLD",
            "AsposeBarCode"
        };

        foreach (string txt in texts)
        {
            // Create a barcode generator for Code128 with the given text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, txt))
            {
                // Ensure the human‑readable text is visible (default is Below, but set explicitly)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Optional: customize appearance of the text
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Save the barcode image as PNG
                string filePath = Path.Combine(outputDir, $"{txt}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode for \"{txt}\" to \"{filePath}\"");
            }
        }
    }
}