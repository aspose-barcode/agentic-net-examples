// Title: Batch barcode recognition and CSV timing report
// Description: Demonstrates generating multiple barcode images, recognizing them in a batch, and creating a CSV file that records the processing time and number of barcodes detected per image.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator to create barcodes, BarCodeReader for multi‑format recognition, and standard .NET I/O for handling files and CSV output. Typical use cases include automated scanning pipelines, performance benchmarking, and bulk data extraction where developers need to process many images efficiently.
/// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
/// Tags: barcode, batch processing, csv, performance, recognition, aspose.barcode, barcodegenerator, barcodereader, decode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and recognition of barcodes, producing a CSV report with timing information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcode images, reads them, measures recognition time, and writes results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        string imagesFolder = Path.Combine(workFolder, "Images");
        Directory.CreateDirectory(imagesFolder);

        // Define sample barcodes to generate (file name, symbology, text)
        var samples = new List<(string FileName, BaseEncodeType Encode, string Text)>
        {
            ("qr.png", EncodeTypes.QR, "Sample QR"),
            ("code128.png", EncodeTypes.Code128, "CODE128"),
            ("datamatrix.png", EncodeTypes.DataMatrix, "DM12345"),
            ("aztec.png", EncodeTypes.Aztec, "AZTEC"),
            ("pdf417.png", EncodeTypes.Pdf417, "PDF417 Sample")
        };

        // Generate barcode images and collect their file paths
        var imageFiles = new List<string>();
        foreach (var (fileName, encode, text) in samples)
        {
            string filePath = Path.Combine(imagesFolder, fileName);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Set common barcode parameters
                generator.Parameters.Barcode.XDimension.Point = 2.0f;
                generator.Parameters.Barcode.FilledBars = true;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode to a memory stream, then to a file
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fileStream);
                    }
                }
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV report file
        string reportPath = Path.Combine(workFolder, "Report.csv");
        using (var writer = new StreamWriter(reportPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,ElapsedMilliseconds,BarcodesDetected");

            // Process each generated image
            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                Stopwatch sw = new Stopwatch();
                int detectedCount = 0;

                try
                {
                    // Initialize reader for all supported barcode types
                    using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                    {
                        // Use high‑performance quality settings
                        reader.QualitySettings = QualitySettings.HighPerformance;

                        // Measure recognition time
                        sw.Start();
                        BarCodeResult[] results = reader.ReadBarCodes();
                        sw.Stop();

                        // Count detected barcodes
                        if (results != null)
                        {
                            detectedCount = results.Length;
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Image loading failed – skip this file
                    Console.WriteLine($"Skipping file due to load error: {ex.Message}");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error processing {imagePath}: {ex.Message}");
                    continue;
                }

                // Write result line to CSV and console
                string line = $"{Path.GetFileName(imagePath)},{sw.ElapsedMilliseconds},{detectedCount}";
                writer.WriteLine(line);
                Console.WriteLine(line);
            }
        }

        Console.WriteLine($"CSV report generated at: {reportPath}");
    }
}