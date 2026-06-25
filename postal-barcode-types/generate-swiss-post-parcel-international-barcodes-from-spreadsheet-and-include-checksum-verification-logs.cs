using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and recognizing Swiss Post Parcel barcodes
/// from a list of parcel codes read from a CSV file (or sample data).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads parcel codes, generates barcodes, saves them, and verifies each barcode.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file containing parcel codes (first column)
        string csvPath = "input.csv";

        // Load parcel codes from CSV or fall back to sample data if the file is missing
        List<string> parcelCodes = new List<string>();
        if (File.Exists(csvPath))
        {
            try
            {
                // Read all lines from the CSV file
                foreach (var line in File.ReadAllLines(csvPath))
                {
                    // Skip empty or whitespace‑only lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Assume CSV format: CodeText,OtherColumns...
                    var parts = line.Split(',');
                    if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
                        parcelCodes.Add(parts[0].Trim()); // Add the first column as the parcel code
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while reading the CSV
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }

        // If no codes were loaded, use a predefined set of sample data (max 5 items)
        if (parcelCodes.Count == 0)
        {
            parcelCodes.AddRange(new[]
            {
                "1234567890123",
                "9876543210987",
                "5555555555555",
                "1111111111111",
                "2222222222222"
            });
        }

        // Process at most 5 items for safety
        int maxItems = Math.Min(5, parcelCodes.Count);
        string outputDir = "Barcodes";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Iterate over each parcel code to generate and verify its barcode
        for (int i = 0; i < maxItems; i++)
        {
            string codeText = parcelCodes[i];
            string outputPath = Path.Combine(outputDir, $"SwissPost_{i + 1}.png");

            // ---------- Barcode Generation ----------
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                // Enable checksum generation and display it in the human‑readable text
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode image to disk
                generator.Save(outputPath);
                Console.WriteLine($"[Generation] Saved barcode #{i + 1} to '{outputPath}' (CodeText: {codeText})");
            }

            // ---------- Barcode Recognition ----------
            using (var reader = new BarCodeReader(outputPath, DecodeType.SwissPostParcel))
            {
                // Enable checksum validation during recognition
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all barcodes found in the image (should be one)
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"[Recognition] Barcode #{i + 1}");
                    Console.WriteLine($"  Detected CodeText : {result.CodeText}");
                    Console.WriteLine($"  Expected CodeText : {codeText}");
                    Console.WriteLine($"  Checksum          : {result.Extended.OneD.CheckSum}");
                    Console.WriteLine($"  Match Original    : {result.CodeText == codeText}");
                }
            }
        }

        // Indicate that processing has finished
        Console.WriteLine("Processing completed.");
    }
}