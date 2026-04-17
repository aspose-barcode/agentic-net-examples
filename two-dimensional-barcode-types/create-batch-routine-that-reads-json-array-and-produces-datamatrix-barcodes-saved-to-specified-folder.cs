using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Determine input JSON file and output folder (fallback to defaults)
        string inputJsonPath = args.Length > 0 ? args[0] : "input.json";
        string outputFolder = args.Length > 1 ? args[1] : "output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input JSON file does not exist, create a small sample file
        if (!File.Exists(inputJsonPath))
        {
            string[] sampleData = new[] { "ABC123", "HelloWorld", "DataMatrix1", "1234567890", "SampleText" };
            string sampleJson = JsonSerializer.Serialize(sampleData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(inputJsonPath, sampleJson);
            Console.WriteLine($"Sample input file created at '{inputJsonPath}'.");
        }

        // Read and deserialize the JSON array
        string jsonContent = File.ReadAllText(inputJsonPath);
        string[] codeTexts;
        try
        {
            codeTexts = JsonSerializer.Deserialize<string[]>(jsonContent);
            if (codeTexts == null)
                throw new JsonException("Deserialized array is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON file: {ex.Message}");
            return;
        }

        // Process each codetext and generate a DataMatrix barcode
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string text = codeTexts[i];
            string fileName = $"barcode_{i}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Optional: set image resolution or size if needed
                // generator.Parameters.Resolution = 300; // DPI

                // Save the barcode image
                generator.Save(outputPath);
            }

            Console.WriteLine($"Saved barcode for '{text}' to '{outputPath}'.");
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}