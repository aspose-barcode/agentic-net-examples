// Title: Barcode detection and CSV export example
// Description: Demonstrates reading barcodes from image files, collecting their details, and storing the results in a CSV file (as a placeholder for a database).
// Prompt: Store detected barcode values into a database table after reading them from each processed image file.
// Tags: barcode, detection, csv, aspnet, aspose.barcoderecognition, file-io

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from a set of image files,
/// collects relevant information, and writes the data to a CSV file.
/// The CSV output serves as a stand‑in for persisting the data to a database.
/// </summary>
class Program
{
    /// <summary>
    /// Simple record to hold barcode information extracted from an image.
    /// </summary>
    private class BarcodeRecord
    {
        public string FileName { get; set; }
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public string Region { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Processes each image, extracts barcodes,
    /// and saves the collected data to a CSV file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define the list of image files to be processed.
        // Adjust the file paths as needed for your environment.
        // --------------------------------------------------------------------
        string[] imageFiles = new string[]
        {
            "sample1.png",
            "sample2.png",
            "sample3.png"
        };

        // Collection to store barcode records from all images.
        var records = new List<BarcodeRecord>();

        // --------------------------------------------------------------------
        // Iterate over each image file, read barcodes, and populate the records.
        // --------------------------------------------------------------------
        foreach (var filePath in imageFiles)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Create a barcode reader that attempts to detect all supported types.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the current image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Build a record with the extracted information.
                    var record = new BarcodeRecord
                    {
                        FileName = Path.GetFileName(filePath),
                        CodeType = result.CodeTypeName,
                        CodeText = result.CodeText,
                        // Store region as a simple string representation of the bounding rectangle.
                        Region = $"{result.Region.Rectangle.X},{result.Region.Rectangle.Y},{result.Region.Rectangle.Width},{result.Region.Rectangle.Height}"
                    };

                    // Add the record to the collection and output a console message.
                    records.Add(record);
                    Console.WriteLine($"Detected {record.CodeType} in {record.FileName}: {record.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Write the collected barcode data to a CSV file.
        // This CSV acts as a placeholder for a real database implementation.
        // --------------------------------------------------------------------
        string csvPath = "barcode_results.csv";
        using (var writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header.
            writer.WriteLine("FileName,CodeType,CodeText,Region");

            // Write each record, escaping fields as necessary.
            foreach (var rec in records)
            {
                string fileName = EscapeCsv(rec.FileName);
                string codeType = EscapeCsv(rec.CodeType);
                string codeText = EscapeCsv(rec.CodeText);
                string region = EscapeCsv(rec.Region);
                writer.WriteLine($"{fileName},{codeType},{codeText},{region}");
            }
        }

        Console.WriteLine($"Barcode data saved to {csvPath}");

        // --------------------------------------------------------------------
        // Placeholder for a real database implementation (e.g., SQLite).
        // The necessary NuGet package is not referenced in this snippet.
        // --------------------------------------------------------------------
        // using var connection = new SqliteConnection("Data Source=barcodes.db");
        // connection.Open();
        // var command = connection.CreateCommand();
        // command.CommandText = "CREATE TABLE IF NOT EXISTS Barcodes (FileName TEXT, CodeType TEXT, CodeText TEXT, Region TEXT);";
        // command.ExecuteNonQuery();
        // foreach (var rec in records) { ... insert into table ... }
    }

    /// <summary>
    /// Escapes a CSV field by surrounding it with quotes if it contains commas,
    /// quotes, or line breaks, and doubles any internal quotes.
    /// </summary>
    /// <param name="field">The field value to escape.</param>
    /// <returns>The escaped field suitable for CSV output.</returns>
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}