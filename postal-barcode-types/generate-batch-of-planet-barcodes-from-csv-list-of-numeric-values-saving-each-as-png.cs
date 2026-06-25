using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Planet barcodes from CSV data and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Parses CSV data, validates numeric values,
    /// generates Planet barcodes, and saves them to the output directory.
    /// </summary>
    static void Main()
    {
        // Sample CSV data containing numeric values for Planet barcodes.
        // In a real scenario, replace this with reading from an actual CSV file.
        string csvData = "12345,67890,24680,13579,112233";

        // Parse CSV values (comma or newline separated) into a list.
        List<string> values = new List<string>();
        using (StringReader reader = new StringReader(csvData))
        {
            string line;
            // Read each line from the CSV string.
            while ((line = reader.ReadLine()) != null)
            {
                // Split the line by commas, ignoring empty entries.
                foreach (var part in line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string trimmed = part.Trim();
                    // Add non‑empty trimmed values to the list.
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        values.Add(trimmed);
                    }
                }
            }
        }

        // Ensure output directory exists.
        string outputDir = "PlanetBarcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a Planet barcode for each numeric value and save as PNG.
        foreach (string codeText in values)
        {
            // Validate that the code text is numeric.
            if (!long.TryParse(codeText, out _))
            {
                Console.WriteLine($"Skipping non-numeric value: {codeText}");
                continue;
            }

            // Build the output file name and full path.
            string fileName = $"Planet_{codeText}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            // Create a barcode generator for the Planet symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Save the barcode image directly as PNG.
                generator.Save(outputPath);
            }

            Console.WriteLine($"Saved barcode for value {codeText} to {outputPath}");
        }
    }
}