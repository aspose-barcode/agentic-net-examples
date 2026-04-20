using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Determine CSV file path (first argument or default)
        string csvPath = args.Length > 0 ? args[0] : "codabar_data.csv";

        // Load code texts from CSV or use sample data if file is missing
        List<string> codeTexts = new List<string>();
        if (File.Exists(csvPath))
        {
            // Read each line, split by comma, take first column as codetext
            foreach (string line in File.ReadAllLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
                {
                    // Trim whitespace and add
                    codeTexts.Add(parts[0].Trim());
                }

                if (codeTexts.Count >= 50)
                    break; // We only need up to 50 entries
            }
        }
        else
        {
            // CSV not found – generate sample codetexts
            for (int i = 1; i <= 50; i++)
            {
                // Codabar requires start/stop symbols (A, B, C, D, E, N, T)
                // Use 'A' as start and stop with a numeric payload
                codeTexts.Add($"A{10000 + i}A");
            }
        }

        // Ensure we have at most 50 items
        if (codeTexts.Count > 50)
            codeTexts = codeTexts.GetRange(0, 50);

        // Create output directory
        string outputDir = "CodabarBarcodes";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Generate each barcode and save as PNG
        for (int index = 0; index < codeTexts.Count; index++)
        {
            string text = codeTexts[index];
            string fileName = Path.Combine(outputDir, $"codabar_{index + 1:D2}.png");

            // Create BarcodeGenerator for Codabar with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, text))
            {
                // Optional: set checksum mode (default is Mod16)
                generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

                // Optional: set bar color and background color
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the barcode image as PNG
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine($"Generated {codeTexts.Count} Codabar barcode images in '{outputDir}'.");
    }
}