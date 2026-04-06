using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input CSV file and output folder
        string inputCsvPath = "codes.csv";
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file does not exist, create a sample one
        if (!File.Exists(inputCsvPath))
        {
            var sampleData = new[]
            {
                "ABC123",
                "CODE39",
                "HELLO-WORLD",
                "1234567890"
            };
            File.WriteAllLines(inputCsvPath, sampleData, Encoding.UTF8);
        }

        // Read all non‑empty lines from the CSV
        string[] lines = File.ReadAllLines(inputCsvPath, Encoding.UTF8);
        int index = 1;
        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue; // Skip empty lines

            // Use the whole line as the CodeText (CSV may contain commas; take first column)
            string[] parts = line.Split(',');
            string codeText = parts[0].Trim();

            // Generate a safe file name
            string safeFileName = $"barcode_{index}_{SanitizeFileName(codeText)}.svg";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            // Create and save the barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
            {
                generator.CodeText = codeText;
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            Console.WriteLine($"Generated barcode for \"{codeText}\" -> {outputPath}");
            index++;
        }
    }

    // Helper to remove invalid filename characters
    static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}