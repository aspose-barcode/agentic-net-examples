// Title: Batch processing CSV files to generate Code 16K barcode images
// Description: Demonstrates how to read multiple CSV files from a directory, generate Code 16K barcodes for each line, and save them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Code16K, file I/O, and batch processing. Developers often need to automate barcode creation from data sources such as CSV files, producing image files for inventory, shipping, or labeling workflows. The snippet shows typical API classes (BarcodeGenerator, EncodeTypes, BarCodeImageFormat) and common patterns for handling multiple input files.
// Prompt: Create batch job processing directory of CSV files, outputting Code 16K images.
// Tags: code16k, barcode, generation, png, csv, batch, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a temporary working directory, generates sample CSV files,
/// reads each line, and produces Code 16K barcode images saved as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create a unique temporary working directory with subfolders
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        string csvDir = Path.Combine(workDir, "Csv");
        string outDir = Path.Combine(workDir, "Barcodes");
        Directory.CreateDirectory(csvDir);
        Directory.CreateDirectory(outDir);

        // -------------------------------------------------
        // Create sample CSV files containing barcode data
        // -------------------------------------------------
        var csvFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string csvPath = Path.Combine(csvDir, $"Sample{i}.csv");
            File.WriteAllLines(csvPath, new[]
            {
                $"CODE16K_SAMPLE_{i}_A",
                $"CODE16K_SAMPLE_{i}_B",
                $"CODE16K_SAMPLE_{i}_C"
            });
            csvFiles.Add(csvPath);
        }

        int imageCount = 0; // Counter for successfully generated images

        // -------------------------------------------------
        // Process each CSV file and generate barcodes
        // -------------------------------------------------
        foreach (string csvFile in csvFiles)
        {
            if (!File.Exists(csvFile))
            {
                Console.WriteLine($"File not found: {csvFile}");
                continue;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(csvFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read {csvFile}: {ex.Message}");
                continue;
            }

            // Iterate over each non‑empty line in the CSV file
            for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
            {
                string codeText = lines[lineIdx].Trim();
                if (string.IsNullOrEmpty(codeText))
                    continue; // Skip empty lines

                // Build output image file name based on source CSV and line number
                string imageFileName = $"{Path.GetFileNameWithoutExtension(csvFile)}_Line{lineIdx + 1}.png";
                string imagePath = Path.Combine(outDir, imageFileName);

                try
                {
                    // Initialize the barcode generator for Code 16K symbology
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                    {
                        // Set module (X) dimension to control barcode size
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;

                        // Optional quiet zone settings (defaults are usually sufficient)
                        // generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
                        // generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;

                        // Save the generated barcode as a PNG image
                        generator.Save(imagePath, BarCodeImageFormat.Png);
                    }
                    imageCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for '{codeText}' in {csvFile}: {ex.Message}");
                }
            }
        }

        // -------------------------------------------------
        // Summary output
        // -------------------------------------------------
        Console.WriteLine($"Processed {csvFiles.Count} CSV file(s). Generated {imageCount} barcode image(s) in:");
        Console.WriteLine(outDir);
    }
}