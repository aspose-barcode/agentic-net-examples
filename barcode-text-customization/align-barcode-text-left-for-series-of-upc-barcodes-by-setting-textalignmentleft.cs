using System;
using System.Windows;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample UPC‑A codes (12 digits each)
        string[] upcCodes = new string[]
        {
            "012345678905",
            "123456789012",
            "036000291452"
        };

        // Create a generator for UPC‑A barcodes
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA))
        {
            // Align human‑readable text to the left
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            for (int i = 0; i < upcCodes.Length; i++)
            {
                generator.CodeText = upcCodes[i];
                string fileName = $"upc_{i + 1}.png";
                generator.Save(fileName);
                Console.WriteLine($"Saved {fileName}");
            }
        }
    }
}