// Title: Batch decode Planet barcodes and generate CSV report
// Description: Generates a set of Planet barcode PNG images, decodes them from a temporary directory, and creates a CSV file summarising each barcode's data.
// Category-Description: This example demonstrates Aspose.BarCode's generation and recognition APIs for the Planet symbology. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to batch‑decode images, and how to extract region information for reporting. Typical use cases include inventory processing, bulk scanning validation, and automated quality checks where developers need to generate test data and verify decoding results programmatically.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, batch, decoding, csv, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation, decoding, and CSV reporting of Planet barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Planet barcodes, decodes them, and writes a CSV summary.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchPlanet_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample numeric texts for Planet barcodes
        List<string> codeTexts = new List<string>
        {
            "123456",
            "9876543210",
            "5555555555",
            "0000012345",
            "9999999999"
        };

        // Generate barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"Planet_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, text))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.FilledBars = true;

                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare CSV report header
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,CodeText,CodeTypeName,ReadingQuality,X,Y,Width,Height,Angle");

        // Define decode type for Planet symbology
        BaseDecodeType decodeType = DecodeType.Planet;

        // Decode each barcode image and append results to CSV
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    // Optional: adjust quality settings here if needed
                    // reader.QualitySettings.AllowIncorrectBarcodes = false;

                    BarCodeResult[] results = reader.ReadBarCodes();

                    // No barcode detected – write empty entry with zero quality
                    if (results.Length == 0)
                    {
                        csvBuilder.AppendLine($"{Path.GetFileName(file)},,,0,,,,,");
                        continue;
                    }

                    // Write each detected barcode's details
                    foreach (BarCodeResult result in results)
                    {
                        var rect = result.Region.Rectangle;
                        string line = string.Format(
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                            Path.GetFileName(file),
                            result.CodeText,
                            result.CodeTypeName,
                            result.ReadingQuality,
                            rect.X,
                            rect.Y,
                            rect.Width,
                            rect.Height,
                            result.Region.Angle);
                        csvBuilder.AppendLine(line);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image loading failed or unsupported file format
                Console.WriteLine($"Warning: Skipping file '{file}'. Reason: {ex.Message}");
                csvBuilder.AppendLine($"{Path.GetFileName(file)},,,0,,,,,");
            }
        }

        // Write CSV report to the temporary folder
        string csvPath = Path.Combine(tempFolder, "PlanetBarcodesReport.csv");
        File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"CSV report generated at: {csvPath}");
    }
}