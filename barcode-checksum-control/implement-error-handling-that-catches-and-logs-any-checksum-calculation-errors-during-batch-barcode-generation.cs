using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string[] codeTexts = new string[]
        {
            "123456789012",
            "12345",
            "ABC123",
            "invalid!"
        };

        foreach (string code in codeTexts)
        {
            string safeFileName = string.Join("_", code.Split(Path.GetInvalidFileNameChars()));
            string filePath = Path.Combine(outputFolder, $"{safeFileName}.png");

            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                    generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                    generator.CodeText = code;
                    generator.Save(filePath);
                }

                Console.WriteLine($"Successfully generated barcode for \"{code}\" -> {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error for \"{code}\": {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}