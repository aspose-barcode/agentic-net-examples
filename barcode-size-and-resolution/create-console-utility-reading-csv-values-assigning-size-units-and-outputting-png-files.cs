// Title: Console utility to generate DataMatrix barcodes from CSV input
// Description: Reads barcode data from a CSV file or sample data, sets X‑dimension size units, and saves each barcode as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with size unit handling. Shows how to use BarcodeGenerator, set XDimension in pixels or millimeters, configure resolution, and save images. Useful for developers needing batch barcode creation from data sources.
// Prompt: Create console utility reading CSV values, assigning size units, and outputting PNG files.
// Tags: datamatrix, barcode generation, png output, size unit, aspose.barcode, console utility

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates DataMatrix barcodes based on CSV input (or sample data) and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a path to a CSV file.</param>
    static void Main(string[] args)
    {
        // Determine CSV source: use provided file path or fall back to built‑in sample data.
        string csvPath = args.Length > 0 ? args[0] : null;
        List<string> lines = new List<string>();

        if (!string.IsNullOrEmpty(csvPath) && File.Exists(csvPath))
        {
            // Load all lines from the specified CSV file.
            lines.AddRange(File.ReadAllLines(csvPath));
        }
        else
        {
            // Sample CSV data format: CodeText,Unit,Value,Resolution(optional)
            lines.Add("ABC123,Pixels,3,96");
            lines.Add("XYZ789,Millimeters,2,300");
            lines.Add("HELLO,Pixels,5,150");
        }

        // Create a unique temporary output directory for the generated PNG files.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Output directory: {outputDir}");

        // Process each CSV line.
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            // Split the CSV line into its components.
            string[] parts = line.Split(',');
            if (parts.Length < 3)
                continue; // Not enough data to process.

            string codeText = parts[0].Trim();
            string unit = parts[1].Trim();
            string valueStr = parts[2].Trim();

            // Parse the X‑dimension value.
            if (!float.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
                continue; // Invalid numeric value.

            // Parse optional resolution; default to 96 DPI if omitted or invalid.
            int resolution = 96;
            if (parts.Length >= 4 && int.TryParse(parts[3].Trim(), out int res))
                resolution = res;

            // Create a barcode generator for DataMatrix symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Assign X‑dimension based on the specified unit.
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
                    // Default to pixels if the unit is unrecognized.
                    generator.Parameters.Barcode.XDimension.Pixels = value;
                }

                // Set the image resolution.
                generator.Parameters.Resolution = resolution;

                // Build a safe file name by replacing invalid characters.
                string safeCode = codeText.Replace(Path.GetInvalidFileNameChars(), '_');
                string fileName = $"{safeCode}_{unit}_{value}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the barcode image as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated: {filePath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
static class PathExtensions
{
    /// <summary>
    /// Replaces each character in <paramref name="chars"/> with the specified <paramref name="replacement"/>.
    /// </summary>
    /// <param name="source">The original string.</param>
    /// <param name="chars">Array of characters to replace.</param>
    /// <param name="replacement">The character to insert in place of each found character.</param>
    /// <returns>A new string with the replacements applied.</returns>
    public static string Replace(this string source, char[] chars, char replacement)
    {
        foreach (char c in chars)
        {
            source = source.Replace(c.ToString(), replacement.ToString());
        }
        return source;
    }
}