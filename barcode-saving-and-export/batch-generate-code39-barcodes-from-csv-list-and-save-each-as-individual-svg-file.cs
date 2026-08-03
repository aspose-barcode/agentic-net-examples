// Title: Batch generate Code39 barcodes from CSV to SVG files
// Description: Reads a CSV file, generates a Code39 barcode for each entry, and saves each barcode as an individual SVG file.
// Category-Description: This example demonstrates batch barcode generation using Aspose.BarCode. It showcases the BarcodeGenerator class with EncodeTypes.Code39 and BarCodeImageFormat.Svg to create SVG images. Typical scenarios include bulk creation of product labels, inventory tags, or any situation where a list of identifiers must be turned into barcodes. Developers working with Aspose.BarCode often need to read data sources, generate barcodes programmatically, and store them in various image formats.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode, generation, svg, csv, aspose.barcode, batch-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to read a CSV file, generate a Code39 barcode for each line,
/// and save each barcode as an individual SVG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes command‑line arguments,
    /// validates input, generates barcodes, and writes SVG files to the output folder.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// args[0] – path to the input CSV file (default: "input.csv").
    /// args[1] – path to the output folder for SVG files (default: "Barcodes").
    /// </param>
    static void Main(string[] args)
    {
        // Default input CSV file and output directory
        string csvPath = "input.csv";
        string outputFolder = "Barcodes";

        // Override defaults with command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            csvPath = args[0];
        }
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputFolder = args[1];
        }

        // Verify that the CSV file exists before proceeding
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Ensure the output directory exists (creates it if necessary)
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            // Skip empty or whitespace‑only lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Assume the first column contains the Code39 value
            string[] parts = line.Split(',');
            string codeText = parts[0].Trim();

            // Skip lines where the first column is empty
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Build a safe file name for the SVG output
            string safeFileName = GetSafeFileName(codeText) + ".svg";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            // Generate the Code39 barcode and save it directly as SVG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
            {
                // Do not throw if the code text contains minor issues (e.g., unsupported characters)
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the barcode image in SVG format
                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            Console.WriteLine($"Generated: {outputPath}");
        }
    }

    /// <summary>
    /// Replaces characters that are invalid in file names with an underscore,
    /// ensuring the generated file name is safe for the file system.
    /// </summary>
    /// <param name="name">Original file name derived from the barcode text.</param>
    /// <returns>A sanitized file name with invalid characters replaced.</returns>
    private static string GetSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}