// Title: Batch decode Planet barcodes and generate CSV report
// Description: Demonstrates how to read Planet symbology barcodes from PNG images in a folder and output the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader with DecodeType.Planet, QualitySettings, and BarCodeResult to process multiple images. Developers often need to batch‑process barcodes for inventory, logistics, or data‑entry automation, and this pattern shows how to collect decoded data into a structured report.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, batch decoding, csv, aspose.barcode, barcodereader, decode type

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch decoding of Planet barcodes from PNG files and generating a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans a directory for PNG images, decodes up to 10 Planet barcodes, and writes results to a CSV file.
    /// </summary>
    /// <param name="args">Optional first argument specifying the input directory; defaults to "Barcodes".</param>
    static void Main(string[] args)
    {
        // Determine input directory: use first argument if provided, otherwise default to "Barcodes"
        string inputDir = args.Length > 0 ? args[0] : "Barcodes";

        // Define output CSV file name
        const string outputCsv = "PlanetBarcodesReport.csv";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Retrieve all PNG files in the top level of the directory
        string[] pngFiles = Directory.GetFiles(inputDir, "*.png", SearchOption.TopDirectoryOnly);
        if (pngFiles.Length == 0)
        {
            Console.WriteLine($"No PNG files found in directory: {inputDir}");
            return;
        }

        // Limit processing to a safe sample size (maximum 10 files)
        int maxFiles = Math.Min(pngFiles.Length, 10);

        // Open the CSV writer with UTF‑8 encoding
        using (var writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
        {
            // Write CSV header row
            writer.WriteLine("\"FileName\",\"CodeText\",\"CodeTypeName\",\"ReadingQuality\",\"Confidence\"");

            // Process each selected PNG file
            for (int i = 0; i < maxFiles; i++)
            {
                string filePath = pngFiles[i];
                string fileName = Path.GetFileName(filePath);

                // Defensive check – the file should exist because it came from GetFiles
                if (!File.Exists(filePath))
                {
                    continue;
                }

                // Initialize barcode reader for Planet symbology
                using (var reader = new BarCodeReader(filePath, DecodeType.Planet))
                {
                    // Apply a normal quality preset (optional but recommended)
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Decode all barcodes in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        // No barcode detected – write a row with empty fields
                        writer.WriteLine($"{Quote(fileName)},\"\",\"\",,\"\"");
                        continue;
                    }

                    // Write up to three detected barcodes per image for safety
                    int maxResults = Math.Min(results.Length, 3);
                    for (int r = 0; r < maxResults; r++)
                    {
                        var result = results[r];
                        string codeText = result.CodeText ?? "";
                        string codeType = result.CodeTypeName ?? "";
                        string readingQuality = result.ReadingQuality
                            .ToString(System.Globalization.CultureInfo.InvariantCulture);
                        string confidence = result.Confidence.ToString();

                        writer.WriteLine($"{Quote(fileName)},{Quote(codeText)},{Quote(codeType)},{Quote(readingQuality)},{Quote(confidence)}");
                    }
                }
            }
        }

        Console.WriteLine($"Decoding completed. Report saved to: {outputCsv}");
    }

    // Helper to escape CSV fields by surrounding with double quotes and escaping internal quotes
    private static string Quote(string field)
    {
        if (field == null) field = "";
        string escaped = field.Replace("\"", "\"\"");
        return $"\"{escaped}\"";
    }
}