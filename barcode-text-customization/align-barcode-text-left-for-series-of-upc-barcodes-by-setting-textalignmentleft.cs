using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Directory to store generated barcode images
        string outputDir = "output";
        Directory.CreateDirectory(outputDir);

        // Sample UPC-A codes (11 digits; check digit will be added automatically)
        string[] upcCodes = new string[]
        {
            "12345678901",
            "98765432109",
            "55555555555",
            "00011122223",
            "77788899990"
        };

        for (int i = 0; i < upcCodes.Length; i++)
        {
            string code = upcCodes[i];

            // Create a barcode generator for UPC-A with the specified code text
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
            {
                // Align the human‑readable text to the left
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Save the barcode image as PNG
                string filePath = Path.Combine(outputDir, $"upc_{i + 1}.png");
                generator.Save(filePath);
            }
        }
    }
}