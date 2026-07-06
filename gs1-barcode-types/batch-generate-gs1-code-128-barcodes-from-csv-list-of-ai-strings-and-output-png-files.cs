// Title: Batch generate GS1 Code 128 barcodes from CSV
// Description: Reads a CSV file containing GS1 AI strings, creates a GS1 Code 128 barcode for each entry, and saves the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128. Typical use cases include bulk barcode creation for inventory, shipping, or retail labeling where AI data is stored in CSV format. Developers often need to automate barcode production, handle file I/O, and manage output directories, which this snippet illustrates.
// Prompt: Batch generate GS1 Code 128 barcodes from a CSV list of AI strings and output PNG files.
// Tags: gs1,code128,barcode,generation,csv,output,png,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of GS1 Code 128 barcodes from a CSV file and saves each barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads AI strings from a CSV, generates barcodes, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Input CSV file containing GS1 AI strings (one per line)
        const string inputCsv = "input.csv";

        // Directory where generated PNG files will be saved
        const string outputDir = "output";

        // --------------------------------------------------------------------
        // Ensure the input file exists; if not, create a sample file with a few AI strings
        // --------------------------------------------------------------------
        if (!File.Exists(inputCsv))
        {
            string[] sampleData =
            {
                "(01)12345678901231",
                "(10)ABC123",
                "(21)9876543210",
                "(01)09876543210987(21)XYZ12345"
            };
            File.WriteAllLines(inputCsv, sampleData);
            Console.WriteLine($"Sample input file '{inputCsv}' created.");
        }

        // --------------------------------------------------------------------
        // Ensure the output directory exists
        // --------------------------------------------------------------------
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Read all non‑empty lines from the CSV
        // --------------------------------------------------------------------
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;

        foreach (string rawLine in lines)
        {
            // Trim whitespace and skip empty lines
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            // Create a safe file name based on the index (e.g., barcode_1.png)
            string fileName = $"barcode_{index}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // --------------------------------------------------------------------
                // Generate GS1 Code128 barcode for the AI string
                // --------------------------------------------------------------------
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, line))
                {
                    // Save as PNG (format inferred from file extension)
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation
                Console.WriteLine($"Error processing line {index}: {ex.Message}");
            }

            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }
}