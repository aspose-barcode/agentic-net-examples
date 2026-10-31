// Title: Generate 50 Codabar barcodes from CSV and save as PNG images
// Description: This example reads up to 50 data strings from a CSV file, creates a Codabar barcode for each entry, and stores the images as separate PNG files.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the Codabar symbology. It covers reading input data, configuring barcode parameters, and exporting images using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Useful for developers needing batch barcode creation for inventory, shipping, or point‑of‑sale systems.
// Prompt: Generate 50 Codabar barcodes from a CSV file, saving each as an individual PNG image.
// Tags: codabar, barcode generation, csv, png, aspose.barcode, batch processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates up to 50 Codabar barcodes from a CSV file and saves each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares sample CSV if missing, reads data, creates output folder, generates barcodes, and reports the result.
    /// </summary>
    static void Main()
    {
        // Ensure a CSV file with sample Codabar data exists in the temp folder
        string csvPath = Path.Combine(Path.GetTempPath(), "codabar_data.csv");
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                // Write 50 lines, each containing a valid Codabar string (start/stop characters A)
                for (int i = 0; i < 50; i++)
                {
                    string code = $"A{i:D5}A";
                    writer.WriteLine(code);
                }
            }
        }

        // Load all lines from the CSV and limit processing to a maximum of 50 entries
        string[] lines = File.ReadAllLines(csvPath);
        int count = Math.Min(lines.Length, 50);

        // Create a unique output directory for the generated PNG images
        string outputFolder = Path.Combine(Path.GetTempPath(), "CodabarBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate through each line, generate a barcode, and save it as a PNG file
        for (int i = 0; i < count; i++)
        {
            string codeText = lines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty lines

            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1:D2}.png");

            // Use Aspose.BarCode to generate a Codabar barcode with optional module size configuration
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set module (X) dimension
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Generated {count} Codabar barcode images in: {outputFolder}");
    }
}