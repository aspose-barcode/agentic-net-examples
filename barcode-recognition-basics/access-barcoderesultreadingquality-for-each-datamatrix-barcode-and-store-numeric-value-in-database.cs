// Title: Extract DataMatrix Barcode Reading Quality and Save to CSV
// Description: Generates DataMatrix barcodes, reads each barcode, extracts the ReadingQuality metric, and stores the results in a CSV file (illustrating where database insertion would occur).
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition for DataMatrix symbology. It uses BarcodeGenerator to create barcodes and BarCodeReader to decode them, accessing BarCodeResult.ReadingQuality. Developers working with barcode quality assessment, inventory systems, or automated scanning solutions often need to capture quality metrics for analysis or storage.
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix, readingquality, barcode, generation, recognition, csv, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate DataMatrix barcodes, read them,
/// retrieve the ReadingQuality value, and persist the data to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads each one,
    /// extracts the reading quality, and writes the information to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data texts for DataMatrix barcodes
        List<string> sampleTexts = new List<string>
        {
            "Sample001",
            "DataMatrixTest",
            "1234567890"
        };

        // Generate DataMatrix barcode images and collect file paths
        List<string> barcodeFiles = new List<string>();
        foreach (string text in sampleTexts)
        {
            string filePath = Path.Combine(tempFolder, $"DM_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Optional: set XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare output CSV file to store reading quality values
        string csvPath = Path.Combine(tempFolder, "reading_quality.csv");
        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            writer.WriteLine("FileName,CodeText,ReadingQuality");

            // Read each barcode and extract ReadingQuality
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.DataMatrix))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        double quality = result.ReadingQuality;
                        string line = $"{Path.GetFileName(file)},{result.CodeText},{quality}";
                        writer.WriteLine(line);
                        Console.WriteLine(line);
                    }
                }
            }
        }

        // In a real scenario, replace the CSV storage with database insertion logic, e.g.:
        // using (SqlConnection conn = new SqlConnection(connectionString))
        // {
        //     conn.Open();
        //     foreach (var record in records)
        //     {
        //         using (SqlCommand cmd = new SqlCommand("INSERT INTO BarcodeQuality (FileName, CodeText, ReadingQuality) VALUES (@file, @text, @quality)", conn))
        //         {
        //             cmd.Parameters.AddWithValue("@file", record.FileName);
        //             cmd.Parameters.AddWithValue("@text", record.CodeText);
        //             cmd.Parameters.AddWithValue("@quality", record.ReadingQuality);
        //             cmd.ExecuteNonQuery();
        //         }
        //     }
        // }

        Console.WriteLine($"Reading quality data saved to: {csvPath}");
    }
}