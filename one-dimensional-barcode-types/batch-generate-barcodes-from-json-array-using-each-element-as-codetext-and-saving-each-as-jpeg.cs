using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample JSON array of code texts
        string json = "[\"12345\",\"ABCDEF\",\"987654321\",\"HelloWorld\",\"Test123\"]";

        // Deserialize JSON to string array
        string[] codeTexts = JsonSerializer.Deserialize<string[]>(json);
        if (codeTexts == null || codeTexts.Length == 0)
        {
            Console.WriteLine("No code texts found in JSON.");
            return;
        }

        // Prepare output directory
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate a barcode for each code text and save as JPEG
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string code = codeTexts[i];
            string fileName = $"barcode_{i + 1}.jpg";
            string filePath = Path.Combine(outputFolder, fileName);

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = code;
                generator.Save(filePath);
            }

            Console.WriteLine($"Saved barcode for \"{code}\" to \"{filePath}\"");
        }
    }
}