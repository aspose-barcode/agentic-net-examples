// Title: Batch QR Code Generation from CSV with Custom Font Sizes
// Description: Demonstrates how to read text and font size values from a CSV file and generate QR code images, applying a unique font size for each barcode's human‑readable text.
// Category-Description: This example is part of the Aspose.BarCode barcode generation collection, showcasing the use of BarcodeGenerator, QR encoding, and CodeTextParameters to customize output. It illustrates typical batch processing scenarios where developers create multiple barcodes from external data sources (e.g., CSV files) and need per‑item visual customization such as font size. Ideal for automating label creation, inventory tagging, or marketing material generation.
// Prompt: Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per row.
// Tags: qr, barcode, generation, csv, font size, aspose.barcode

using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates QR codes in batch by reading text and font size values from a CSV file.
/// Each QR code is saved as a PNG image with the specified font size applied to the
/// human‑readable text displayed below the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles CSV processing, QR code creation,
    /// and image output. No interactive console input is required.
    /// </summary>
    static void Main()
    {
        // Define input CSV path and output folder for generated images
        string csvPath = "input.csv";
        string outputFolder = "Output";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file is missing, create a sample file with example rows
        if (!File.Exists(csvPath))
        {
            string[] sampleLines =
            {
                "Hello World,12",
                "Aspose.BarCode,14",
                "QR Code Sample,10",
                "Custom Text,16",
                "Sample 5,11"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line by comma: first part = text, second part = font size
            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping invalid line {index}: '{line}'");
                continue;
            }

            string codeText = parts[0].Trim();

            // Parse the font size; fall back to 12 if parsing fails
            if (!float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float fontSize))
            {
                Console.WriteLine($"Invalid font size on line {index}, using default 12.");
                fontSize = 12f;
            }

            // Create a QR code generator with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Display the human‑readable text below the QR code
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Apply the custom font size to the code text
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Optional: set a high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Build the output file name (e.g., qr_1.png)
                string outputPath = Path.Combine(outputFolder, $"qr_{index}.png");

                // Save the generated QR code image
                generator.Save(outputPath);
                Console.WriteLine($"Generated QR code {index}: '{codeText}' with font size {fontSize} -> {outputPath}");
            }

            index++;
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}