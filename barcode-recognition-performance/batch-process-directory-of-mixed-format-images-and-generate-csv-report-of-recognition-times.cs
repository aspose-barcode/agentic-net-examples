using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that scans a directory of images for barcodes and generates a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans up to 10 image files in the specified directory (or "Images" by default),
    /// attempts to read any barcodes using Aspose.BarCode, and writes the results to a CSV file.
    /// </summary>
    /// <param name="args">Optional command‑line arguments. The first argument can specify the images directory.</param>
    static void Main(string[] args)
    {
        // Determine the directory to process (use first argument or default to "Images")
        string imagesDir = args.Length > 0 ? args[0] : "Images";

        // Verify that the directory exists before proceeding
        if (!Directory.Exists(imagesDir))
        {
            Console.WriteLine($"Directory not found: {imagesDir}");
            return;
        }

        // Define supported image file extensions
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".gif" };

        // Retrieve all files in the directory (filtering will be done later)
        string[] files = Directory.GetFiles(imagesDir);

        // Limit processing to a safe sample size (10 files max)
        const int maxFiles = 10;
        int processedCount = 0;

        // Prepare the CSV output file path
        string csvPath = Path.Combine(imagesDir, "BarcodeReport.csv");

        // Open a StreamWriter for the CSV file (overwrite if it already exists)
        using (var writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header line
            writer.WriteLine("FileName,BarcodeType,CodeText,RecognitionTimeMs");

            // Iterate over each file found in the directory
            foreach (string filePath in files)
            {
                // Stop processing once the maximum number of files has been reached
                if (processedCount >= maxFiles)
                    break;

                // Check file extension against the list of supported types
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                    continue; // skip unsupported files

                // Ensure the file still exists (it might have been removed meanwhile)
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Load the image into a bitmap object
                    using (var bitmap = new Bitmap(filePath))
                    {
                        // Create a barcode reader that attempts to decode all supported barcode types
                        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Measure the time taken to read barcodes
                            var stopwatch = Stopwatch.StartNew();
                            var results = reader.ReadBarCodes();
                            stopwatch.Stop();

                            if (results.Length == 0)
                            {
                                // No barcode detected – write an entry with empty fields for type and text
                                writer.WriteLine($"{Path.GetFileName(filePath)},,,{stopwatch.ElapsedMilliseconds}");
                            }
                            else
                            {
                                // One or more barcodes were detected; write a line for each result
                                foreach (var result in results)
                                {
                                    // Replace commas in the output fields to avoid breaking CSV format
                                    string type = result.CodeTypeName?.Replace(",", " ");
                                    string text = result.CodeText?.Replace(",", " ");
                                    writer.WriteLine($"{Path.GetFileName(filePath)},{type},{text},{stopwatch.ElapsedMilliseconds}");
                                }
                            }
                        }
                    }

                    // Increment the count of successfully processed files
                    processedCount++;
                }
                catch (Exception ex)
                {
                    // Log any errors (e.g., unsupported image format) and continue with the next file
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }
        }

        // Inform the user where the report has been saved
        Console.WriteLine($"Barcode recognition report generated at: {csvPath}");
    }
}