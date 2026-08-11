// Title: Generate 50 Codabar barcodes from CSV and save each as PNG
// Description: This example reads up to 50 Codabar values from a CSV file (creating a sample file if missing) and generates individual PNG images for each barcode.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.Codabar. Typical scenarios include batch creation of barcode images from data sources such as CSV files for inventory, shipping, or point‑of‑sale systems. Developers often need to configure start/stop symbols, choose image formats, and handle file I/O efficiently.
// Prompt: Generate 50 Codabar barcodes from a CSV file, saving each as an individual PNG image.
// Tags: codabar, barcode, generation, png, csv, aspose.barcode, encode-types, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Codabar barcodes from a CSV file and saving each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that reads barcode data, creates Codabar barcodes, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file containing Codabar values (one per line).
        string csvPath = "codes.csv";

        // If the CSV does not exist, create a sample file with 50 Codabar values.
        if (!File.Exists(csvPath))
        {
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                for (int i = 1; i <= 50; i++)
                {
                    // Codabar requires start/stop symbols (A, B, C, D). Use 'A' for both.
                    string code = $"A{i:D5}A";
                    writer.WriteLine(code);
                }
            }
        }

        // Read all lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);

        // Process up to 50 entries (or fewer if the file has less).
        int count = Math.Min(50, lines.Length);
        for (int i = 0; i < count; i++)
        {
            string codeText = lines[i].Trim();

            // Skip empty lines and report them.
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Line {i + 1} is empty. Skipping.");
                continue;
            }

            // Create a Codabar barcode generator with the specified code text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Optional: set start/stop symbols explicitly (default is 'A').
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                // Save each barcode as an individual PNG file.
                string outputFile = $"barcode_{i + 1}.png";
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode {i + 1} to '{outputFile}'.");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}