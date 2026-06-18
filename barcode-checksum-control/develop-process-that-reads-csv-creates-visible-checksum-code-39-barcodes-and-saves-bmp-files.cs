using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code39 barcodes from a CSV file or sample data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads barcode data from a CSV file (or uses sample data) and generates BMP images.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file containing barcode data.
        const string csvPath = "input.csv";

        // Load lines from the CSV file if it exists; otherwise, fall back to in‑memory sample data.
        string[] lines;
        if (File.Exists(csvPath))
        {
            // Read all lines from the existing CSV file.
            lines = File.ReadAllLines(csvPath);
        }
        else
        {
            // Sample CSV content: CodeText,OutputFileName (optional).
            lines = new[]
            {
                "ABC123,ABC123.bmp",
                "XYZ789,XYZ789.bmp",
                "HELLO WORLD,HelloWorld.bmp"
            };
            Console.WriteLine($"CSV file \"{csvPath}\" not found. Using sample data.");
        }

        // Process each non‑empty line from the input.
        foreach (var rawLine in lines)
        {
            // Skip empty or whitespace‑only lines.
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Split the line into parts: barcode text and optional output file name.
            var parts = rawLine.Split(',');
            var codeText = parts[0].Trim();

            // If the barcode text is missing, skip this line.
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Determine the output file path: use the provided name or generate a default one.
            var outputPath = parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1])
                ? parts[1].Trim()
                : $"{codeText}_code39.bmp";

            // Ensure the directory for the output file exists.
            var outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Generate a Code39 barcode with a visible checksum.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum generation for the barcode.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                // Show the checksum digit in the human‑readable text.
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode as a BMP image.
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            // Inform the user about the generated barcode.
            Console.WriteLine($"Generated barcode for \"{codeText}\" -> {outputPath}");
        }

        // Indicate that all processing is complete.
        Console.WriteLine("Processing completed.");
    }
}