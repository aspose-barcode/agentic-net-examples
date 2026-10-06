// Title: Generate Code 39 Barcodes with Visible Checksum from CSV and Save as BMP
// Description: This example reads a CSV file, creates Code 39 barcodes with a visible checksum, and saves each barcode as a BMP image.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. It utilizes the BarcodeGenerator class with EncodeTypes.Code39FullASCII and BarCodeImageFormat to produce BMP files. Typical scenarios include creating printable barcodes from data sources such as CSV files, where developers need to enable checksums and customize appearance for inventory, shipping, or labeling applications.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: code39, checksum, bmp, aspose.barcode, generation, csv, batch

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads barcode data from a CSV file, generates Code 39 barcodes with a visible checksum,
/// and saves each barcode as a BMP image in an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles file I/O, barcode generation, and logging.
    /// </summary>
    static void Main()
    {
        // Define base directory and file paths
        string baseDir = Directory.GetCurrentDirectory();
        string csvPath = Path.Combine(baseDir, "input.csv");
        string outputDir = Path.Combine(baseDir, "Barcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a sample CSV if none exists
        if (!File.Exists(csvPath))
        {
            string[] sampleLines =
            {
                "CODE39A,Code39A.bmp",
                "HELLO123,Hello123.bmp",
                "TEST-XYZ,TestXYZ.bmp"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at: {csvPath}");
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int lineNumber = 0;

        foreach (string rawLine in lines)
        {
            lineNumber++;

            // Skip empty or whitespace-only lines
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Split the line into barcode text and optional file name
            string[] parts = rawLine.Split(',');
            if (parts.Length == 0)
                continue;

            string codeText = parts[0].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Determine output file name
            string fileName = parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1])
                ? parts[1].Trim()
                : $"Barcode_{lineNumber}.bmp";

            string outputPath = Path.Combine(outputDir, fileName);

            // Generate barcode with visible checksum using Aspose.BarCode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum calculation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                // Show checksum in human‑readable text
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                // Optional: increase X dimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a BMP image
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
        }

        Console.WriteLine("Processing completed.");
    }
}