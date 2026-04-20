using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define input and output directories
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputCsv");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure directories exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Seed a sample CSV file so the example can run end‑to‑end
            string sampleCsv = Path.Combine(inputFolder, "Sample1.csv");
            File.WriteAllText(sampleCsv, "ABC123\nDEF456\nGHI789");
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get CSV files (limit to a safe number for the demo)
        string[] csvFiles = Directory.GetFiles(inputFolder, "*.csv");
        int maxFiles = Math.Min(csvFiles.Length, 5);

        for (int i = 0; i < maxFiles; i++)
        {
            string csvPath = csvFiles[i];
            string[] lines = File.ReadAllLines(csvPath);
            int maxLines = Math.Min(lines.Length, 10); // limit rows per file

            for (int j = 0; j < maxLines; j++)
            {
                string codeText = lines[j].Trim();
                if (string.IsNullOrEmpty(codeText))
                    continue;

                // Create barcode generator for Code 16K
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
                {
                    generator.CodeText = codeText;

                    // Build output file name: originalFile_rowIndex.png
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(csvPath)}_Row{j + 1}.png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}