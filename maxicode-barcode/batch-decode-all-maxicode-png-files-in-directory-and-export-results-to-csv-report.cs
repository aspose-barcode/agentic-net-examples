// Title: Batch decode MaxiCode PNG files and generate CSV report
// Description: This example generates sample MaxiCode barcodes, decodes them in bulk, and writes the results to a CSV file. It demonstrates how to use Aspose.BarCode for batch processing and reporting.
// Category-Description: Shows batch barcode recognition using Aspose.BarCode's BarCodeReader for MaxiCode symbology, combined with barcode generation via BarcodeGenerator. Typical use cases include automated scanning of multiple images and exporting results for analysis or integration. Developers often need to process directories of images, handle errors gracefully, and produce structured reports such as CSV.
// Prompt: Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report.
// Tags: maxicode, batch, decode, csv, report, barcodereader, barcodegenerator, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch decoding of MaxiCode PNG files and exporting results to a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, decodes them, and writes a CSV report.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files and the report
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchMaxiCode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample MaxiCode PNG files
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"maxicode_{i}.png");
            string codeText = $"Sample{i}";
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare CSV header row
        List<string[]> csvRows = new List<string[]>();
        csvRows.Add(new[] { "FileName", "CodeText" });

        // Decode each generated file and collect results
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.MaxiCode))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        csvRows.Add(new[] { Path.GetFileName(file), result.CodeText });
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Write collected data to a CSV report file
        string reportPath = Path.Combine(tempFolder, "report.csv");
        using (StreamWriter writer = new StreamWriter(reportPath))
        {
            foreach (string[] row in csvRows)
            {
                string line = string.Join(",", row.Select(v => $"\"{v.Replace("\"", "\"\"")}\""));
                writer.WriteLine(line);
            }
        }

        Console.WriteLine($"CSV report generated at: {reportPath}");
    }
}