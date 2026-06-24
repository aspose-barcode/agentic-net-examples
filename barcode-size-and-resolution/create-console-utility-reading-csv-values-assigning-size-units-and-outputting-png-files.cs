using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates barcode images from a CSV file or sample data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads barcode definitions, creates output directory, and generates PNG files.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the CSV path.</param>
    static void Main(string[] args)
    {
        // Determine CSV file path: use first argument if provided, otherwise default.
        string csvPath = args.Length > 0 ? args[0] : "barcodes.csv";

        // List to hold barcode data: text, width, and height.
        List<(string CodeText, float Width, float Height)> items = new List<(string, float, float)>();

        // Attempt to read barcode definitions from the CSV file.
        if (File.Exists(csvPath))
        {
            using (var reader = new StreamReader(csvPath))
            {
                bool isFirstLine = true; // Tracks header row.

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    // Skip empty lines.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Skip header line if it contains column names.
                    if (isFirstLine && line.Contains("CodeText"))
                    {
                        isFirstLine = false;
                        continue;
                    }

                    // Split CSV line into parts.
                    string[] parts = line.Split(',');

                    // Ensure we have at least three columns (code, width, height).
                    if (parts.Length < 3)
                        continue; // insufficient data, skip

                    // Extract and trim code text.
                    string codeText = parts[0].Trim();

                    // Parse width; fall back to default if parsing fails.
                    if (!float.TryParse(parts[1].Trim(), out float width))
                        width = 200f; // default width

                    // Parse height; fall back to default if parsing fails.
                    if (!float.TryParse(parts[2].Trim(), out float height))
                        height = 100f; // default height

                    // Add the parsed item to the collection.
                    items.Add((codeText, width, height));
                }
            }
        }
        else
        {
            // CSV not found – use hard‑coded sample data.
            Console.WriteLine($"CSV file not found at '{csvPath}'. Using sample data.");
            items.Add(("Sample123", 250f, 120f));
            items.Add(("Test456", 300f, 150f));
            items.Add(("Demo789", 200f, 100f));
        }

        // Ensure the output directory exists.
        string outputDir = "BarcodesOutput";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a barcode image for each item.
        foreach (var item in items)
        {
            // Use Code128 as a generic symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, item.CodeText))
            {
                // Set image dimensions in points.
                generator.Parameters.ImageWidth.Point = item.Width;
                generator.Parameters.ImageHeight.Point = item.Height;

                // Optional: set image resolution (DPI).
                generator.Parameters.Resolution = 300f;

                // Build a safe file name for the output PNG.
                string safeFileName = MakeSafeFileName(item.CodeText);
                string outputPath = Path.Combine(outputDir, $"{safeFileName}.png");

                // Save the barcode image.
                generator.Save(outputPath);
                Console.WriteLine($"Generated barcode for '{item.CodeText}' -> {outputPath}");
            }
        }
    }

    /// <summary>
    /// Creates a file‑system‑safe file name by replacing invalid characters with underscores.
    /// </summary>
    /// <param name="name">Original file name.</param>
    /// <returns>Sanitized file name.</returns>
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}