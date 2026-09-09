// Title: Batch barcode image processing with CSV timing report
// Description: Demonstrates how to generate sample barcode images, recognize them in batch, and produce a CSV file containing recognition times and barcode counts.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for image creation and BarCodeReader for multi‑format barcode recognition. Typical use cases include automated scanning of large image sets, performance measurement, and reporting. Developers often need to handle various symbologies, measure processing speed, and export results, which this sample illustrates.
// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
// Tags: barcode, generation, recognition, csv, batch, aspose.barcode, aspose.drawing, performance

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation, recognition, and reporting of barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them, measures recognition time, and writes a CSV report.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and the report
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images of different symbologies
        List<string> imageFiles = new List<string>();
        GenerateBarcodeImage(tempFolder, "QR_Sample.png", EncodeTypes.QR, "QR12345");
        imageFiles.Add(Path.Combine(tempFolder, "QR_Sample.png"));
        GenerateBarcodeImage(tempFolder, "Code128_Sample.png", EncodeTypes.Code128, "CODE128TEXT");
        imageFiles.Add(Path.Combine(tempFolder, "Code128_Sample.png"));
        GenerateBarcodeImage(tempFolder, "DataMatrix_Sample.png", EncodeTypes.DataMatrix, "DM123");
        imageFiles.Add(Path.Combine(tempFolder, "DataMatrix_Sample.png"));

        // Prepare CSV report header
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,RecognitionTimeMs,BarcodesFound");

        // Process each generated image
        foreach (string filePath in imageFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            Stopwatch sw = new Stopwatch();
            int foundCount = 0;

            try
            {
                // Initialize reader for all supported barcode types
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    sw.Start(); // Start timing
                    BarCodeResult[] results = reader.ReadBarCodes();
                    sw.Stop(); // Stop timing

                    if (results != null)
                        foundCount = results.Length;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to load image '{Path.GetFileName(filePath)}': {ex.Message}");
                continue;
            }

            // Append result line to CSV
            csvBuilder.AppendLine($"{Path.GetFileName(filePath)},{sw.ElapsedMilliseconds},{foundCount}");
        }

        // Write CSV report to the temporary folder
        string reportPath = Path.Combine(tempFolder, "RecognitionReport.csv");
        File.WriteAllText(reportPath, csvBuilder.ToString());

        Console.WriteLine($"Report generated at: {reportPath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="folder">Destination folder for the image.</param>
    /// <param name="fileName">File name of the generated image.</param>
    /// <param name="encodeType">Barcode symbology to encode.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    static void GenerateBarcodeImage(string folder, string fileName, BaseEncodeType encodeType, string codeText)
    {
        string fullPath = Path.Combine(folder, fileName);
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: set additional parameters if needed
            generator.Save(fullPath, BarCodeImageFormat.Png);
        }
    }
}