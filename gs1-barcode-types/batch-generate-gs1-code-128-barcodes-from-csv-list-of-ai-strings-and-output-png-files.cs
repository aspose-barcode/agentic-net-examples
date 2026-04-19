using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Determine CSV file path (first argument or default)
        string csvPath = args.Length > 0 ? args[0] : "input.csv";

        // Output directory for generated PNG files
        string outputDir = "Barcodes";

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If the CSV file does not exist, create a small sample file
        if (!File.Exists(csvPath))
        {
            string[] sampleLines = new[]
            {
                "(01)12345678901231",
                "(10)ABC123",
                "(21)9876543210"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all non‑empty lines from the CSV
        string[] lines = File.ReadAllLines(csvPath);
        int generatedCount = 0;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            generatedCount++;

            // Create a barcode generator for GS1 Code 128 with the AI string
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, line))
            {
                // Optional: set encoding mode and visual parameters
                generator.Parameters.Barcode.Code128.Code128EncodeMode = Code128EncodeMode.Auto;
                generator.Parameters.Barcode.XDimension.Point = 2f;   // 2 points x‑dimension
                generator.Parameters.Barcode.BarHeight.Point = 50f; // 50 points bar height

                // Build output file name
                string outputPath = Path.Combine(outputDir, $"barcode_{generatedCount}.png");

                // Save the barcode as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine($"Generated {generatedCount} barcode(s) in folder '{outputDir}'.");
    }
}