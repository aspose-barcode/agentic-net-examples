// Title: DataMatrix Barcode Reading Quality Extraction and CSV Storage
// Description: Demonstrates how to generate DataMatrix barcodes, read them back, retrieve the ReadingQuality metric, and store the values in a CSV file (simulating database storage).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix symbols, BarCodeReader for decoding them, and the BarCodeResult.ReadingQuality property to assess scan quality. Developers working with barcode quality analysis, inventory systems, or automated data capture often need to extract such metrics for logging or database persistence.
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix,readingquality,barcode,recognition,generation,csv,aspnet,aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating DataMatrix barcodes, reading them, extracting ReadingQuality, and saving results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images and output CSV
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample texts to encode into DataMatrix barcodes
        List<string> texts = new List<string>
        {
            "Sample1",
            "DataMatrixTest",
            "1234567890"
        };

        // Generate barcode images and collect their file paths
        List<string> imageFiles = new List<string>();
        foreach (string txt in texts)
        {
            string filePath = Path.Combine(tempFolder, txt + ".png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, txt))
            {
                // Set module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare CSV output (simulating database storage) with header row
        string csvPath = Path.Combine(tempFolder, "ReadingQuality.csv");
        File.WriteAllText(csvPath, "CodeText,ReadingQuality\r\n");

        // Read each generated barcode, extract ReadingQuality, and append to CSV
        foreach (string file in imageFiles)
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
                    string line = $"{result.CodeText},{quality}\r\n";
                    File.AppendAllText(csvPath, line);
                    Console.WriteLine($"Processed {result.CodeText}: Quality={quality}");
                }
            }
        }

        // Note: In a real application, replace the CSV file write with actual database insertion logic.
        Console.WriteLine($"Reading qualities saved to: {csvPath}");
    }
}