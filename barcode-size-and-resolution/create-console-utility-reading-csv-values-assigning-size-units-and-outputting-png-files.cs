// Title: Console utility to generate barcodes from CSV with size units
// Description: Demonstrates reading barcode data from a CSV file, assigning X‑dimension units, and saving each barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes programmatically. Typical use cases include batch barcode creation from data sources such as CSV, Excel, or databases, where developers need to control size units (pixels or millimeters) and output formats like PNG. The snippet serves as a reference for building console utilities that automate barcode production.
// Prompt: Create console utility reading CSV values, assigning size units, and outputting PNG files.
// Tags: barcode symbology, generation, png, aspose.barcode, csv, console utility, xdimension

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Code128 barcodes from a CSV file, applying specified size units,
/// and saves each barcode as a PNG image in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Reads sample CSV data, creates barcodes with pixel or millimeter X‑dimensions,
    /// and writes the resulting PNG files to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary CSV file with sample data
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "CsvBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string csvPath = Path.Combine(tempDir, "input.csv");
        var sampleLines = new List<string>
        {
            "CodeText,Unit,Value",
            "ABC123,Pixels,3",
            "XYZ789,Millimeters,2",
            "HELLO,Pixels,5"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // --------------------------------------------------------------------
        // Create output directory for generated PNG files
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(tempDir, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Read CSV and generate barcodes line by line
        // --------------------------------------------------------------------
        using (var reader = new StreamReader(csvPath))
        {
            // Skip header row
            string header = reader.ReadLine();
            if (header == null)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Ignore empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split CSV columns
                string[] parts = line.Split(',');
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Invalid CSV line: {line}");
                    continue;
                }

                string codeText = parts[0].Trim();
                string unit = parts[1].Trim();

                // Parse numeric value for X‑dimension
                if (!float.TryParse(parts[2].Trim(), out float value))
                {
                    Console.WriteLine($"Invalid numeric value in line: {line}");
                    continue;
                }

                // ----------------------------------------------------------------
                // Create barcode generator (using Code128 as example)
                // ----------------------------------------------------------------
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Assign size unit based on CSV value
                    if (unit.Equals("Pixels", StringComparison.OrdinalIgnoreCase))
                    {
                        generator.Parameters.Barcode.XDimension.Pixels = value;
                    }
                    else if (unit.Equals("Millimeters", StringComparison.OrdinalIgnoreCase))
                    {
                        generator.Parameters.Barcode.XDimension.Millimeters = value;
                    }
                    else
                    {
                        Console.WriteLine($"Unknown unit '{unit}' for line: {line}");
                        continue;
                    }

                    // Optional: set resolution (default is 96 dpi)
                    generator.Parameters.Resolution = 300f;

                    // Build output file name and path
                    string fileName = $"{codeText}_{unit}_{value}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save barcode as PNG
                    generator.Save(outPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode: {outPath}");
                }
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputDir}");
    }
}