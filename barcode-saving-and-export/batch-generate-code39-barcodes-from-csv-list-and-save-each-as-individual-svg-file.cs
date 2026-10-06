// Title: Batch generate Code39 barcodes from CSV and save as SVG
// Description: Demonstrates reading a list of Code39 values from a CSV file and creating individual SVG barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the batch barcode generation category of Aspose.BarCode, showcasing how to use BarcodeGenerator with EncodeTypes.Code39 and BarCodeImageFormat.Svg to produce multiple barcode files from data sources. Typical scenarios include inventory labeling, bulk ticket creation, and automated document processing where developers need to export barcodes to vector formats. The pattern is common for reading input files (CSV, database) and iterating over records to generate visual barcode assets.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode, batch, csv, svg, generation, aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads Code39 values from a CSV file and generates individual SVG barcode files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary CSV, reads each line, and saves a Code39 SVG barcode for each entry.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output SVG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Create a temporary CSV file with sample Code39 values
        string csvPath = Path.Combine(outputFolder, "codes.csv");
        var sampleCodes = new List<string> { "ABC123", "12345", "HELLO", "CODE39", "TEST123" };
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            foreach (var code in sampleCodes)
            {
                writer.WriteLine(code);
            }
        }

        // Read the CSV and generate an SVG barcode for each entry
        using (var reader = new StreamReader(csvPath, Encoding.UTF8))
        {
            int index = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // Skip empty lines

                // Assume the first column contains the code text
                string[] parts = line.Split(',');
                string codeText = parts[0].Trim();
                if (string.IsNullOrEmpty(codeText))
                    continue; // Skip lines without a code

                // Build a safe file name for the SVG output
                string safeFileName = $"barcode_{index}_{codeText}.svg";
                string outputPath = Path.Combine(outputFolder, safeFileName);

                try
                {
                    // Initialize the barcode generator for Code39
                    var generator = new BarcodeGenerator(EncodeTypes.Code39);
                    generator.SetCodeText(codeText, Encoding.UTF8);
                    // Save the barcode as an SVG file
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Generated: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
                }

                index++;
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}