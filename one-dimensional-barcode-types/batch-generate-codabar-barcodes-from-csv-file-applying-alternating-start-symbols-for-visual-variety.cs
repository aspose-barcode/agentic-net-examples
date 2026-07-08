// Title: Batch Codabar Barcode Generation from CSV
// Description: Demonstrates reading a CSV file and generating Codabar barcode images, alternating start/stop symbols for visual variety.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and CodabarSymbol classes to create barcodes in bulk. Typical use cases include batch processing of inventory codes, ticket numbers, or any data set stored in CSV format where developers need to automate image creation for downstream systems.
// Prompt: Batch generate Codabar barcodes from a CSV file, applying alternating start symbols for visual variety.
// Tags: codabar, barcode, batch, csv, image, generation, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Codabar barcodes from a CSV file, alternating start/stop symbols for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads input data, creates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file containing the data to encode.
        const string csvPath = "input.csv";

        // If the CSV does not exist, create a sample file with placeholder data.
        if (!File.Exists(csvPath))
        {
            var sampleData = new List<string>
            {
                "12345",
                "67890",
                "24680",
                "13579",
                "112233"
            };
            File.WriteAllLines(csvPath, sampleData);
            Console.WriteLine($"Sample CSV created at '{csvPath}'.");
        }

        // Load all lines from the CSV and filter out empty entries.
        string[] lines = File.ReadAllLines(csvPath);
        var codes = new List<string>();
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                codes.Add(trimmed);
        }

        // Abort if no valid data was found.
        if (codes.Count == 0)
        {
            Console.WriteLine("No data found in CSV.");
            return;
        }

        // Define a rotating set of start/stop symbols for visual variety.
        CodabarSymbol[] symbols = new CodabarSymbol[]
        {
            CodabarSymbol.A,
            CodabarSymbol.B,
            CodabarSymbol.C,
            CodabarSymbol.D
        };

        // Iterate over each code and generate the corresponding barcode image.
        for (int i = 0; i < codes.Count; i++)
        {
            string codeText = codes[i];
            CodabarSymbol startStop = symbols[i % symbols.Length];

            // Initialize the generator with Codabar symbology and the current code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Apply the alternating start and stop symbols.
                generator.Parameters.Barcode.Codabar.StartSymbol = startStop;
                generator.Parameters.Barcode.Codabar.StopSymbol = startStop;

                // Set a modest image size (points) for the output PNG.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode to a file.
                string outputFile = $"barcode_{i + 1}.png";
                generator.Save(outputFile);
                Console.WriteLine($"Generated '{outputFile}' with start/stop symbol '{startStop}'.");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}