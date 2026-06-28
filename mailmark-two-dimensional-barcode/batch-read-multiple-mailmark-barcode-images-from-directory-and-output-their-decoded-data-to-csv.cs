using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Reads Mailmark barcodes from image files in a directory and writes the extracted data to a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – input directory containing barcode images (default: "Barcodes").
    /// args[1] – output CSV file path (default: "output.csv").
    /// </param>
    static void Main(string[] args)
    {
        // Determine input directory (first argument or default)
        string inputDirectory = args.Length > 0 ? args[0] : "Barcodes";

        // Determine output CSV file path (second argument or default)
        string outputCsvPath = args.Length > 1 ? args[1] : "output.csv";

        // Verify that the input directory exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        // Prepare CSV lines collection and add header row
        var csvLines = new List<string>();
        csvLines.Add("FileName,Format,VersionID,Class,SupplychainID,ItemID,DestinationPostCodePlusDPS,RawCodeText");

        // Define supported image file extensions
        string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff" };

        // Enumerate all files in the input directory
        var files = Directory.GetFiles(inputDirectory);
        foreach (var filePath in files)
        {
            // Skip files that do not have a supported image extension
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Ensure the file actually exists (defensive check)
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            // Open a barcode reader for Mailmark type on the current image file
            using (var reader = new BarCodeReader(filePath, DecodeType.Mailmark))
            {
                // Read all barcodes present in the image
                var results = reader.ReadBarCodes();

                // If no barcodes were found, write an empty CSV entry for the file
                if (results == null || results.Length == 0)
                {
                    csvLines.Add($"{Path.GetFileName(filePath)},,,,,,,");
                    continue;
                }

                // Process each detected barcode
                foreach (var result in results)
                {
                    // Attempt to decode the complex Mailmark codetext into its components
                    MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);

                    // Extract individual fields, handling possible null values
                    string format = mailmark?.Format.ToString() ?? "";
                    string versionId = mailmark?.VersionID.ToString() ?? "";
                    string classValue = mailmark?.Class ?? "";
                    string supplychainId = mailmark?.SupplychainID.ToString() ?? "";
                    string itemId = mailmark?.ItemID.ToString() ?? "";
                    string destination = mailmark?.DestinationPostCodePlusDPS ?? "";
                    string rawCode = result.CodeText ?? "";

                    // Build a CSV line with proper escaping for each field
                    string line = $"{Path.GetFileName(filePath)}," +
                                  $"{EscapeCsv(format)}," +
                                  $"{EscapeCsv(versionId)}," +
                                  $"{EscapeCsv(classValue)}," +
                                  $"{EscapeCsv(supplychainId)}," +
                                  $"{EscapeCsv(itemId)}," +
                                  $"{EscapeCsv(destination)}," +
                                  $"{EscapeCsv(rawCode)}";

                    csvLines.Add(line);
                }
            }
        }

        // Attempt to write all collected CSV lines to the output file
        try
        {
            File.WriteAllLines(outputCsvPath, csvLines);
            Console.WriteLine($"CSV output written to: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write CSV file: {ex.Message}");
        }
    }

    /// <summary>
    /// Escapes a CSV field by surrounding it with quotes if it contains commas, quotes, or newlines.
    /// Internal quotes are doubled per CSV specification.
    /// </summary>
    /// <param name="field">The field value to escape.</param>
    /// <returns>The escaped field string.</returns>
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";

        // Check for characters that require quoting
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            // Double any existing quotes and wrap the field in quotes
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        // No escaping needed
        return field;
    }
}