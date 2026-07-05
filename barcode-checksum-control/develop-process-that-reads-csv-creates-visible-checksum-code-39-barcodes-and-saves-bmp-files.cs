// Title: Generate visible‑checksum Code 39 barcodes from CSV data
// Description: Reads a CSV file, creates Code 39 Full ASCII barcodes with visible checksum, and saves each as a BMP image.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: barcode symbology, code39, checksum, bmp, csv, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading a CSV file, generating Code 39 barcodes with a visible checksum,
/// and saving each barcode as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the CSV and creates barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file (adjust as needed)
        string csvPath = "input.csv";

        // Verify that the CSV file exists before proceeding
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Directory where generated BMP files will be stored
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all non‑empty lines from the CSV file
        var lines = File.ReadAllLines(csvPath)
                        .Where(l => !string.IsNullOrWhiteSpace(l))
                        .ToArray();

        // Process each line in the CSV
        foreach (var line in lines)
        {
            // Assume the first column contains the text to encode
            var columns = line.Split(',');
            if (columns.Length == 0) continue;

            string codeText = columns[0].Trim();
            if (string.IsNullOrEmpty(codeText)) continue;

            // Create a barcode generator for Code39FullASCII
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum and make it visible in the human‑readable text
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Optional visual settings: black bars on white background
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build a safe file name from the code text (remove invalid characters, replace spaces)
                string safeFileName = string.Concat(codeText.Split(Path.GetInvalidFileNameChars()))
                                            .Replace(' ', '_');
                if (string.IsNullOrWhiteSpace(safeFileName))
                {
                    // Fallback to a GUID if the resulting name is empty
                    safeFileName = Guid.NewGuid().ToString();
                }

                // Full path for the output BMP file
                string outputPath = Path.Combine(outputDir, $"{safeFileName}.bmp");

                // Save the barcode as BMP
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
                Console.WriteLine($"Saved barcode for \"{codeText}\" to {outputPath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}