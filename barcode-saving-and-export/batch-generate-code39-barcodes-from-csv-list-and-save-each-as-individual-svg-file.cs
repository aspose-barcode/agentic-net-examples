using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Determine input CSV path and output folder from arguments or use defaults
        string inputCsv = args.Length > 0 ? args[0] : "codes.csv";
        string outputFolder = args.Length > 1 ? args[1] : "Barcodes";

        // Ensure the CSV file exists; if not, create a small sample file
        if (!File.Exists(inputCsv))
        {
            string[] sampleCodes = { "ABC123", "CODE39", "HELLO", "12345", "TEST" };
            File.WriteAllLines(inputCsv, sampleCodes);
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Read each line (code text) from the CSV file
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;
        foreach (string rawLine in lines)
        {
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty lines

            // Build a safe file name for the SVG output
            string safeFileName = $"barcode_{index:D4}.svg";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            // Generate the Code39 barcode and save as SVG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Optional: adjust image size or other parameters here if needed
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            index++;
        }
    }
}