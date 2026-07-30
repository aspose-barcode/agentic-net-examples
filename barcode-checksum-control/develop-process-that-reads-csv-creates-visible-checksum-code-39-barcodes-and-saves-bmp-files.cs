// Title: Generate visible‑checksum Code 39 barcodes from CSV and save BMP
// Description: Reads a CSV file, creates Code 39 barcodes with visible checksum, and writes each barcode to a BMP image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using BarcodeGenerator, EncodeTypes.Code39FullASCII, and image export. This example belongs to the “Barcode Generation” category, showing how to configure checksum, colors, and file handling for batch processing. Developers often need to generate multiple barcodes from data sources such as CSV files, customize appearance, and save them in common image formats.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: barcode symbology, generation, code39, checksum, bmp, csv, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads a CSV file, generates Code 39 barcodes with visible checksum, and saves them as BMP images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Handles directory setup, CSV reading, barcode generation, and file saving.
    /// </summary>
    static void Main()
    {
        // Define input CSV path and output directory
        string csvPath = "input.csv";
        string outputDir = "Barcodes";

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If CSV does not exist, create a sample file with a few entries
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                "ABC123",
                "XYZ789",
                "CODE39",
                "HELLO WORLD",
                "1234567890"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read CSV lines
        var lines = new List<string>();
        using (var reader = new StreamReader(csvPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Trim and skip empty lines
                line = line.Trim();
                if (line.Length == 0) continue;

                // Use first column as code text (comma‑separated)
                string[] parts = line.Split(',');
                lines.Add(parts[0]);
            }
        }

        // Process each code text
        foreach (var codeText in lines)
        {
            // Create barcode generator for Code39FullASCII
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum generation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                // Show checksum in human‑readable text
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Optional: set colors
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build safe file name
                string safeFileName = GetSafeFileName(codeText) + ".bmp";
                string outputPath = Path.Combine(outputDir, safeFileName);

                // Save as BMP
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved barcode for \"{codeText}\" to \"{outputPath}\"");
            }
        }
    }

    // Replace characters that are invalid in file names and limit length
    private static string GetSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        // Limit length to avoid overly long paths
        if (name.Length > 100)
            name = name.Substring(0, 100);
        return name;
    }
}