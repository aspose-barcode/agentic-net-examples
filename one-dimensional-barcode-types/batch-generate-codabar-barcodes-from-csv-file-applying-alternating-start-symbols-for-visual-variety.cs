using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Codabar barcodes from a CSV file using Aspose.BarCode.
/// </summary>
class Program
{
    // Maximum number of barcodes to generate in this demo (kept small for the runner)
    private const int MaxBarcodes = 5;

    /// <summary>
    /// Entry point of the application. Reads a CSV file, generates up to <see cref="MaxBarcodes"/>
    /// Codabar barcodes, and saves them as PNG images.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the CSV path.</param>
    static void Main(string[] args)
    {
        // Determine CSV path: command‑line argument or default sample file
        string csvPath = args.Length > 0 ? args[0] : "sample.csv";

        // If the CSV does not exist, create a simple sample file
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found at '{csvPath}'. Creating a sample CSV.");
            CreateSampleCsv(csvPath);
        }

        // Read all lines from the CSV (including possible header)
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length == 0)
        {
            Console.WriteLine("CSV file is empty. Nothing to process.");
            return;
        }

        // Ensure the output directory exists (creates if missing)
        string outputDir = Path.Combine(Path.GetDirectoryName(csvPath) ?? "", "Barcodes");
        Directory.CreateDirectory(outputDir);

        int processed = 0; // Counter for successfully generated barcodes

        // Iterate over CSV lines until the maximum number of barcodes is reached
        for (int i = 0; i < allLines.Length && processed < MaxBarcodes; i++)
        {
            string line = allLines[i].Trim();

            // Skip empty lines
            if (string.IsNullOrEmpty(line))
                continue;

            // Assume CSV format: CodeText[,OtherColumns...]
            string[] parts = line.Split(',');
            string codeText = parts[0].Trim();

            // Skip lines where the code text is missing
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Alternate start/stop symbols: A, B, C, D in round‑robin fashion
            CodabarSymbol startSymbol = GetAlternatingSymbol(processed);
            CodabarSymbol stopSymbol = startSymbol; // Usually the same as start

            // Build the output file path for the current barcode
            string outputPath = Path.Combine(outputDir, $"codabar_{processed + 1}.png");

            // Generate the Codabar barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Apply alternating start/stop symbols
                generator.Parameters.Barcode.Codabar.StartSymbol = startSymbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = stopSymbol;

                // Optional: set checksum mode to No (Codabar does not require checksum)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the image (PNG format by default)
                generator.Save(outputPath);
            }

            Console.WriteLine($"Generated barcode #{processed + 1}: '{codeText}' with start/stop symbol '{startSymbol}'. Saved to '{outputPath}'.");
            processed++;
        }

        Console.WriteLine($"Processing complete. {processed} barcode(s) generated.");
    }

    /// <summary>
    /// Returns a Codabar start/stop symbol in a round‑robin sequence A, B, C, D.
    /// </summary>
    /// <param name="index">Zero‑based index used to select the symbol.</param>
    /// <returns>The selected <see cref="CodabarSymbol"/>.</returns>
    private static CodabarSymbol GetAlternatingSymbol(int index)
    {
        switch (index % 4)
        {
            case 0: return CodabarSymbol.A;
            case 1: return CodabarSymbol.B;
            case 2: return CodabarSymbol.C;
            default: return CodabarSymbol.D;
        }
    }

    /// <summary>
    /// Creates a simple CSV file with a few sample code texts.
    /// </summary>
    /// <param name="path">The file path where the CSV will be written.</param>
    private static void CreateSampleCsv(string path)
    {
        string[] sampleData =
        {
            "1234567",
            "9876543",
            "5551234",
            "24681012",
            "1357911"
        };
        File.WriteAllLines(path, sampleData);
    }
}