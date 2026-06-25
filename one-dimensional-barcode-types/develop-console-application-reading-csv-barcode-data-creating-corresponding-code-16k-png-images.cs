using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code16K barcodes from a CSV file or sample data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads barcode texts from a CSV file (or uses sample data) and generates PNG images.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be a CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV file path: use first argument if provided, otherwise default to "barcodes.csv".
        string csvPath = args.Length > 0 ? args[0] : "barcodes.csv";

        // List to hold the barcode texts that will be processed.
        List<string> codeTexts = new List<string>();

        if (File.Exists(csvPath))
        {
            // Read all lines from the CSV file.
            string[] lines = File.ReadAllLines(csvPath);
            foreach (string line in lines)
            {
                // Skip empty or whitespace‑only lines.
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split the line by commas; assume the first column contains the Code16K text.
                string[] parts = line.Split(',');
                if (parts.Length > 0)
                {
                    string code = parts[0].Trim();
                    // Add non‑empty codes to the collection.
                    if (!string.IsNullOrEmpty(code))
                        codeTexts.Add(code);
                }
            }
        }
        else
        {
            // CSV file not found – fall back to hard‑coded sample data.
            Console.WriteLine($"CSV file '{csvPath}' not found. Using sample data.");
            codeTexts.Add("1234567890123456789012345678901234567890");
            codeTexts.Add("98765432109876543210");
        }

        // Generate barcode images for all collected texts.
        GenerateCode16KBarcodes(codeTexts);
    }

    /// <summary>
    /// Generates Code16K barcode PNG files for each text in the provided list.
    /// </summary>
    /// <param name="codeTexts">Collection of strings to encode as Code16K barcodes.</param>
    static void GenerateCode16KBarcodes(List<string> codeTexts)
    {
        const string outputFolder = "output";

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        int index = 1;
        foreach (string codeText in codeTexts)
        {
            // Build the output file path, e.g., "output/code16k_1.png".
            string outputPath = Path.Combine(outputFolder, $"code16k_{index}.png");
            try
            {
                // Set the encoding type to Code16K.
                BaseEncodeType encodeType = EncodeTypes.Code16K;

                // Create a barcode generator with the specified type and text.
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Optional: adjust the aspect ratio for better visual appearance.
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                    // Save the generated barcode as a PNG image.
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Barcode {index} saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during barcode generation.
                Console.WriteLine($"Error generating barcode for text '{codeText}': {ex.Message}");
            }

            index++;
        }
    }
}