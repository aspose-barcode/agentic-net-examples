// Title: Batch barcode image generation and recognition timing report
// Description: Generates sample barcode images in multiple formats, reads each image to recognize barcodes, measures recognition time, and outputs a CSV report.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation, BarCodeReader for multi-format recognition, and standard .NET I/O for reporting. Typical use cases include performance benchmarking, bulk validation of barcode assets, and automated documentation of recognition results. Developers often need to generate test images, iterate over them, capture timing metrics, and export the data for analysis.
// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
// Tags: barcode, generation, recognition, batch, csv, aspose.barcode, code128, imageformats

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of barcode images, recognition timing, and CSV reporting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, measures recognition performance, and writes a CSV report.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images and the report
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold full paths of generated barcode image files
        var files = new List<string>();

        // Define the image formats to generate (format enum and file extension)
        var formats = new (BarCodeImageFormat format, string extension)[]
        {
            (BarCodeImageFormat.Png, "png"),
            (BarCodeImageFormat.Jpeg, "jpg"),
            (BarCodeImageFormat.Bmp, "bmp"),
            (BarCodeImageFormat.Gif, "gif"),
            (BarCodeImageFormat.Tiff, "tiff")
        };

        // Set up barcode generation parameters
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Generate a barcode image for each format and store its path
        for (int i = 0; i < formats.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.{formats[i].extension}");
            var generator = new BarcodeGenerator(encodeType, codeText);
            generator.Save(filePath, formats[i].format);
            files.Add(filePath);
        }

        // Prepare a StringBuilder to build the CSV report header
        var sb = new StringBuilder();
        sb.AppendLine("FileName,Format,RecognitionTimeMs,BarcodesFound");

        // Process each generated file, measure recognition time, and record results
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];

            // Verify the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: File not found - {file}");
                continue;
            }

            var stopwatch = new Stopwatch();
            int foundCount = 0;

            try
            {
                // Use BarCodeReader to recognize all supported barcode types in the image
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    stopwatch.Start();
                    BarCodeResult[] results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    // Count the number of barcodes detected (null‑safe)
                    foundCount = results?.Length ?? 0;
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be read as an image
                Console.WriteLine($"Error reading file {file}: {ex.Message}");
                continue;
            }

            // Extract file name and format for the CSV line
            string fileName = Path.GetFileName(file);
            string format = Path.GetExtension(file).TrimStart('.').ToUpperInvariant();

            // Append the result line to the CSV content
            sb.AppendLine($"{fileName},{format},{stopwatch.ElapsedMilliseconds},{foundCount}");
        }

        // Write the CSV report to the temporary folder
        string csvPath = Path.Combine(tempFolder, "RecognitionReport.csv");
        File.WriteAllText(csvPath, sb.ToString());

        Console.WriteLine($"CSV report saved to: {csvPath}");
    }
}