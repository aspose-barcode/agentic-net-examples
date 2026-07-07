// Title: Batch Mailmark Barcode Reader to CSV
// Description: Reads Mailmark barcodes from images in a folder and writes decoded data to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, demonstrating how to use BarCodeReader with DecodeType.Mailmark to process multiple images. Typical use cases include bulk scanning of Mailmark symbols for mail sorting or inventory tracking, where developers need to extract barcode data and export it for further analysis. The example showcases file handling, supported image filtering, and CSV output generation, common tasks for batch barcode processing solutions.
// Prompt: Batch read multiple Mailmark barcode images from a directory and output their decoded data to CSV.
// Tags: mailmark, barcode, batch, csv, reading, aspose.barcode, decode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch reading of Mailmark barcodes from a directory and exporting results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for supported image files, decodes Mailmark barcodes,
    /// and writes the extracted information to a CSV file.
    /// </summary>
    static void Main()
    {
        // Input directory containing Mailmark barcode images.
        string inputDir = "MailmarkImages";

        // Output CSV file path.
        string outputCsv = "MailmarkResults.csv";

        // Verify that the input directory exists.
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Retrieve all files in the directory (any extension) and filter by supported image extensions.
        string[] imageFiles = Directory.GetFiles(inputDir, "*.*", SearchOption.TopDirectoryOnly);
        var supportedExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };
        var filesToProcess = new System.Collections.Generic.List<string>();

        foreach (var file in imageFiles)
        {
            // Include file only if its extension matches one of the supported image types.
            if (Array.Exists(supportedExtensions, ext => ext.Equals(Path.GetExtension(file), StringComparison.OrdinalIgnoreCase)))
                filesToProcess.Add(file);
        }

        // If no supported image files were found, inform the user and exit.
        if (filesToProcess.Count == 0)
        {
            Console.WriteLine("No image files found to process.");
            return;
        }

        // Create (or overwrite) the CSV file and write the header row.
        using (var writer = new StreamWriter(outputCsv, false))
        {
            writer.WriteLine("FileName,CodeText,CodeType,Confidence,ReadingQuality");

            // Process each image file individually.
            foreach (var filePath in filesToProcess)
            {
                // Double‑check that the file still exists before attempting to read it.
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found (skipped): {filePath}");
                    continue;
                }

                try
                {
                    // Initialize the barcode reader for the Mailmark symbology.
                    using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
                    {
                        // Attempt to read all barcodes present in the image.
                        var results = reader.ReadBarCodes();

                        // If no barcodes were detected, write an empty data line for this file.
                        if (results.Length == 0)
                        {
                            writer.WriteLine($"{Path.GetFileName(filePath)},,,," );
                            continue;
                        }

                        // Write a CSV line for each detected barcode.
                        foreach (var result in results)
                        {
                            // Replace commas in the decoded text to preserve CSV column integrity.
                            string codeText = result.CodeText?.Replace(",", " ");
                            writer.WriteLine($"{Path.GetFileName(filePath)},{codeText},{result.CodeTypeName},{result.Confidence},{result.ReadingQuality}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any processing errors and write an error entry to the CSV.
                    Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                    writer.WriteLine($"{Path.GetFileName(filePath)},Error,,," );
                }
            }
        }

        // Inform the user that processing has completed.
        Console.WriteLine($"Processing complete. Results saved to {outputCsv}");
    }
}