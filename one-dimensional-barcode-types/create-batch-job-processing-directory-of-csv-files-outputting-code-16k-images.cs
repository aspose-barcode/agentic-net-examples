// Title: Batch processing CSV files to generate Code 16K barcode images
// Description: Demonstrates how to read multiple CSV files from a directory, extract the text, and create Code 16K barcode PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating the use of BarcodeGenerator with EncodeTypes.Code16K, setting barcode parameters, and saving images. Typical scenarios include generating barcodes from data files in bulk, such as inventory lists or shipping manifests. Developers often need to automate barcode creation from structured inputs, adjust dimensions, and output common image formats.
// Prompt: Create batch job processing directory of CSV files, outputting Code 16K images.
// Tags: code16k, batch, csv, png, barcodegenerator, aspose.barcodes

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that processes a set of CSV files and generates Code 16K barcode images for each entry.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary input/output folders, writes sample CSV files, reads each file,
    /// generates a Code 16K barcode, and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create unique temporary folders for input CSVs and output images
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BatchCsvInput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------
        // Generate sample CSV files (each containing a single line of text)
        // --------------------------------------------------------------
        List<string> csvFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i}.csv");
            File.WriteAllText(filePath, $"SampleCode{i}");
            csvFiles.Add(filePath);
        }

        Console.WriteLine($"Input folder: {inputFolder}");
        Console.WriteLine($"Output folder: {outputFolder}");

        // --------------------------------------------------------------
        // Process each CSV file: read text, create barcode, save as PNG
        // --------------------------------------------------------------
        foreach (string csvFile in csvFiles)
        {
            try
            {
                if (!File.Exists(csvFile))
                {
                    Console.WriteLine($"File not found: {csvFile}");
                    continue;
                }

                // Read and trim the code text from the CSV file
                string codeText = File.ReadAllText(csvFile).Trim();
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Empty code text in file: {csvFile}");
                    continue;
                }

                // Initialize the barcode generator for Code 16K with the extracted text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Configure barcode appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;

                    // Determine output file path and save the image as PNG
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(csvFile) + ".png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{csvFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}