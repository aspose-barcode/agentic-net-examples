// Title: Batch barcode generation, recognition, and state export to XML
// Description: Demonstrates creating multiple barcode images, reading them, exporting the recognition state to XML, and logging the process.
// Category-Description: Shows a typical batch processing workflow using Aspose.BarCode. It covers image generation with BarcodeGenerator, barcode reading with BarCodeReader, exporting recognition state via ExportToXml, and logging. Developers working with bulk barcode operations, automated validation, or archival of recognition data will find this pattern useful.
// Prompt: Develop a method that loops through a directory, sets each image, exports state to XML, and logs results.
// Tags: barcode generation, barcode recognition, batch processing, xml export, aspose.barcode, code128, logging

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample Code128 barcodes, reads them,
/// exports the recognition state to XML files, and logs the entire process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch barcode workflow.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a dedicated temporary folder for generated files and logs
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> files = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            BaseEncodeType encode = EncodeTypes.Code128;
            using (BarcodeGenerator generator = new BarcodeGenerator(encode, $"Sample{i}"))
            {
                // Set barcode visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        // Prepare a log file to record processing details
        string logPath = Path.Combine(tempFolder, "process_log.txt");
        File.WriteAllText(logPath, $"Processing started at {DateTime.Now}{Environment.NewLine}");

        // Process each generated barcode image
        int index = 0;
        foreach (string file in files)
        {
            index++;
            if (!File.Exists(file))
            {
                // Log missing file and continue with the next one
                File.AppendAllText(logPath, $"File not found: {file}{Environment.NewLine}");
                continue;
            }

            try
            {
                // Initialize a reader for Code128 barcodes
                BaseDecodeType decode = DecodeType.Code128;
                using (BarCodeReader reader = new BarCodeReader(file, decode))
                {
                    // Read all barcodes from the image
                    var results = reader.ReadBarCodes();
                    File.AppendAllText(logPath, $"File {index}: {Path.GetFileName(file)} - Barcodes read: {results.Length}{Environment.NewLine}");

                    // Log details of each detected barcode
                    foreach (var result in results)
                    {
                        File.AppendAllText(logPath, $"   Type: {result.CodeTypeName}, Text: {result.CodeText}{Environment.NewLine}");
                    }

                    // Export the recognition state to an XML file for later analysis
                    string xmlPath = Path.Combine(tempFolder, $"state_{index}.xml");
                    reader.ExportToXml(xmlPath);
                    File.AppendAllText(logPath, $"   Exported state to {xmlPath}{Environment.NewLine}");
                }
            }
            catch (ArgumentException ex)
            {
                // Log argument errors (e.g., unsupported format)
                File.AppendAllText(logPath, $"ArgumentException for file {file}: {ex.Message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                File.AppendAllText(logPath, $"Exception for file {file}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Finalize the log with a completion timestamp
        File.AppendAllText(logPath, $"Processing completed at {DateTime.Now}{Environment.NewLine}");
        Console.WriteLine($"Log written to: {logPath}");
    }
}