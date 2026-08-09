// Title: Batch decode MaxiCode PNG images and generate CSV report
// Description: Demonstrates how to read multiple MaxiCode barcodes from PNG files in a folder and export the decoded text and type to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the BarCodeReader with DecodeType.MaxiCode, CSV report generation, and optional sample image creation using ComplexBarcodeGenerator. Developers working with bulk barcode processing, reporting, or logistics applications can use this pattern to automate data extraction from MaxiCode symbols.
// Prompt: Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report.
// Tags: maxicode, barcode, decoding, csv, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Provides a console application that decodes all MaxiCode PNG images in a specified directory
/// and writes the results to a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans the input folder, generates sample images if needed, decodes each MaxiCode,
    /// and writes a CSV file containing file name, decoded text, and barcode type.
    /// </summary>
    static void Main()
    {
        // Define input and output paths
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeImages");
        string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeReport.csv");

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Generate a few sample MaxiCode PNG files if the folder is empty
        string[] sampleFiles = Directory.GetFiles(inputFolder, "*.png");
        if (sampleFiles.Length == 0)
        {
            GenerateSampleMaxiCodeImages(inputFolder);
        }

        // Prepare CSV header
        var csvLines = new List<string> { "FileName,CodeText,CodeType" };

        // Process each PNG file in the folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.png"))
        {
            if (!File.Exists(filePath))
            {
                // Skip missing files gracefully
                continue;
            }

            // Decode using MaxiCode decode type
            using (var reader = new BarCodeReader(filePath, DecodeType.MaxiCode))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Build CSV line with proper escaping
                    string line = $"{Path.GetFileName(filePath)},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)}";
                    csvLines.Add(line);
                }
            }
        }

        // Write all lines to the CSV report
        File.WriteAllLines(reportPath, csvLines);
    }

    // Generates a few sample MaxiCode images (Mode2) for demonstration
    private static void GenerateSampleMaxiCodeImages(string folder)
    {
        // Sample data for three images
        var samples = new[]
        {
            new { FileName = "sample1.png", PostalCode = "524032140", CountryCode = 56, ServiceCategory = 999, Message = "Hello World" },
            new { FileName = "sample2.png", PostalCode = "524032141", CountryCode = 56, ServiceCategory = 100, Message = "Aspose.BarCode" },
            new { FileName = "sample3.png", PostalCode = "524032142", CountryCode = 56, ServiceCategory = 200, Message = "MaxiCode Test" }
        };

        foreach (var s in samples)
        {
            // Create MaxiCode codetext (Mode2)
            var maxiCode = new MaxiCodeCodetextMode2
            {
                PostalCode = s.PostalCode,
                CountryCode = s.CountryCode,
                ServiceCategory = s.ServiceCategory
            };

            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = s.Message
            };
            maxiCode.SecondMessage = secondMessage;

            string imagePath = Path.Combine(folder, s.FileName);

            // Generate and save the barcode image
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }
    }

    // Escapes CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}