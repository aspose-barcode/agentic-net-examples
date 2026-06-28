using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Mailmark barcodes from a CSV file or sample data and saves them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads records, constructs MailmarkCodetext objects, and creates barcode images.
    /// </summary>
    static void Main()
    {
        const string csvPath = "mailmark_data.csv";

        // Collection to hold each CSV record as an array of fields
        List<string[]> records = new List<string[]>();

        // Attempt to read data from the CSV file if it exists
        if (File.Exists(csvPath))
        {
            // Read all lines and split each line by commas (simple parsing, no quoted fields)
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Skip empty or whitespace-only lines
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Split the line into individual fields
                var parts = line.Split(',');

                // Ensure the line has at least the expected number of columns
                if (parts.Length >= 6)
                    records.Add(parts);
            }
        }
        else
        {
            // CSV not found – use hard‑coded sample data (5 rows)
            records.Add(new[] { "4", "1", "0", "384224", "16563762", "EF61AH8T " });
            records.Add(new[] { "4", "1", "1", "384224", "16563763", "EF61AH8T " });
            records.Add(new[] { "4", "1", "2", "384224", "16563764", "EF61AH8T " });
            records.Add(new[] { "4", "1", "3", "384224", "16563765", "EF61AH8T " });
            records.Add(new[] { "4", "1", "4", "384224", "16563766", "EF61AH8T " });
        }

        int index = 0; // Counter for generated files

        // Process each record and generate a barcode
        foreach (var fields in records)
        {
            try
            {
                // Parse numeric and string fields from the CSV record
                int format = int.Parse(fields[0].Trim());
                int versionId = int.Parse(fields[1].Trim());
                string classValue = fields[2].Trim(); // Class is a string property
                int supplyChainId = int.Parse(fields[3].Trim());
                int itemId = int.Parse(fields[4].Trim());
                string destinationPostCodePlusDps = fields[5].Trim();

                // Populate a MailmarkCodetext object with the parsed values
                var mailmark = new MailmarkCodetext
                {
                    Format = format,
                    VersionID = versionId,
                    Class = classValue,
                    SupplychainID = supplyChainId,
                    ItemID = itemId,
                    DestinationPostCodePlusDPS = destinationPostCodePlusDps
                };

                // Generate the barcode image using Aspose ComplexBarcodeGenerator
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Construct a unique filename for each barcode
                    string outputFile = $"Mailmark_{itemId}_{index}.png";

                    // Save the barcode as a PNG file
                    generator.Save(outputFile, BarCodeImageFormat.Png);

                    // Inform the user that the file was created
                    Console.WriteLine($"Generated: {outputFile}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the current record
                Console.WriteLine($"Error processing record #{index}: {ex.Message}");
            }

            index++; // Increment the file counter
        }
    }
}