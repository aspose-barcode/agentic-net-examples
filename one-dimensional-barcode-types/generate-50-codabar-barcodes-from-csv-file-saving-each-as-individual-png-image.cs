// Title: Generate 50 Codabar barcodes from CSV and save as PNG images
// Description: This example reads up to 50 Codabar codes from a CSV file (creating the file if missing) and generates individual PNG barcode images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Codabar symbology. It covers reading data from a CSV source, configuring barcode parameters, and saving each barcode as a separate image file. Developers working with batch barcode creation, inventory labeling, or data export can use this pattern with BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes.
// Prompt: Generate 50 Codabar barcodes from a CSV file, saving each as an individual PNG image.
// Tags: codabar, barcode generation, csv, png, aspose.barcode, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program to generate Codabar barcodes from a CSV file and save each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Reads the first column of each non‑empty line from a CSV file and returns the values as a list of strings.
    /// </summary>
    /// <param name="path">Full path to the CSV file.</param>
    /// <returns>List of barcode texts extracted from the file.</returns>
    static List<string> ReadCodes(string path)
    {
        var codes = new List<string>();
        foreach (var line in File.ReadAllLines(path))
        {
            // Skip blank lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split by comma and take the first element as the code
            var parts = line.Split(',');
            if (parts.Length > 0)
                codes.Add(parts[0].Trim());
        }
        return codes;
    }

    /// <summary>
    /// Entry point. Reads or creates a CSV of codes, generates up to 50 Codabar barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Determine the CSV file location (in the current working directory)
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "codes.csv");

        // If the CSV does not exist, create it with 50 sample Codabar codes
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                for (int i = 1; i <= 50; i++)
                {
                    // Codabar format: start/stop characters 'A' surrounding a zero‑padded number
                    string data = i.ToString("D4");
                    string code = $"A{data}A";
                    writer.WriteLine(code);
                }
            }
        }

        // Load barcode texts from the CSV file
        List<string> codes = ReadCodes(csvPath);
        int count = Math.Min(50, codes.Count); // Ensure we process at most 50 entries

        // Prepare the output directory for generated PNG images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Generate each barcode and save it as a PNG file
        for (int i = 0; i < count; i++)
        {
            string codeText = codes[i];
            string filePath = Path.Combine(outputDir, $"Codabar_{i + 1}.png");

            // Create a BarcodeGenerator for Codabar with the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Set the X dimension (module width) to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved barcode {i + 1} to {filePath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}