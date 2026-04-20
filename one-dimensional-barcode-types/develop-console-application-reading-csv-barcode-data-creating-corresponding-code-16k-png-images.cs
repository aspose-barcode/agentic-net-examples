using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;

namespace Code16KBatchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Determine CSV file path (argument or default)
            string csvPath = args.Length > 0 ? args[0] : "barcodes.csv";

            // If CSV does not exist, use a small built‑in sample
            List<(string CodeText, string FileName)> entries = new List<(string, string)>();
            if (File.Exists(csvPath))
            {
                foreach (string line in File.ReadAllLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Expected format: CodeText,FileName
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                        continue; // ignore malformed lines

                    string codeText = parts[0].Trim();
                    string fileName = parts[1].Trim();

                    if (string.IsNullOrEmpty(codeText) || string.IsNullOrEmpty(fileName))
                        continue;

                    entries.Add((codeText, fileName));
                }
            }
            else
            {
                // Sample data (5 items) when CSV is missing
                entries.Add(("1234567890123456", "code1.png"));
                entries.Add(("ABCDEF1234567890", "code2.png"));
                entries.Add(("9876543210987654", "code3.png"));
                entries.Add(("CODE16KTEST01", "code4.png"));
                entries.Add(("CODE16KTEST02", "code5.png"));
            }

            // Process each entry
            foreach (var entry in entries)
            {
                try
                {
                    // Create generator for Code16K symbology with the provided text
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, entry.CodeText))
                    {
                        // Optional: adjust image size or resolution if needed
                        generator.Parameters.ImageWidth.Point = 300f;
                        generator.Parameters.ImageHeight.Point = 300f;
                        generator.Parameters.Resolution = 96;

                        // Save as PNG (extension determines format)
                        generator.Save(entry.FileName);
                        Console.WriteLine($"Generated '{entry.FileName}' for text '{entry.CodeText}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for '{entry.CodeText}': {ex.Message}");
                }
            }
        }
    }
}