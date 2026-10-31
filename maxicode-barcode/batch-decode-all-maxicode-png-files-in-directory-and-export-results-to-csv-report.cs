// Title: Batch decode MaxiCode PNG files and generate CSV report
// Description: Demonstrates how to generate sample MaxiCode barcodes, decode them in bulk, and export the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating MaxiCode images and BarCodeReader for batch decoding. Typical scenarios include automated barcode processing pipelines, inventory management, and reporting where developers need to read multiple barcodes from a folder and produce structured output such as CSV.
// Prompt: Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report.
// Tags: maxicode, batch decoding, csv export, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates sample MaxiCode PNG files, decodes them, and writes a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them, and saves results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated files and the report
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchMaxiCode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample MaxiCode PNG files
        List<string> generatedFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"MaxiCode_{i}.png");
            string codeText = $"Sample{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Prepare CSV header
        List<string> csvLines = new List<string>();
        csvLines.Add("FilePath,CodeText,CodeType");

        // Decode each generated file and collect results
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: file not found - {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.MaxiCode))
                {
                    bool anyResult = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        anyResult = true;
                        string line = $"\"{file}\",\"{result.CodeText}\",\"{result.CodeType}\"";
                        csvLines.Add(line);
                    }

                    if (!anyResult)
                    {
                        // No barcode detected; add empty entry for the file
                        string line = $"\"{file}\",\"\",\"\"";
                        csvLines.Add(line);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle files that cannot be read as images
                Console.WriteLine($"Warning: could not read file {file}: {ex.Message}");
                string line = $"\"{file}\",\"\",\"\"";
                csvLines.Add(line);
            }
        }

        // Write the collected data to a CSV report file
        string csvPath = Path.Combine(tempFolder, "Report.csv");
        File.WriteAllLines(csvPath, csvLines, Encoding.UTF8);

        Console.WriteLine($"Batch decoding completed. Report saved to: {csvPath}");
    }
}