// Title: Batch generation of GS1 Composite barcodes from CSV
// Description: Reads a CSV file where each line contains a GS1 Composite codetext and creates a PNG barcode image for each record.
// Category-Description: Demonstrates Aspose.BarCode batch processing for GS1 Composite symbology. Shows how to use BarcodeGenerator, configure linear and 2D components, and save images. Useful for developers automating barcode creation from data sources such as CSV, databases, or APIs.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, barcode generation, csv batch, png output, aspose.barcode, encoding

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program entry point for generating GS1 Composite barcodes from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Reads the CSV file (or creates a sample), generates a barcode for each line, and saves PNG images to the output folder.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can specify the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV file path (first argument or default)
        string csvPath = args.Length > 0 ? args[0] : "input.csv";

        // If the CSV does not exist, create a small sample file
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found at '{csvPath}'. Creating sample file.");
            var sampleLines = new List<string>
            {
                // Each line contains the full GS1 Composite codetext (linear|2D)
                "(01)00123456789012|(21)A12345678",
                "(01)00012345678905|(21)B98765432",
                "(01)01234567890128|(21)C11223344"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Prepare output directory
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all non‑empty lines from the CSV
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;
        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
                continue; // skip empty lines

            // The line is expected to be the full GS1 Composite codetext
            string codeText = line;

            // Generate the barcode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Configure linear and 2D components
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Optional: enforce GS1 encoding for the 2D component
                generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = true;

                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the image
                string outputPath = Path.Combine(outputDir, $"barcode_{index}.png");
                generator.Save(outputPath);
                Console.WriteLine($"Saved barcode #{index} to '{outputPath}'.");
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}