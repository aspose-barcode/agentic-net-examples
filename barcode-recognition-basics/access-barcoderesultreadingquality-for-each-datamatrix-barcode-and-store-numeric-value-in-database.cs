// Title: Read DataMatrix barcode quality and export to CSV (simulated database)
// Description: Demonstrates generating DataMatrix barcodes, reading each barcode, extracting the ReadingQuality metric, and persisting the values for later database insertion.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the BarCodeResult.ReadingQuality property to assess scan quality. Developers working with barcode quality analysis, inventory systems, or automated data capture often need to evaluate and store quality metrics for each scanned symbol.
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix, readingquality, barcode, quality, csv, database, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample DataMatrix barcodes, reads them back to obtain the ReadingQuality value,
/// and writes the results to a CSV file (as a stand‑in for database storage).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, quality extraction, and CSV export.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample texts to encode as DataMatrix barcodes
        List<string> sampleTexts = new List<string> { "ABC123", "XYZ789", "DATA456" };
        List<string> imagePaths = new List<string>();

        // --------------------------------------------------------------------
        // Generate DataMatrix barcode images and store their file paths
        // --------------------------------------------------------------------
        foreach (string text in sampleTexts)
        {
            string imagePath = Path.Combine(tempFolder, text + ".png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Save the barcode image directly to file in PNG format
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(imagePath);
        }

        // --------------------------------------------------------------------
        // Prepare output CSV file (simulating a database table)
        // --------------------------------------------------------------------
        string outputCsv = Path.Combine(tempFolder, "ReadingQuality.csv");
        using (StreamWriter writer = new StreamWriter(outputCsv, false))
        {
            writer.WriteLine("FileName,ReadingQuality");

            // ----------------------------------------------------------------
            // Read each barcode image, extract ReadingQuality, and write to CSV
            // ----------------------------------------------------------------
            foreach (string filePath in imagePaths)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Decode only DataMatrix barcodes to improve performance
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.DataMatrix))
                {
                    // Apply high‑performance quality settings (optional)
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Verify that the decoded symbol is a DataMatrix barcode
                        if (string.Equals(result.CodeTypeName, "DataMatrix", StringComparison.OrdinalIgnoreCase))
                        {
                            double quality = result.ReadingQuality; // Value ranges from 0 to 100
                            string fileName = Path.GetFileName(filePath);
                            writer.WriteLine($"{fileName},{quality}");
                            Console.WriteLine($"Processed {fileName}: ReadingQuality = {quality}");
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Note: Replace the CSV write with actual database insertion logic in production.
        // --------------------------------------------------------------------
        // Example (pseudo‑code):
        // using (var connection = new SqlConnection(connectionString))
        // {
        //     connection.Open();
        //     foreach (var record in records)
        //         connection.Execute("INSERT INTO BarcodeQuality (FileName, Quality) VALUES (@FileName, @Quality)", record);
        // }

        Console.WriteLine($"Reading quality data saved to: {outputCsv}");
    }
}