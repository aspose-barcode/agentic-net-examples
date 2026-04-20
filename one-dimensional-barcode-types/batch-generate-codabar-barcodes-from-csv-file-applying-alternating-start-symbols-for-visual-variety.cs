using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define folders
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Output");
        string csvPath = Path.Combine(inputFolder, "data.csv");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample CSV if it does not exist (CodeText column only)
        if (!File.Exists(csvPath))
        {
            File.WriteAllLines(csvPath, new[]
            {
                "123456",
                "987654",
                "A1B2C3",
                "555555",
                "999999"
            });
        }

        // Read all non‑empty lines from the CSV
        string[] lines = File.ReadAllLines(csvPath);
        int index = 0;
        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue; // skip empty lines

            // Alternate start/stop symbols: A for even rows, B for odd rows
            CodabarSymbol startStopSymbol = (index % 2 == 0) ? CodabarSymbol.A : CodabarSymbol.B;

            // Create barcode generator for Codabar
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar))
            {
                // Set the text to encode
                generator.CodeText = line;

                // Apply alternating start/stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startStopSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = startStopSymbol;

                // Save the barcode image as PNG
                string fileName = $"barcode_{index + 1}_{line}.png";
                string outputPath = Path.Combine(outputFolder, fileName);
                generator.Save(outputPath);
            }

            index++;
        }

        Console.WriteLine($"Generated {index} barcode(s) in \"{outputFolder}\".");
    }
}