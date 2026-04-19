using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output paths
        string inputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DotCodeImages");

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input file does not exist, create a sample file with a few lines
        if (!File.Exists(inputFile))
        {
            string[] sampleLines = new[]
            {
                "Sample001",
                "HelloWorld",
                "Aspose.BarCode",
                "DotCode123",
                "2026-04-16"
            };
            File.WriteAllLines(inputFile, sampleLines);
        }

        // Read all non‑empty lines from the input file
        string[] lines = File.ReadAllLines(inputFile);
        int index = 1;
        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            // Create a DotCode barcode generator for the current line
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
            {
                // Set the text to be encoded
                generator.CodeText = line;

                // Optional: set encode mode to Auto (default) – shown for completeness
                generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;

                // Build output file name
                string outputFile = Path.Combine(outputFolder, $"dotcode_{index:D3}.png");

                // Save the barcode image
                generator.Save(outputFile);
            }

            index++;
        }
    }
}