// Title: Generate, Recognize, and Export Barcode Data to CSV
// Description: Demonstrates creating sample barcode images, reading them back, and writing detected barcode information to a CSV file for later database insertion.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for detecting them. Typical scenarios include batch processing of images, extracting barcode data, and persisting results to storage such as databases or CSV files. Developers often need to automate barcode handling pipelines, and this sample provides a clear pattern for those operations.
/// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
/// Tags: barcode generation, barcode recognition, csv export, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates barcode images, reads them, and writes detection results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them, and exports detection data.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated image file paths
        List<string> imageFiles = new List<string>();

        // Sample barcode data (type, text, file name)
        var samples = new (BaseEncodeType EncodeType, string CodeText, string FileName)[]
        {
            (EncodeTypes.Code128, "Sample123", "Code128.png"),
            (EncodeTypes.QR, "https://example.com", "QR.png"),
            (EncodeTypes.DataMatrix, "DM12345", "DataMatrix.png")
        };

        // Generate barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV output (as a substitute for a database)
        string csvPath = Path.Combine(tempFolder, "Barcodes.csv");
        using (var writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            writer.WriteLine("FileName,CodeText,CodeTypeName");

            // Use a decoder that supports all barcode types
            BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;

            // Process each generated image
            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                try
                {
                    // Read barcodes from the image
                    using (var reader = new BarCodeReader(imagePath, decodeAll))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                            continue;
                        }

                        // Write each detected barcode to the CSV file
                        foreach (var result in results)
                        {
                            string line = $"{Path.GetFileName(imagePath)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)}";
                            writer.WriteLine(line);
                            Console.WriteLine($"Detected: {result.CodeText} ({result.CodeTypeName}) in {Path.GetFileName(imagePath)}");
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(imagePath)}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Barcode data saved to: {csvPath}");
        // Note: In a real application, you would insert the data into a database table here.
    }

    // Helper to escape CSV fields containing commas, quotes, or newlines
    static string EscapeCsv(string field)
    {
        if (field == null) return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}