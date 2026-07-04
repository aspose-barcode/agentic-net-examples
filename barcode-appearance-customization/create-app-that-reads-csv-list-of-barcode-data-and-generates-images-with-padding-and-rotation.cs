// Title: Generate barcode images from CSV with padding and rotation
// Description: Demonstrates reading barcode data from a CSV file (or sample data) and creating PNG images with uniform padding and optional rotation.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode, csv, padding, rotation, png, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Reads barcode data from a CSV file (or uses sample data) and generates PNG images
/// with uniform padding and optional rotation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Loads data, creates output directory, and generates barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file (optional). If the file does not exist, sample data will be used.
        const string csvPath = "barcodes.csv";

        // Load barcode data from CSV or fall back to a default list.
        List<(string CodeText, float Rotation)> records = LoadCsv(csvPath);

        // Ensure the output directory exists.
        const string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each record and generate a barcode image.
        int index = 1;
        foreach (var record in records)
        {
            // Create a BarcodeGenerator for Code128 (change EncodeTypes as needed).
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, record.CodeText))
            {
                // Set uniform padding (10 points on each side).
                generator.Parameters.Barcode.Padding.Left.Point = 10f;
                generator.Parameters.Barcode.Padding.Top.Point = 10f;
                generator.Parameters.Barcode.Padding.Right.Point = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

                // Set rotation angle (must be 0, 90, 180, or 270 for best readability).
                generator.Parameters.RotationAngle = record.Rotation;

                // Optional: set barcode colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to the output directory.
                string fileName = Path.Combine(outputDir, $"barcode_{index}.png");
                generator.Save(fileName);
                Console.WriteLine($"Saved barcode #{index}: {fileName}");
            }

            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Loads CSV data. Expected format per line: CodeText,RotationAngle
    // RotationAngle is optional; if missing, 0 is used.
    private static List<(string CodeText, float Rotation)> LoadCsv(string path)
    {
        var list = new List<(string, float)>();

        if (File.Exists(path))
        {
            // Read each line from the CSV file.
            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Skip empty lines.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Split line into parts: code text and optional rotation.
                    string[] parts = line.Split(',');
                    string codeText = parts[0].Trim();
                    float rotation = 0f;

                    // Parse rotation if provided.
                    if (parts.Length > 1 && float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed))
                    {
                        rotation = parsed;
                    }

                    // Add valid entries to the list.
                    if (!string.IsNullOrEmpty(codeText))
                    {
                        list.Add((codeText, rotation));
                    }
                }
            }
        }
        else
        {
            // Sample data if CSV is missing.
            list.Add(("Sample001", 0f));
            list.Add(("Sample002", 90f));
            list.Add(("Sample003", 180f));
            list.Add(("Sample004", 270f));
            list.Add(("Sample005", 0f));
        }

        return list;
    }
}