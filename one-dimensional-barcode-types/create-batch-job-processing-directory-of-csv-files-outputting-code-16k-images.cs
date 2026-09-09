// Title: Batch processing of CSV files to generate Code 16K barcode images
// Description: Demonstrates reading multiple CSV files from a temporary directory and creating PNG images for each non‑empty line using the Code 16K symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.Code16K, configure parameters, and save images. Typical use cases include bulk barcode creation from data files such as CSV, automating inventory labeling, or preparing assets for printing. Developers often need to iterate over input records, set barcode properties, and handle file I/O efficiently.
// Prompt: Create batch job processing directory of CSV files, outputting Code 16K images.
// Tags: barcode, code16k, generation, csv, batch, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code 16K barcodes from CSV files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary input/output folders, writes sample CSV files,
    /// reads each line, generates a barcode image, and reports the total count.
    /// </summary>
    static void Main()
    {
        // Create unique temporary input and output folders
        string inputFolder = Path.Combine(Path.GetTempPath(), "BatchCsv_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Prepare sample CSV files with three rows each
        var csvFiles = new List<string>();
        for (int i = 1; i <= 2; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i}.csv");
            File.WriteAllLines(filePath, new[]
            {
                $"Sample{i}_CodeA",
                $"Sample{i}_CodeB",
                $"Sample{i}_CodeC"
            });
            csvFiles.Add(filePath);
        }

        int imageCount = 0;

        // Process each CSV file
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
                // Read all lines from the current CSV file
                lines = File.ReadAllLines(csvFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read {csvFile}: {ex.Message}");
                continue;
            }

            // Generate a barcode image for each non‑empty line
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string codeText = lines[lineIndex].Trim();
                if (string.IsNullOrEmpty(codeText))
                    continue;

                // Build output image file name based on source CSV and row number
                string imageFileName = $"{Path.GetFileNameWithoutExtension(csvFile)}_Row{lineIndex + 1}.png";
                string imagePath = Path.Combine(outputFolder, imageFileName);

                try
                {
                    // Create barcode generator for Code 16K symbology
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                    {
                        // Optional: set module size and aspect ratio for better readability
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;
                        generator.Parameters.Barcode.Code16K.AspectRatio = 10f;

                        // Save the generated barcode as a PNG image
                        generator.Save(imagePath, BarCodeImageFormat.Png);
                    }

                    imageCount++;
                    Console.WriteLine($"Generated: {imagePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating barcode for '{codeText}' in {csvFile}: {ex.Message}");
                }
            }
        }

        // Summarize batch processing results
        Console.WriteLine($"Batch processing completed. Total images generated: {imageCount}");
    }
}