// Title: Batch Code39 SVG Barcode Generator
// Description: Reads a list of values from a CSV file (or uses a default list) and creates a Code39 barcode SVG for each entry.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode, batch, svg, csv, aspnet, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a batch of Code39 barcodes from a CSV file
/// and save each barcode as an individual SVG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles CSV loading, barcode generation,
    /// and file output for each code text.
    /// </summary>
    static void Main()
    {
        // Input CSV file path (optional). If the file does not exist, a default list is used.
        const string csvPath = "input.csv";

        // Directory where SVG files will be saved.
        const string outputDir = "Barcodes";

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load code texts from CSV or use a fallback sample.
        List<string> codeTexts = new List<string>();
        if (File.Exists(csvPath))
        {
            // Simple CSV parsing: each line's first column is taken as the barcode value.
            foreach (var line in File.ReadAllLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split by comma and trim whitespace.
                var parts = line.Split(',');
                if (parts.Length > 0)
                {
                    var code = parts[0].Trim();
                    if (!string.IsNullOrEmpty(code))
                        codeTexts.Add(code);
                }
            }
        }
        else
        {
            // Fallback sample data (safe size for demonstration).
            codeTexts.AddRange(new[]
            {
                "CODE39A",
                "12345",
                "HELLO-WORLD",
                "ASP.NET",
                "BARCODE"
            });
        }

        // Generate a Code39 barcode for each code text and save as SVG.
        foreach (var codeText in codeTexts)
        {
            // File name is sanitized to avoid invalid path characters.
            var safeFileName = GetSafeFileName(codeText);
            var outputPath = Path.Combine(outputDir, safeFileName + ".svg");

            // Create a barcode generator for Code39 with the current text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Save directly as SVG.
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
        }
    }

    // Replaces characters that are invalid in file names with an underscore.
    private static string GetSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}