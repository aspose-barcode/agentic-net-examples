// Title: Batch decode Planet barcodes and export results to CSV
// Description: Demonstrates generating sample Planet barcode images, decoding them in bulk, and writing a CSV report with details such as confidence and angle.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for batch decoding, and common result properties like CodeText, Confidence, and Region. Developers often need to process multiple images, extract barcode data, and produce machine‑readable reports for inventory, logistics, or analytics.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, batch decoding, csv, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and decoding of Planet barcodes, producing a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample Planet barcode images, decodes them, and writes a CSV report.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string imageFolder = Path.Combine(Path.GetTempPath(), "PlanetBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(imageFolder);

        // List to hold the generated image file paths
        List<string> imageFiles = new List<string>();

        // Generate sample Planet barcodes (5 samples)
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"PLANET{i:D3}";
            string filePath = Path.Combine(imageFolder, $"planet_{i}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
            {
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // Prepare CSV report file (outside the image folder)
        string csvPath = Path.Combine(Path.GetTempPath(), "PlanetReport_" + Guid.NewGuid().ToString("N") + ".csv");

        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeText,CodeType,Confidence,ReadingQuality,Angle");

            // Process each image file
            foreach (string file in imageFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                try
                {
                    // Initialize reader for Planet symbology
                    using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Planet))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            // No barcode detected; write a line indicating empty result
                            writer.WriteLine($"{Path.GetFileName(file)},,,0,0,0");
                            continue;
                        }

                        // Write a CSV line for each detected barcode
                        foreach (BarCodeResult result in results)
                        {
                            // Extract region rectangle and angle
                            var bounds = result.Region.Rectangle;
                            double angle = result.Region.Angle;

                            // Build CSV line with escaped fields
                            string line = $"{Path.GetFileName(file)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)},{result.Confidence},{result.ReadingQuality},{angle}";
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed or unsupported format; skip file
                    Console.WriteLine($"Skipping file {Path.GetFileName(file)}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Unexpected error; report and continue
                    Console.WriteLine($"Error processing file {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"CSV report generated at: {csvPath}");
        // Cleanup: optionally delete the temporary image folder
        // Directory.Delete(imageFolder, true);
    }

    // Helper to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}