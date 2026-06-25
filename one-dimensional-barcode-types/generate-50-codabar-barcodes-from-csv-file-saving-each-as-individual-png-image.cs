using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Codabar barcodes from a CSV file or sample data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads barcode texts from a CSV file and generates PNG images.
    /// If the CSV file is missing, generates a set of sample barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file containing Codabar code texts (one per line)
        const string csvPath = "input.csv";

        // Verify that the CSV file exists before attempting to read it
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            // Fallback: generate a few sample barcodes when the file is absent
            GenerateSampleBarcodes(5);
            return;
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        // Limit the number of barcodes for a safe demo (adjust to 50 in production)
        int maxCount = Math.Min(lines.Length, 5);

        // Process each line up to the defined limit
        for (int i = 0; i < maxCount; i++)
        {
            // Trim whitespace and skip empty lines
            string codeText = lines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Define the output file name for the current barcode
            string outputFile = $"barcode_{i + 1}.png";

            // Create a barcode generator for Codabar with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Codabar does not require a checksum; explicitly disable it
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Save the generated barcode as a PNG image
                generator.Save(outputFile);
            }

            Console.WriteLine($"Saved: {outputFile}");
        }
    }

    // Generates a small set of sample Codabar barcodes when the CSV file is missing.
    static void GenerateSampleBarcodes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Construct a sample code text using the pattern A{i}12345B
            string codeText = $"A{i}12345B";
            // Define the output file name for the sample barcode
            string outputFile = $"sample_barcode_{i + 1}.png";

            // Create a barcode generator for Codabar with the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Disable checksum for Codabar
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Save the generated sample barcode as a PNG image
                generator.Save(outputFile);
            }

            Console.WriteLine($"Sample saved: {outputFile}");
        }
    }
}