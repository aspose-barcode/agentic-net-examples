using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the CSV file (change as needed)
        const string csvPath = "input.csv";

        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read all lines from the CSV
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Assume the first column contains the code text
            string[] parts = line.Split(',');
            string codeText = parts[0].Trim();

            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine("Empty code text found, skipping line.");
                continue;
            }

            // Create a Code39 barcode generator with the code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Enable checksum generation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                // Make the checksum digit visible in the human‑readable text
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Build a safe file name for the BMP image
                string safeFileName = GetSafeFileName(codeText) + ".bmp";
                string outputPath = Path.Combine("Barcodes", safeFileName);

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Save the barcode as BMP
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved barcode for '{codeText}' to '{outputPath}'.");
            }
        }
    }

    // Replace characters that are invalid in file names
    static string GetSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}