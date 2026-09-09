// Title: Batch QR Code Generation from CSV with Custom Font Sizes
// Description: Demonstrates how to read a CSV file, generate a QR code for each row, and apply a specific font size to the code text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes.QR, and CodeTextParameters to create QR codes. Typical use cases include bulk barcode creation from data sources such as CSV files, where each barcode may require individual styling like custom font sizes. Developers often need to automate barcode production for inventory, marketing, or authentication purposes.
// Prompt: Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per row.
// Tags: qr, barcode, csv, batch, font-size, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates QR code images in batch based on rows from a CSV file,
/// applying a custom font size for each barcode's text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary working directory,
    /// writes a sample CSV, reads each line, and generates a QR code image
    /// with the specified text and font size.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary working directory for the demo files.
        string workDir = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Prepare a sample CSV file containing the text and desired font size for each QR code.
        string csvPath = Path.Combine(workDir, "data.csv");
        var sampleLines = new List<string>
        {
            "Hello World,12",
            "Aspose,14",
            "Sample Text,10",
            "1234567890,16",
            "QR Code Test,18"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Create an output folder where the generated QR code images will be saved.
        string outputDir = Path.Combine(workDir, "output");
        Directory.CreateDirectory(outputDir);

        // Read all rows from the CSV file.
        string[] rows = File.ReadAllLines(csvPath);
        int count = 0;

        // Process each non‑empty row and generate a QR code image.
        foreach (string row in rows)
        {
            if (string.IsNullOrWhiteSpace(row))
                continue; // Skip empty lines.

            // Split the row into text and font size components.
            string[] parts = row.Split(',');
            if (parts.Length < 2)
                continue; // Skip malformed lines.

            string text = parts[0];
            if (!float.TryParse(parts[1], out float fontSize))
                fontSize = 12f; // Fallback to default size if parsing fails.

            // Initialize the barcode generator for QR encoding with the provided text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Enable manual font mode and set the custom font size.
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Optionally adjust the module (pixel) size of the QR code.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Build the output file path and save the QR code as a PNG image.
                string outPath = Path.Combine(outputDir, $"qr_{count + 1}.png");
                generator.Save(outPath, BarCodeImageFormat.Png);
            }

            count++;
        }

        // Inform the user about the number of generated images and their location.
        Console.WriteLine($"Generated {count} QR code images in: {outputDir}");
    }
}