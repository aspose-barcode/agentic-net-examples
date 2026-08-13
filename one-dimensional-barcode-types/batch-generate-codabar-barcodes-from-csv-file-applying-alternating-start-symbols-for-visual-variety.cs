// Title: Batch generate Codabar barcodes from CSV with alternating start symbols
// Description: Demonstrates how to read values from a CSV file and create a series of Codabar barcode images, alternating the start/stop symbols for visual variety.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodabarSymbol classes. It illustrates typical batch processing scenarios such as reading data sources, configuring symbology options, and exporting PNG images—common tasks for developers integrating barcode creation into automated workflows.
// Prompt: Batch generate Codabar barcodes from a CSV file, applying alternating start symbols for visual variety.
// Tags: codabar, barcode, csv, batch, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes from a CSV file with alternating start/stop symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads data, creates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define the path to the CSV file containing barcode data.
        string csvPath = "data.csv";

        // Create a sample CSV file with test data if it does not already exist.
        if (!File.Exists(csvPath))
        {
            string[] sampleData = new string[]
            {
                "12345",
                "67890",
                "ABCDEF",
                "98765",
                "XYZ"
            };
            File.WriteAllLines(csvPath, sampleData);
        }

        // Read all lines from the CSV and collect non‑empty, trimmed values.
        string[] lines = File.ReadAllLines(csvPath);
        var values = new System.Collections.Generic.List<string>();
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                values.Add(trimmed);
        }

        // Define a sequence of Codabar start/stop symbols to alternate between.
        CodabarSymbol[] symbols = new CodabarSymbol[]
        {
            CodabarSymbol.A,
            CodabarSymbol.B,
            CodabarSymbol.C,
            CodabarSymbol.D
        };

        // Ensure the output directory for barcode images exists.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a barcode image for each value, applying the alternating symbol.
        for (int i = 0; i < values.Count; i++)
        {
            string codeText = values[i];
            CodabarSymbol symbol = symbols[i % symbols.Length];

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Apply the selected start and stop symbols to the Codabar barcode.
                generator.Parameters.Barcode.Codabar.StartSymbol = symbol;
                generator.Parameters.Barcode.Codabar.StopSymbol = symbol;

                // Save the generated barcode as a PNG file.
                string fileName = Path.Combine(outputDir, $"barcode_{i + 1}.png");
                generator.Save(fileName, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode for \"{codeText}\" with symbol {symbol} to {fileName}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}