// Title: Batch processing CSV files to generate Code 16K barcode images
// Description: This example reads all CSV files from a directory, creates a Code 16K barcode for each line, and saves the images as PNG files.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. It showcases the BarcodeGenerator class with EncodeTypes.Code16K, handling file I/O, and configuring barcode parameters. Ideal for developers needing to automate barcode creation from data files in bulk.
// Prompt: Create batch job processing directory of CSV files, outputting Code 16K images.
// Tags: code16k, batch-processing, png, barcodegenerator, aspnet.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that processes CSV files in a directory,
/// generates Code 16K barcodes for each line, and saves the results as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional input and output directory arguments, creates sample data if needed,
    /// and iterates through each CSV file to produce barcode images.
    /// </summary>
    /// <param name="args">
    /// Command‑line arguments where:
    /// args[0] – input directory path (optional),
    /// args[1] – output directory path (optional).
    /// </param>
    static void Main(string[] args)
    {
        // Determine input and output directories (fallback to sample folders)
        string inputDir = args.Length > 0 ? args[0] : "InputCsv";
        string outputDir = args.Length > 1 ? args[1] : "OutputBarcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If the input directory does not exist, create it and add a sample CSV file
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
            string sampleCsvPath = Path.Combine(inputDir, "sample.csv");
            File.WriteAllLines(sampleCsvPath, new[]
            {
                "ABC1234567890",
                "XYZ9876543210",
                "CODE16KTEST"
            });
        }

        // Process each CSV file in the input directory
        foreach (string csvFilePath in Directory.GetFiles(inputDir, "*.csv"))
        {
            // Base name of the CSV file without extension (used for output naming)
            string csvFileName = Path.GetFileNameWithoutExtension(csvFilePath);
            // Read all lines from the current CSV file
            string[] lines = File.ReadAllLines(csvFilePath);

            int lineIndex = 0;
            foreach (string rawLine in lines)
            {
                // Trim whitespace and skip empty lines
                string codeText = rawLine.Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                // Generate Code16K barcode for the current line
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Optional: set aspect ratio (default is 1.0)
                    generator.Parameters.Barcode.Code16K.AspectRatio = 1f;

                    // Build output file name: <csvname>_line<index>.png
                    string outputFileName = $"{csvFileName}_line{lineIndex}.png";
                    string outputPath = Path.Combine(outputDir, outputFileName);

                    // Save the barcode image as PNG
                    generator.Save(outputPath);
                }

                lineIndex++;
            }
        }

        // Indicate completion
        Console.WriteLine("Barcode generation completed.");
    }
}