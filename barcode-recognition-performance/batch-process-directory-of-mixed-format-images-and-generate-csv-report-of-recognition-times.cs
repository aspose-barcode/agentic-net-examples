// Title: Batch barcode recognition with CSV timing report
// Description: Demonstrates processing a directory of mixed‑format images, recognizing any barcodes, and writing a CSV file that records each barcode and the time taken for recognition.
// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
// Tags: barcode, recognition, csv, batch, aspose.barcode, file-io

using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that scans a folder of images for barcodes,
/// measures recognition time for each file, and writes the results to a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments for input directory and output CSV file.
    /// </summary>
    /// <param name="args">
    /// args[0] – input directory (default: "Images")
    /// args[1] – output CSV file path (default: "report.csv")
    /// </param>
    static void Main(string[] args)
    {
        // Determine input directory (first argument) or fall back to default "Images"
        string inputDir = args.Length > 0 ? args[0] : "Images";

        // Determine output CSV file (second argument) or fall back to default "report.csv"
        string outputCsv = args.Length > 1 ? args[1] : "report.csv";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Define supported image file extensions
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tif", ".tiff" };

        // Retrieve up to 10 image files matching the supported extensions
        var files = Directory.GetFiles(inputDir)
                             .Where(f => extensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                             .Take(10)
                             .ToArray();

        // Exit if no matching image files were found
        if (files.Length == 0)
        {
            Console.WriteLine("No image files found to process.");
            return;
        }

        // Open the CSV writer; using ensures the file is closed properly
        using (StreamWriter writer = new StreamWriter(outputCsv))
        {
            // Write CSV header row
            writer.WriteLine("FileName,BarcodeType,CodeText,RecognitionTimeMs");

            // Process each image file
            foreach (string filePath in files)
            {
                // Skip files that cannot be accessed
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found, skipping: {filePath}");
                    continue;
                }

                // Start timing the recognition operation
                Stopwatch sw = Stopwatch.StartNew();

                // Initialize the barcode reader for all supported symbologies
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Perform barcode detection and collect results
                    var results = reader.ReadBarCodes().ToArray();
                    sw.Stop(); // Stop timing after reading completes

                    if (results.Length == 0)
                    {
                        // No barcode detected – log the file with empty fields
                        writer.WriteLine($"{Path.GetFileName(filePath)},,,{sw.ElapsedMilliseconds}");
                        Console.WriteLine($"No barcode found in {Path.GetFileName(filePath)} (Time: {sw.ElapsedMilliseconds} ms)");
                    }
                    else
                    {
                        // Write a CSV line for each detected barcode
                        foreach (var result in results)
                        {
                            // Escape commas in the decoded text to preserve CSV format
                            string codeText = result.CodeText?.Replace(",", "&#44;") ?? string.Empty;
                            writer.WriteLine($"{Path.GetFileName(filePath)},{result.CodeTypeName},{codeText},{sw.ElapsedMilliseconds}");
                            Console.WriteLine($"Processed {Path.GetFileName(filePath)} - Type: {result.CodeTypeName}, Text: {result.CodeText}, Time: {sw.ElapsedMilliseconds} ms");
                        }
                    }
                }
            }
        }

        // Inform the user where the CSV report was saved
        Console.WriteLine($"CSV report generated at: {Path.GetFullPath(outputCsv)}");
    }
}