// Title: Batch Mailmark Barcode Generation from CSV
// Description: Demonstrates reading a CSV file with Mailmark data and generating a PNG barcode for each row using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcodes such as Mailmark. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to create 4‑state Mailmark symbols, a common requirement for postal automation and tracking solutions. Developers often need to batch‑process data sources (e.g., CSV, databases) to produce barcodes for large volumes of items.
// Prompt: Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data.
// Tags: mailmark, barcode, csv, batch, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Mailmark barcodes in batch from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads Mailmark data from a CSV file,
    /// creates a MailmarkCodetext for each record, and saves the resulting PNG images.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file containing Mailmark fields.
        string csvPath = "mailmark_data.csv";

        // Ensure a sample CSV exists if the file is missing.
        if (!File.Exists(csvPath))
        {
            // Create a small safe sample (5 rows) with required fields.
            // Format is fixed to 4 for 4‑state Mailmark.
            // DestinationPostCodePlusDPS uses the required trailing space.
            string[] sampleLines = new[]
            {
                "VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
                "1,0,384224,16563762,EF61AH8T ",
                "1,1,384224,16563763,EF61AH8T ",
                "1,2,384224,16563764,EF61AH8T ",
                "1,3,384224,16563765,EF61AH8T ",
                "1,0,384224,16563766,EF61AH8T "
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Output directory for generated barcode images.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from the CSV file and skip the header row.
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Process each data row in the CSV.
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            // Split the line into individual fields.
            string[] parts = line.Split(',');

            // Validate the expected number of fields (5).
            if (parts.Length != 5)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}: {line}");
                continue;
            }

            // Parse VersionID.
            if (!int.TryParse(parts[0].Trim(), out int versionId))
            {
                Console.WriteLine($"Invalid VersionID on line {i + 1}");
                continue;
            }

            // Class is a string value (e.g., "0", "1").
            string classValue = parts[1].Trim();

            // Parse SupplychainID.
            if (!int.TryParse(parts[2].Trim(), out int supplyChainId))
            {
                Console.WriteLine($"Invalid SupplychainID on line {i + 1}");
                continue;
            }

            // Parse ItemID.
            if (!int.TryParse(parts[3].Trim(), out int itemId))
            {
                Console.WriteLine($"Invalid ItemID on line {i + 1}");
                continue;
            }

            // DestinationPostCodePlusDPS may contain trailing spaces, which are required.
            string destination = parts[4];

            // Build the Mailmark codetext object with the parsed values.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state Mailmark.
                VersionID = versionId,
                Class = classValue,
                SupplychainID = supplyChainId,
                ItemID = itemId,
                DestinationPostCodePlusDPS = destination
            };

            // Generate the barcode using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                string outPath = Path.Combine(outputDir, $"Mailmark_{itemId}.png");
                generator.Save(outPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for ItemID {itemId} -> {outPath}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}