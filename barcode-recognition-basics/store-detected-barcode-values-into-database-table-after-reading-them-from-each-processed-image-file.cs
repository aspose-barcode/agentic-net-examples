// Title: Barcode generation, detection, and CSV export example
// Description: Demonstrates creating barcode images, reading them back, and exporting detected values to a CSV file, simulating database storage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to detect them, and typical handling of results for further processing such as database insertion. Developers working with barcode automation often need to generate images, batch‑process files, and store extracted data using ADO.NET or ORM tools.
// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
// Tags: barcode generation, barcode recognition, csv export, aspnet.barcode, encode types, decode types, file processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating sample barcodes, reading them, and saving detection results to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them, and writes detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List of sample barcode specifications (symbology and text)
        var samples = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // Generate barcode images and save them to the temporary folder
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Prepare explicit list of files to process (no directory scanning)
        var filesToProcess = new List<string>();
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            filesToProcess.Add(filePath);
        }

        // List to hold detection results; first entry is CSV header
        var records = new List<string>();
        records.Add("FileName,CodeText,CodeType");

        // Read barcodes from each image and store results
        foreach (string filePath in filesToProcess)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(filePath)}");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        string line = $"{Path.GetFileName(filePath)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)}";
                        records.Add(line);
                        Console.WriteLine($"Detected in {Path.GetFileName(filePath)}: Type={result.CodeTypeName}, Text={result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        // Store results to a CSV file (simulating a database table)
        string outputCsv = Path.Combine(tempFolder, "DetectedBarcodes.csv");
        File.WriteAllLines(outputCsv, records);
        Console.WriteLine($"Barcode detection results saved to: {outputCsv}");

        // Note: In a real application, replace the CSV storage with actual database operations
        // using appropriate ADO.NET or ORM libraries (e.g., SqlConnection, SqlCommand, etc.).
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