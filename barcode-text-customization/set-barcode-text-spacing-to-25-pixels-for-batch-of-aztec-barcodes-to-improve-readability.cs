using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare output folder
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample Aztec code texts
        string[] codeTexts = new string[]
        {
            "ABC123",
            "HelloWorld",
            "2023-04-15",
            "AsposeAztec",
            "Sample5"
        };

        // Generate a batch of Aztec barcodes with text spacing set to 2.5 pixels
        for (int i = 0; i < codeTexts.Length; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Aztec, codeTexts[i]))
            {
                // Set the spacing between the human‑readable text and the barcode (2.5 pixels)
                generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2.5f;

                // Optional: set a visible text different from the encoded value
                // generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = codeTexts[i];

                string filePath = Path.Combine(outputFolder, $"Aztec_{i + 1}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}