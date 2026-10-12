// Title: Generate 50 Codabar barcodes from CSV and save as PNG
// Description: The example reads up to 50 Codabar strings from a CSV file (creating sample data if missing) and generates individual PNG barcode images using Aspose.BarCode.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Codabar to produce image files. Typical use cases include batch creation of barcodes for inventory, shipping labels, or point‑of‑sale systems. Developers often need to read data from external sources (e.g., CSV) and output each barcode as a separate image for downstream processing.
// Prompt: Generate 50 Codabar barcodes from a CSV file, saving each as an individual PNG image.
// Tags: codabar, barcode generation, csv, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Codabar barcodes from a CSV file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample CSV if needed, reads up to 50 codes, and saves each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for the generated files
        string workDir = Path.Combine(Path.GetTempPath(), "CodabarBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define the path to the CSV file that will hold the barcode data
        string csvPath = Path.Combine(workDir, "data.csv");

        // If the CSV does not exist, generate sample data (50 Codabar codes)
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                for (int i = 0; i < 50; i++)
                {
                    // Example Codabar code: start A, 5‑digit number, stop A
                    string code = $"A{(i + 1).ToString("D5")}A";
                    writer.WriteLine(code);
                }
            }
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        // Limit processing to a maximum of 50 entries
        int count = Math.Min(lines.Length, 50);

        // Iterate through each barcode text and generate a PNG image
        for (int i = 0; i < count; i++)
        {
            string codeText = lines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty lines

            // Build the output file path for the current barcode image
            string outputPath = Path.Combine(workDir, $"barcode_{i + 1:D2}.png");

            // Initialize the barcode generator with Codabar symbology and the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
            {
                // Optional: set the module (X) dimension to control image size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Generated {count} Codabar barcodes in: {workDir}");
    }
}