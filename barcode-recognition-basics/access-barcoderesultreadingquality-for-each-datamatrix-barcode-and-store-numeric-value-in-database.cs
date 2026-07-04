// Title: DataMatrix ReadingQuality extraction and CSV storage
// Description: Demonstrates how to read the ReadingQuality property of each DataMatrix barcode and store the values in a CSV file (as a placeholder for a database).
// Prompt: Access BarCodeResult.ReadingQuality for each DataMatrix barcode and store the numeric value in a database.
// Tags: datamatrix, readingquality, csv, aspose.barcode, barcode, data extraction, database

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates extracting ReadingQuality from DataMatrix barcodes and persisting the values.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a sample DataMatrix barcode, reads its ReadingQuality, and writes the results to a CSV file.
    /// </summary>
    static void Main()
    {
        // Path for the temporary barcode image
        const string imagePath = "datamatrix.png";
        // Path for the CSV file that will store the reading quality values
        const string csvPath = "reading_quality.csv";

        // Generate a sample DataMatrix barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleDataMatrix123"))
        {
            // Save the generated barcode to a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // List to hold reading quality information for each detected barcode
        var qualities = new List<(int Index, string CodeText, double ReadingQuality)>();

        // Read the barcode(s) from the image using a DataMatrix decoder
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            int index = 0;
            foreach (var result in reader.ReadBarCodes())
            {
                // Process only DataMatrix barcodes (additional safety check)
                if (result.CodeTypeName.Equals("DataMatrix", StringComparison.OrdinalIgnoreCase))
                {
                    // Retrieve the ReadingQuality value (double) from the result
                    double readingQuality = result.ReadingQuality;
                    // Store the index, decoded text, and quality in the list
                    qualities.Add((index, result.CodeText, readingQuality));
                    // Output the information to the console for verification
                    Console.WriteLine($"Detected DataMatrix #{index}: CodeText=\"{result.CodeText}\", ReadingQuality={readingQuality}");
                    index++;
                }
            }
        }

        // Store the results in a CSV file (as a stand‑in for a real database)
        using (var writer = new StreamWriter(csvPath))
        {
            // Write CSV header
            writer.WriteLine("Index,CodeText,ReadingQuality");
            // Write each record
            foreach (var item in qualities)
            {
                writer.WriteLine($"{item.Index},\"{item.CodeText}\",{item.ReadingQuality}");
            }
        }

        Console.WriteLine($"Reading quality data saved to \"{csvPath}\".");

        // NOTE:
        // In a production scenario you would insert the values into a database
        // (e.g., using ADO.NET, Entity Framework, Dapper, etc.). The database
        // code is omitted here because the required NuGet packages are not
        // available in the snippet runner environment.
    }
}