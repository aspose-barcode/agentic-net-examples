using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates reading text and font size values (from a CSV file or sample data)
/// and generating QR code images with human‑readable text displayed below each code.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Reads input data, configures the barcode generator,
    /// and saves QR code images with custom font sizes.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file. Expected format per line: Text,FontSize
        const string csvPath = "input.csv";

        // Collection to hold each row's text and associated font size
        List<(string Text, float FontSize)> rows = new List<(string, float)>();

        // Check if the CSV file exists before attempting to read it
        if (File.Exists(csvPath))
        {
            // Read all lines from the CSV file
            foreach (string line in File.ReadAllLines(csvPath))
            {
                // Skip empty or whitespace‑only lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split the line into text and font size components
                string[] parts = line.Split(',');

                // Ensure the line contains at least two parts (text and size)
                if (parts.Length < 2)
                    continue;

                string text = parts[0];

                // Try to parse the font size; fall back to 12 if parsing fails
                if (!float.TryParse(parts[1], out float size))
                    size = 12f;

                // Add the parsed tuple to the collection
                rows.Add((text, size));
            }
        }
        else
        {
            // CSV not found – use a predefined set of sample data (5 rows)
            rows.Add(("Sample One", 10f));
            rows.Add(("Sample Two", 12f));
            rows.Add(("Sample Three", 14f));
            rows.Add(("Sample Four", 16f));
            rows.Add(("Sample Five", 18f));
        }

        // Iterate over each row and generate a corresponding QR code image
        int index = 1;
        foreach (var (text, fontSize) in rows)
        {
            // Construct the output file name (e.g., qr_1.png, qr_2.png, ...)
            string outputPath = $"qr_{index}.png";

            // Initialize the barcode generator for QR codes with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Position the human‑readable text below the QR code
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Set the font family and size for the human‑readable text
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Optional: define the QR error correction level (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image to the specified path
                generator.Save(outputPath);
            }

            // Inform the user that the QR code has been generated
            Console.WriteLine($"Generated QR code: {outputPath}");
            index++;
        }
    }
}