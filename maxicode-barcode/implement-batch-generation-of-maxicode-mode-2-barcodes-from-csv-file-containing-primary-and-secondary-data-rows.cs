using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of MaxiCode barcodes from CSV or sample data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads input data, generates MaxiCode barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file containing barcode data.
        string csvPath = "maxicode_input.csv";

        // List to hold rows of data read from the CSV or fallback sample data.
        List<string[]> rows = new List<string[]>();

        // Check if the CSV file exists before attempting to read it.
        if (File.Exists(csvPath))
        {
            // Read each line of the CSV file. This simple parser splits on commas and trims whitespace.
            foreach (var line in File.ReadAllLines(csvPath))
            {
                // Skip empty or whitespace-only lines.
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Split the line into parts based on commas.
                var parts = line.Split(',');

                // Expect at least four columns: PostalCode, CountryCode, ServiceCategory, Message.
                if (parts.Length >= 4)
                {
                    // Trim each part and add as a string array to the rows collection.
                    rows.Add(new[] { parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), parts[3].Trim() });
                }
            }
        }
        else
        {
            // CSV not found – use hard‑coded sample data as a fallback.
            rows.Add(new[] { "524032140", "056", "999", "Sample message 1" });
            rows.Add(new[] { "524032141", "056", "998", "Sample message 2" });
            rows.Add(new[] { "524032142", "056", "997", "Sample message 3" });
        }

        // Ensure the output directory exists; create it if necessary.
        string outputDir = "MaxiCodeOutputs";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each row and generate a corresponding MaxiCode barcode.
        int index = 1;
        foreach (var fields in rows)
        {
            try
            {
                // Build the codetext for MaxiCode Mode 2 using the first three fields.
                var maxiCode = new MaxiCodeCodetextMode2
                {
                    PostalCode = fields[0],
                    CountryCode = int.Parse(fields[1]),
                    ServiceCategory = int.Parse(fields[2])
                };

                // Attach the standard second message (fourth field) to the MaxiCode.
                var secondMessage = new MaxiCodeStandardSecondMessage
                {
                    Message = fields[3]
                };
                maxiCode.SecondMessage = secondMessage;

                // Generate the barcode using the ComplexBarcodeGenerator.
                using (var generator = new ComplexBarcodeGenerator(maxiCode))
                {
                    // Set a high resolution for the output image (optional).
                    generator.Parameters.Resolution = 300f;

                    // Construct the output file path and save the barcode as a PNG.
                    string outputPath = Path.Combine(outputDir, $"maxicode_{index}.png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);

                    // Inform the user that the barcode was generated successfully.
                    Console.WriteLine($"Generated barcode {index}: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing the current row.
                Console.WriteLine($"Error processing row {index}: {ex.Message}");
            }

            // Move to the next index for naming the subsequent output file.
            index++;
        }

        // Indicate that the batch generation process has finished.
        Console.WriteLine("Batch generation completed.");
    }
}