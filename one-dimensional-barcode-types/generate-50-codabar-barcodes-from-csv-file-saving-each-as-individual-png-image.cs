// Title: Generate Codabar Barcodes from CSV
// Description: Creates up to 50 Codabar barcodes from a CSV file, each saved as an individual PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to read data from a CSV source and produce barcode images using the BarcodeGenerator class. Typical use cases include batch creation of product labels, inventory tags, or any scenario where multiple barcodes must be generated programmatically. Developers often need to iterate over input records, configure the desired symbology, and export images in common formats such as PNG.
// Prompt: Generate 50 Codabar barcodes from a CSV file, saving each as an individual PNG image.
// Tags: codabar, barcode generation, png, csv, batch, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchGenerator
{
    /// <summary>
    /// Demonstrates batch generation of Codabar barcodes from a CSV file using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Reads up to 50 lines from a CSV file and creates a PNG barcode for each non‑empty entry.
        /// </summary>
        /// <param name="args">Optional command‑line argument specifying the path to the CSV file.</param>
        static void Main(string[] args)
        {
            // Determine CSV file path (first argument or default "input.csv")
            string csvPath = args.Length > 0 ? args[0] : "input.csv";

            // Verify that the CSV file exists before proceeding
            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"CSV file not found: {csvPath}");
                return;
            }

            // Read all lines from the CSV file into an array
            string[] lines = File.ReadAllLines(csvPath);

            // Process a maximum of 50 entries to avoid excessive output
            int maxCount = Math.Min(50, lines.Length);
            for (int i = 0; i < maxCount; i++)
            {
                // Trim whitespace from the current line to obtain the barcode text
                string codeText = lines[i].Trim();

                // Skip empty lines to prevent generating blank barcodes
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                // Build output file name (e.g., barcode_1.png) using a 1‑based index
                string outputFile = $"barcode_{i + 1}.png";

                // Create a barcode generator for Codabar symbology and save the image as PNG
                using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, codeText))
                {
                    generator.Save(outputFile, BarCodeImageFormat.Png);
                }
            }
        }
    }
}