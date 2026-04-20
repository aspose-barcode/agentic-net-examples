using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample JSON array of identifiers (fallback if no external input is provided)
        string json = "[\"123456789012\", \"987654321098\", \"555555555555\", \"111111111111\", \"222222222222\"]";

        // Deserialize the JSON into a string array
        string[] identifiers = JsonSerializer.Deserialize<string[]>(json);

        // Ensure we have identifiers to process
        if (identifiers == null || identifiers.Length == 0)
        {
            Console.WriteLine("No identifiers found to generate barcodes.");
            return;
        }

        // Create output directory
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a Dutch KIX barcode for each identifier and save as BMP
        for (int i = 0; i < identifiers.Length; i++)
        {
            string codeText = identifiers[i];

            // Validate that the code text is not null or empty
            if (string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Identifier at index {i} is empty. Skipping.");
                continue;
            }

            // Construct the file name
            string fileName = Path.Combine(outputDir, $"barcode_{i + 1}.bmp");

            // Generate and save the barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX))
            {
                generator.CodeText = codeText;
                // Save directly as BMP using the appropriate format enum
                generator.Save(fileName, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Saved barcode for identifier '{codeText}' to '{fileName}'.");
        }
    }
}