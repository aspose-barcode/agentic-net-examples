using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode batch generation utility.
/// Reads AI strings from a CSV file (or uses sample data) and creates GS1-128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates reading input, generating barcodes, and saving them to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine input CSV path (first argument or default)
        string csvPath = args.Length > 0 ? args[0] : "input.csv";

        // Prepare list to hold AI strings read from CSV or sample data
        List<string> aiStrings = new List<string>();

        // Attempt to read AI strings from the specified CSV file
        if (File.Exists(csvPath))
        {
            try
            {
                // Read all lines and split each line by commas
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Split by commas, remove empty entries, and trim whitespace
                    var parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var part in parts)
                    {
                        var trimmed = part.Trim();
                        if (!string.IsNullOrEmpty(trimmed))
                        {
                            aiStrings.Add(trimmed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while reading the CSV
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }
        else
        {
            // CSV not found – fall back to predefined sample AI strings
            Console.WriteLine($"CSV file not found at '{csvPath}'. Using sample data.");
            aiStrings.Add("(01)12345678901231");
            aiStrings.Add("(01)98765432109876");
            aiStrings.Add("(01)00012345678905");
            aiStrings.Add("(01)55555555555555");
            aiStrings.Add("(01)99999999999999");
        }

        // If no AI strings were collected, exit early
        if (aiStrings.Count == 0)
        {
            Console.WriteLine("No AI strings to process. Exiting.");
            return;
        }

        // Ensure the output directory exists before saving files
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each AI string and generate a corresponding barcode image
        int index = 1;
        foreach (var codeText in aiStrings)
        {
            // Construct a safe file name for the barcode image
            string safeFileName = $"barcode_{index}.png";
            string outputPath = Path.Combine(outputDir, safeFileName);

            try
            {
                // Create a barcode generator for GS1-128 using the current AI string
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
                {
                    // Set desired resolution (dots per inch)
                    generator.Parameters.Resolution = 300f;

                    // Save the generated barcode as a PNG file (format inferred from extension)
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any failures during barcode generation
                Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
            }

            index++;
        }

        // Indicate that the batch process has finished
        Console.WriteLine("Batch generation completed.");
    }
}