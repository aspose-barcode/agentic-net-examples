// Title: Generate Code39 Barcodes from CSV and Save as BMP
// Description: This example reads a CSV file containing code texts, generates Code 39 barcodes with visible checksums, and saves each barcode as a BMP image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It shows how to configure checksum visibility, set code text location, and export images in BMP format—common tasks when integrating barcode creation into batch processing or reporting workflows. Ideal for developers needing to automate barcode production from data sources such as CSV files.
// Prompt: Develop a process that reads a CSV, creates visible‑checksum Code 39 barcodes, and saves BMP files.
// Tags: code39, barcode, generation, csv, bmp, checksum, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Reads a CSV file, generates Code 39 barcodes with visible checksums, and saves each barcode as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares temporary folders, ensures a sample CSV exists,
    /// processes each line to create a barcode, and writes the output files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary folders for input CSV and output barcode images
        // --------------------------------------------------------------------
        string tempRoot = Path.GetTempPath();
        string csvPath = Path.Combine(tempRoot, "sample_codes.csv");
        string outputDir = Path.Combine(tempRoot, "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Create a sample CSV file if it does not already exist
        // --------------------------------------------------------------------
        if (!File.Exists(csvPath))
        {
            var sb = new StringBuilder();
            sb.AppendLine("CodeText");
            sb.AppendLine("ABC123");
            sb.AppendLine("CODE39");
            sb.AppendLine("HELLO");
            File.WriteAllText(csvPath, sb.ToString());
        }

        // --------------------------------------------------------------------
        // Verify that the CSV file exists before attempting to read it
        // --------------------------------------------------------------------
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Read all lines from the CSV file
        // --------------------------------------------------------------------
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data.");
            return;
        }

        // --------------------------------------------------------------------
        // Process each code text (skip header row)
        // --------------------------------------------------------------------
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split CSV line (comma‑separated) and trim the first column as the code text
            string[] parts = line.Split(',');
            string codeText = parts[0].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip rows without a code text

            // Determine output file path for the current barcode image
            string outputPath = Path.Combine(outputDir, $"barcode_{i}.bmp");

            // ----------------------------------------------------------------
            // Generate the barcode with checksum enabled and visible
            // ----------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum calculation and force its display
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Position the human‑readable text below the barcode
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the generated barcode as a BMP image
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' at: {outputPath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}