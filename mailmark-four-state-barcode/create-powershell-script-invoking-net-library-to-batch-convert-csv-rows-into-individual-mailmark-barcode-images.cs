// Title: Batch conversion of CSV rows to Mailmark barcode images
// Description: Demonstrates reading a CSV file and generating individual Mailmark barcode PNGs using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to create barcodes from structured data, a common requirement for logistics and mailing applications. Developers can adapt this pattern for bulk barcode creation from data sources like CSV, databases, or APIs.
// Prompt: Create a PowerShell script invoking the .NET library to batch convert CSV rows into individual Mailmark barcode images.
// Tags: mailmark, barcode generation, csv, batch processing, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a CSV file and generates Mailmark barcode images for each data row.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the CSV and creates PNG barcode files.
    /// </summary>
    static void Main()
    {
        // Input CSV file path (adjust as needed)
        string csvPath = "mailmark_data.csv";

        // Output directory for generated barcode images
        string outputDir = "Barcodes";

        // Verify that the CSV file exists before proceeding
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from the CSV file into memory
        string[] allLines = File.ReadAllLines(csvPath);

        // Check that there is at least one data row (excluding header)
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV file does not contain data rows.");
            return;
        }

        // Assume the first line is a header row describing column names
        // Expected columns: Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS
        // Process up to 5 rows for safety (adjust as needed)
        int maxRows = Math.Min(5, allLines.Length - 1);
        for (int i = 1; i <= maxRows; i++)
        {
            string line = allLines[i];

            // Skip empty or whitespace-only lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the CSV line into individual fields
            string[] parts = line.Split(',');

            // Validate that the required number of columns is present
            if (parts.Length < 6)
            {
                Console.WriteLine($"Skipping line {i + 1}: insufficient columns.");
                continue;
            }

            try
            {
                // Trim whitespace from each field to ensure clean parsing
                for (int p = 0; p < parts.Length; p++)
                    parts[p] = parts[p].Trim();

                // Parse fields into appropriate data types
                int format = int.Parse(parts[0]);               // Format as integer (1,2,4 etc.)
                int versionId = int.Parse(parts[1]);            // VersionID as integer
                string classValue = parts[2];                   // Class as string
                int supplychainId = int.Parse(parts[3]);        // SupplychainID as integer
                int itemId = int.Parse(parts[4]);               // ItemID as integer
                string destinationPostCodePlusDps = parts[5];   // DestinationPostCodePlusDPS as string (keep spaces)

                // Create a MailmarkCodetext object populated with the parsed values
                var mailmark = new MailmarkCodetext
                {
                    Format = format,
                    VersionID = versionId,
                    Class = classValue,
                    SupplychainID = supplychainId,
                    ItemID = itemId,
                    DestinationPostCodePlusDPS = destinationPostCodePlusDps
                };

                // Use ComplexBarcodeGenerator to generate the barcode image
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Construct the output file name (e.g., Mailmark_12345.png)
                    string outputPath = Path.Combine(outputDir, $"Mailmark_{itemId}.png");

                    // Save the generated barcode as a PNG file
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for ItemID {itemId} -> {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the current line
                Console.WriteLine($"Error processing line {i + 1}: {ex.Message}");
            }
        }

        // Indicate that the batch processing has finished
        Console.WriteLine("Batch processing completed.");
    }
}