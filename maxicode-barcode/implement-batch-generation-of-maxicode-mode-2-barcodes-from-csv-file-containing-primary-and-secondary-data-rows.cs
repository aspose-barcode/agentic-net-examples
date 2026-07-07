// Title: Batch Generation of MaxiCode Mode 2 Barcodes from CSV
// Description: Demonstrates reading a CSV file containing primary and secondary data rows and creating MaxiCode Mode 2 barcodes for each record.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and MaxiCodeStructuredSecondMessage classes to build MaxiCode data structures, and the ComplexBarcodeGenerator to render PNG images. Developers working with shipping, logistics, or any application that requires bulk creation of MaxiCode symbols will find this pattern useful for automating barcode production from data sources such as CSV files.
// Prompt: Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary and secondary data rows.
// Tags: maxicode, batch, csv, barcode generation, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides a console application that reads a CSV file and generates a batch of MaxiCode Mode 2 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads input data, creates MaxiCode objects, and saves barcode images.
    /// </summary>
    static void Main()
    {
        // Input CSV path (relative to executable)
        string csvPath = "input.csv";

        // If the CSV does not exist, create a small sample file
        if (!File.Exists(csvPath))
        {
            var sampleLines = new[]
            {
                "PostalCode,CountryCode,ServiceCategory,SecondMessageType,SecondMessageContent",
                "524032140,056,999,Standard,Test message 1",
                "123456789,840,100,Standard,Hello World",
                "987654321,124,200,Structured,634 ALPHA DRIVE|PITTSBURGH|PA",
                "111222333,036,300,Standard,Sample Text",
                "444555666,124,400,Structured,123 MAIN ST|ANYTOWN|CA"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Ensure output directory exists
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from CSV (including header)
        var lines = File.ReadAllLines(csvPath);
        int barcodesGenerated = 0;

        // Process each data line, limiting to 5 barcodes for the demo
        for (int i = 1; i < lines.Length && barcodesGenerated < 5; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split CSV fields
            string[] parts = line.Split(',');
            if (parts.Length < 5)
                continue; // Skip malformed lines

            // Trim whitespace from each field
            for (int p = 0; p < parts.Length; p++)
                parts[p] = parts[p].Trim();

            // Build MaxiCode codetext for Mode 2 using primary fields
            var maxiCode = new MaxiCodeCodetextMode2
            {
                PostalCode = parts[0],
                CountryCode = int.Parse(parts[1]),
                ServiceCategory = int.Parse(parts[2])
            };

            // Determine and assign the appropriate second message type
            if (parts[3].Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                var stdMsg = new MaxiCodeStandardSecondMessage
                {
                    Message = parts[4]
                };
                maxiCode.SecondMessage = stdMsg;
            }
            else if (parts[3].Equals("Structured", StringComparison.OrdinalIgnoreCase))
            {
                var structMsg = new MaxiCodeStructuredSecondMessage();

                // Structured content parts are separated by '|'
                string[] identifiers = parts[4].Split('|');
                foreach (var id in identifiers)
                {
                    structMsg.Add(id.Trim());
                }

                // Year is optional; not provided in this sample
                maxiCode.SecondMessage = structMsg;
            }
            else
            {
                // Unknown message type – skip this record
                continue;
            }

            // Generate and save the barcode image as PNG
            string outputPath = Path.Combine(outputDir, $"MaxiCode_{barcodesGenerated + 1}.png");
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {barcodesGenerated + 1}: {outputPath}");
            barcodesGenerated++;
        }

        Console.WriteLine("Batch generation completed.");
    }
}