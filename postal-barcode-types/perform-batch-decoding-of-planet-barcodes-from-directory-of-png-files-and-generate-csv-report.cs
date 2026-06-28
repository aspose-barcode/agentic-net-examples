using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that scans PNG images for Planet barcodes and generates a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: input directory and output CSV path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Determine input directory; default to "Barcodes" if not supplied.
        string inputDirectory = args.Length > 0 ? args[0] : "Barcodes";

        // Determine output CSV file path; default to "report.csv" if not supplied.
        string outputCsvPath = args.Length > 1 ? args[1] : "report.csv";

        // Verify that the input directory exists.
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Retrieve all PNG files in the directory and limit processing to the first 10.
        string[] allPngFiles = Directory.GetFiles(inputDirectory, "*.png");
        if (allPngFiles.Length == 0)
        {
            Console.WriteLine($"No PNG files found in directory: {inputDirectory}");
            return;
        }

        // Take up to 10 files for a safe sample size.
        var pngFiles = allPngFiles.Take(10).ToArray();

        // Open the CSV file for writing.
        using (var writer = new StreamWriter(outputCsvPath))
        {
            // Write CSV header line.
            writer.WriteLine("FileName,CodeType,CodeText,Confidence,ReadingQuality");

            // Process each selected PNG file.
            foreach (string filePath in pngFiles)
            {
                // Skip files that cannot be found (defensive check).
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found (skipped): {filePath}");
                    continue;
                }

                // Create a barcode reader configured for Planet symbology.
                using (var reader = new BarCodeReader(filePath, DecodeType.Planet))
                {
                    // Read all barcodes from the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were detected, record this in the CSV.
                    if (results.Length == 0)
                    {
                        writer.WriteLine($"{Path.GetFileName(filePath)},,,No barcode detected,");
                        Console.WriteLine($"No Planet barcode found in: {filePath}");
                        continue;
                    }

                    // Write each detected barcode's details to the CSV.
                    foreach (var result in results)
                    {
                        string line = string.Format(
                            "{0},{1},{2},{3},{4}",
                            Path.GetFileName(filePath),
                            result.CodeTypeName,
                            result.CodeText,
                            result.Confidence,
                            result.ReadingQuality);

                        writer.WriteLine(line);
                        Console.WriteLine($"Decoded from {Path.GetFileName(filePath)}: {result.CodeText}");
                    }
                }
            }
        }

        // Inform the user that the report has been generated.
        Console.WriteLine($"CSV report generated at: {outputCsvPath}");
    }
}