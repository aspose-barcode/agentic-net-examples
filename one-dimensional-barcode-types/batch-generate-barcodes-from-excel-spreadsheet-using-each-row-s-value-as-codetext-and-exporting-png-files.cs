// Title: Batch barcode generation from Excel rows
// Description: Demonstrates reading code texts from an Excel (or CSV) file and generating PNG barcode images for each row.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes, AutoSizeMode, and image format settings. Typical use cases include bulk barcode creation from data sources such as spreadsheets for inventory, shipping, or labeling. Developers often need to read data, loop through entries, and export barcodes in common image formats.
// Prompt: Batch generate barcodes from an Excel spreadsheet, using each row’s value as CodeText and exporting PNG files.
// Tags: barcode, code128, batch, excel, csv, png, generation, aspose.barcode, autosizemode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that reads code texts from an Excel/CSV file and generates PNG barcodes
/// using Aspose.BarCode. Each row's first column becomes the CodeText for a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, loads code texts, and generates a PNG barcode for each entry.
    /// </summary>
    static void Main()
    {
        // Input Excel (or CSV) file path – adjust as needed.
        string inputPath = "input.xlsx";

        // Output directory for generated PNG files.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load code texts from the spreadsheet (or fallback sample data).
        List<string> codeTexts = LoadCodeTexts(inputPath);

        // Generate a barcode image for each code text.
        int index = 1;
        foreach (string text in codeTexts)
        {
            // Create a barcode generator for Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Use interpolation auto‑size mode for automatic image dimensions.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Optional: set image resolution (dots per inch).
                generator.Parameters.Resolution = 300f;

                // Optional: set foreground (barcode) and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Build output file name (e.g., barcode_001.png).
                string fileName = Path.Combine(outputDir, $"barcode_{index:D3}.png");

                // Save the barcode as PNG.
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {index}: {text}");
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Loads code texts from a CSV file (simple fallback for Excel) or returns a sample list.
    private static List<string> LoadCodeTexts(string path)
    {
        var list = new List<string>();

        // If a CSV file exists, read each line's first column as a code text.
        if (File.Exists(path) && Path.GetExtension(path).Equals(".csv", StringComparison.OrdinalIgnoreCase))
        {
            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split by comma and take the first column.
                string[] parts = line.Split(',');
                if (parts.Length > 0)
                {
                    list.Add(parts[0].Trim());
                }

                // Limit to a safe sample size (max 5 items).
                if (list.Count >= 5)
                    break;
            }
        }
        else
        {
            // File not found or not CSV – use a predefined sample set (max 5 items).
            for (int i = 1; i <= 5; i++)
            {
                list.Add($"Sample{i:D3}");
            }
        }

        return list;
    }
}