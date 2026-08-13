// Title: Access DataMatrix ReadingQuality and store results
// Description: Demonstrates generating DataMatrix barcodes, reading their ReadingQuality property, and persisting the values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showing how to use BarcodeGenerator, BarCodeReader, and BarCodeResult to evaluate barcode quality. Typical use cases include quality assessment for scanning devices, database logging, and automated testing. Developers often need to extract metrics like ReadingQuality for each barcode and store them for analysis.
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix, readingquality, barcode, generation, recognition, csv, database

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates sample DataMatrix barcodes, reads their <c>ReadingQuality</c> values,
/// and writes the results to a CSV file (placeholder for database storage).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, quality extraction,
    /// and result persistence.
    /// </summary>
    static void Main()
    {
        // Define sample data for barcode generation.
        var samples = new List<(string Text, string FileName)>
        {
            ("Hello", "datamatrix1.png"),
            ("1234567890", "datamatrix2.png")
        };

        // --------------------------------------------------------------------
        // Generate DataMatrix barcode images and save them as PNG files.
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, sample.Text))
            {
                // Save the generated barcode image.
                generator.Save(sample.FileName);
            }
        }

        // Prepare a collection to hold filename and reading quality pairs.
        var results = new List<(string FileName, double ReadingQuality)>();

        // --------------------------------------------------------------------
        // Read each generated image, decode DataMatrix barcodes, and capture
        // the ReadingQuality metric from the BarCodeResult.
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            if (!File.Exists(sample.FileName))
            {
                Console.WriteLine($"File not found: {sample.FileName}");
                continue;
            }

            using (var reader = new BarCodeReader(sample.FileName, DecodeType.DataMatrix))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Verify that the detected barcode is a DataMatrix type.
                    if (result.CodeTypeName.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add((sample.FileName, result.ReadingQuality));
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Persist the collected reading quality data.
        // In a production scenario, replace this CSV write with database insertion.
        // --------------------------------------------------------------------
        const string csvPath = "datamatrix_reading_quality.csv";
        using (var writer = new StreamWriter(csvPath, false))
        {
            writer.WriteLine("FileName,ReadingQuality");
            foreach (var entry in results)
            {
                writer.WriteLine($"{entry.FileName},{entry.ReadingQuality}");
            }
        }

        Console.WriteLine($"Reading quality data saved to '{csvPath}'.");
    }
}