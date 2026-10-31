// Title: Barcode generation, recognition, and quality logging example
// Description: This example creates several barcode images, reads them back using Aspose.BarCode, and records quality metrics to a log file.
// Category-Description: Demonstrates core Aspose.BarCode operations such as barcode generation (BarcodeGenerator), multi‑symbology recognition (BarCodeReader), and quality assessment (QualitySettings). Ideal for developers building batch processing, monitoring services, or automated quality checks where reading accuracy and metrics are required. Use cases include inventory systems, document scanning pipelines, and production line verification.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode symbology, generation, recognition, quality, logging, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that generates barcode images, reads them, and logs quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with high‑quality settings, and writes a quality log.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data (symbology, text, output file name)
        var samples = new (BaseEncodeType EncodeType, string CodeText, string FileName)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        var createdFiles = new System.Collections.Generic.List<string>();

        // Generate barcode images and store their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.EncodeType))
            {
                generator.SetCodeText(sample.CodeText, Encoding.UTF8);
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            createdFiles.Add(filePath);
        }

        // Prepare a log file for quality metrics
        string logPath = Path.Combine(tempFolder, "QualityLog.txt");
        File.WriteAllText(logPath, $"Barcode quality log - {DateTime.Now}{Environment.NewLine}");

        // Read each generated barcode image and log its quality metrics
        foreach (string file in createdFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply a high‑quality preset for more accurate reading
                    reader.QualitySettings = QualitySettings.HighQuality;

                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                        File.AppendAllText(logPath, $"No barcode detected in {Path.GetFileName(file)}{Environment.NewLine}");
                        continue;
                    }

                    // Log each detected barcode's details and reading quality
                    foreach (var result in results)
                    {
                        string logEntry = $"File: {Path.GetFileName(file)} | " +
                                          $"CodeText: {result.CodeText} | " +
                                          $"Symbology: {result.CodeTypeName} | " +
                                          $"ReadingQuality: {result.ReadingQuality:F2}";
                        Console.WriteLine(logEntry);
                        File.AppendAllText(logPath, logEntry + Environment.NewLine);
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle image loading failures gracefully
                Console.WriteLine($"Failed to load image {Path.GetFileName(file)}: {ex.Message}");
                File.AppendAllText(logPath, $"Failed to load image {Path.GetFileName(file)}: {ex.Message}{Environment.NewLine}");
            }
        }

        // Indicate completion and location of the log file
        Console.WriteLine($"Processing complete. Log saved to: {logPath}");
    }
}