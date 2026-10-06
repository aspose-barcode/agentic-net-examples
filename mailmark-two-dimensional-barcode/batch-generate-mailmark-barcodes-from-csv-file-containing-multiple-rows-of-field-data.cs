// Title: Batch generate Mailmark barcodes from CSV data
// Description: Demonstrates reading a CSV file containing Mailmark fields and generating a PNG barcode for each row using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use ComplexBarcodeGenerator and MailmarkCodetext to create multiple Mailmark barcodes programmatically. Typical use cases include bulk printing of postal barcodes, automated document workflows, and integration with logistics systems. Developers often need to read structured data (e.g., CSV) and produce barcodes in common image formats.
// Prompt: Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data.
// Tags: mailmark, barcode, batch, csv, generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Mailmark barcodes in bulk from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads CSV data, creates output folder, and generates a PNG barcode per row.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV path (argument or temporary sample)
        string csvPath = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "mailmark_input.csv");

        // If CSV does not exist, create a sample with a few rows
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                "Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS",
                "4,1,0,384224,16563762,EF61AH8T ",
                "4,1,1,384225,16563763,EF61AH8T ",
                "4,1,2,384226,16563764,EF61AH8T "
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at: {csvPath}");
        }

        // Create a unique output folder for generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "MailmarkBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Read all lines from CSV
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV contains no data rows.");
            return;
        }

        // Parse header to get column indices
        string[] header = allLines[0].Split(',');
        int idxFormat = Array.IndexOf(header, "Format");
        int idxVersionID = Array.IndexOf(header, "VersionID");
        int idxClass = Array.IndexOf(header, "Class");
        int idxSupplychainID = Array.IndexOf(header, "SupplychainID");
        int idxItemID = Array.IndexOf(header, "ItemID");
        int idxDestination = Array.IndexOf(header, "DestinationPostCodePlusDPS");

        // Validate required columns are present
        if (idxFormat < 0 || idxVersionID < 0 || idxClass < 0 || idxSupplychainID < 0 ||
            idxItemID < 0 || idxDestination < 0)
        {
            Console.WriteLine("CSV header missing required columns.");
            return;
        }

        int successCount = 0;

        // Process each data row
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            string[] parts = line.Split(',');
            try
            {
                // Parse fields from CSV
                int format = int.Parse(parts[idxFormat].Trim());
                if (format != 4)
                {
                    Console.WriteLine($"Row {i}: Unsupported Format {format}, skipping.");
                    continue;
                }

                int versionId = int.Parse(parts[idxVersionID].Trim());
                string classValue = parts[idxClass].Trim();
                int supplyChainId = int.Parse(parts[idxSupplychainID].Trim());
                int itemId = int.Parse(parts[idxItemID].Trim());
                string destination = parts[idxDestination];
                // Preserve exact spacing (including trailing space) as required
                destination = destination.Length < 9 ? destination.PadRight(9) : destination;

                // Build Mailmark codetext object
                var mailmark = new MailmarkCodetext
                {
                    Format = format,
                    VersionID = versionId,
                    Class = classValue,
                    SupplychainID = supplyChainId,
                    ItemID = itemId,
                    DestinationPostCodePlusDPS = destination
                };

                // Generate barcode image
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 4;
                    string outPath = Path.Combine(outputFolder, $"Mailmark_{i}.png");
                    generator.Save(outPath, BarCodeImageFormat.Png);
                }

                successCount++;
                Console.WriteLine($"Row {i}: Barcode generated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Row {i}: Error - {ex.Message}");
            }
        }

        Console.WriteLine($"Generation completed. {successCount} barcodes created.");
    }
}