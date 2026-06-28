using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Entry point for the Mailmark barcode generation utility.
/// Reads input data from a CSV file (or uses sample data) and creates PNG barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates reading input, generating barcodes, and saving output files.
    /// </summary>
    /// <param name="args">
    /// Command‑line arguments:
    /// args[0] – optional path to the CSV input file.
    /// args[1] – optional output directory for generated PNG files.
    /// </param>
    static void Main(string[] args)
    {
        // Determine CSV input path and output directory from command‑line arguments
        string csvPath = args.Length > 0 ? args[0] : string.Empty;
        string outputDir = args.Length > 1 ? args[1] : string.Empty;

        // Collection that will hold all Mailmark records to process
        List<MailmarkCodetext> records = new List<MailmarkCodetext>();

        // If a CSV path is supplied and the file exists, read records from it
        if (!string.IsNullOrWhiteSpace(csvPath) && File.Exists(csvPath))
        {
            try
            {
                // Read each line of the CSV file
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Split the line into comma‑separated parts
                    var parts = line.Split(',');

                    // Ensure the line has at least the required number of columns
                    if (parts.Length < 6) continue;

                    // Trim whitespace from each column value
                    for (int i = 0; i < parts.Length; i++) parts[i] = parts[i].Trim();

                    // Parse numeric fields; if any parsing fails, skip the line
                    if (!int.TryParse(parts[0], out int format)) continue;
                    if (!int.TryParse(parts[1], out int versionId)) continue;
                    string classStr = parts[2]; // class is a string value
                    if (!int.TryParse(parts[3], out int supplyChainId)) continue;
                    if (!int.TryParse(parts[4], out int itemId)) continue;
                    string destination = parts[5]; // destination postcode plus DPS

                    // Create a MailmarkCodetext instance from the parsed values
                    var mailmark = new MailmarkCodetext
                    {
                        Format = format,
                        VersionID = versionId,
                        Class = classStr,
                        SupplychainID = supplyChainId,
                        ItemID = itemId,
                        DestinationPostCodePlusDPS = destination
                    };

                    // Add the record to the collection
                    records.Add(mailmark);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                return;
            }
        }
        else
        {
            // No valid CSV supplied – generate a set of 5 sample records
            for (int i = 0; i < 5; i++)
            {
                var sample = new MailmarkCodetext
                {
                    Format = 4,
                    VersionID = 1,
                    Class = "0",
                    SupplychainID = 384224,
                    ItemID = 16563762 + i,
                    DestinationPostCodePlusDPS = "EF61AH8T "
                };
                records.Add(sample);
            }

            // If no output directory was provided, use a temporary folder
            if (string.IsNullOrWhiteSpace(outputDir))
            {
                outputDir = Path.Combine(Path.GetTempPath(), "MailmarkBarcodes");
            }
        }

        // Ensure the output directory exists (creates it if necessary)
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Iterate over each Mailmark record and generate a PNG barcode image
        int index = 1;
        foreach (var mailmark in records)
        {
            // Build the output file name and full path
            string fileName = $"mailmark_{index}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Use ComplexBarcodeGenerator to create and save the barcode image
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for record {index}: {ex.Message}");
            }

            index++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}