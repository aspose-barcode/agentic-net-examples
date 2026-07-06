// Title: Generate Code128 barcodes from CSV and save as PNG
// Description: Reads a CSV file containing barcode text and image dimensions, then creates PNG images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode generation with size control, covering BarcodeGenerator, EncodeTypes, and image format settings. Useful for developers needing batch barcode creation, custom dimensions, and file output in console utilities.
// Prompt: Create console utility reading CSV values, assigning size units, and outputting PNG files.
// Tags: barcode symbology, generation, png, csv, console, aspose.barcode, code128, size units

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console utility that reads barcode data from a CSV file (or uses sample data) and generates PNG images with specified dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional CSV file path argument, processes each line, and saves generated barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV file path (first argument or default)
        string csvPath = args.Length > 0 ? args[0] : "input.csv";

        // Prepare data list: each item holds the code text and desired image size (width, height) in points
        var items = new List<(string CodeText, float Width, float Height)>();

        if (File.Exists(csvPath))
        {
            // Read CSV lines
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expected format: CodeText,Width,Height
                var parts = line.Split(',');
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Invalid line (expected 3 columns): {line}");
                    continue;
                }

                // Parse barcode text
                string code = parts[0].Trim();

                // Parse width (points)
                if (!float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float width))
                {
                    Console.WriteLine($"Invalid width value: {parts[1]}");
                    continue;
                }

                // Parse height (points)
                if (!float.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float height))
                {
                    Console.WriteLine($"Invalid height value: {parts[2]}");
                    continue;
                }

                // Add valid entry to the collection
                items.Add((code, width, height));
            }
        }
        else
        {
            // Fallback sample data (5 items) when CSV is missing
            items.Add(("Sample001", 200f, 100f));
            items.Add(("Sample002", 250f, 120f));
            items.Add(("Sample003", 180f, 90f));
            items.Add(("Sample004", 220f, 110f));
            items.Add(("Sample005", 240f, 130f));
            Console.WriteLine($"CSV file not found at '{csvPath}'. Using sample data.");
        }

        // Ensure output directory exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Process each item and generate a PNG barcode
        foreach (var item in items)
        {
            // Use Code128 as a generic 1D barcode type
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.CodeText))
            {
                // Set image size using point units
                generator.Parameters.ImageWidth.Point = item.Width;
                generator.Parameters.ImageHeight.Point = item.Height;

                // Use interpolation mode to respect the explicit size
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build a safe output file name
                string safeCode = string.Concat(item.CodeText.Split(Path.GetInvalidFileNameChars()));
                string outputPath = Path.Combine(outputDir, $"{safeCode}.png");

                // Save as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{item.CodeText}' -> {outputPath}");
            }
        }

        // Program ends automatically
    }
}