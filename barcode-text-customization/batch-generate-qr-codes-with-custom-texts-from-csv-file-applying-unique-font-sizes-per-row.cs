// Title: Batch QR Code Generation from CSV with Custom Font Sizes
// Description: Demonstrates reading a CSV file and generating QR code images, each using a distinct text and font size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and QRErrorLevel to create QR codes in bulk. Typical use cases include batch processing of product identifiers, marketing materials, or any scenario where each barcode requires custom human‑readable text styling. Developers often need to read data sources, apply per‑row formatting, and output images in common formats like PNG.
// Prompt: Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per row.
// Tags: qr,barcode,generation,csv,fontsize,aspose.barcode,aspose.drawing

using System;
using System.IO;
using System.Globalization;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of QR codes from a CSV file, applying custom font sizes per row.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the CSV, creates QR codes with specified text and font size, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the CSV file. Each line should contain: Text,FontSize
        string csvPath = "input.csv";

        // If the CSV does not exist, create a small sample file (5 rows) to keep the example runnable.
        if (!File.Exists(csvPath))
        {
            using (var writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("Hello World,12");
                writer.WriteLine("Aspose.BarCode,14");
                writer.WriteLine("QR Code Sample,16");
                writer.WriteLine("Sample Text,10");
                writer.WriteLine("Final Row,18");
            }
        }

        // Read all non‑empty lines from the CSV.
        string[] lines = File.ReadAllLines(csvPath);
        int rowIndex = 0;

        foreach (string rawLine in lines)
        {
            // Skip blank lines.
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            rowIndex++;

            // Split by comma – expected format: Text,FontSize
            string[] parts = rawLine.Split(new[] { ',' }, 2);
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping line {rowIndex}: insufficient columns.");
                continue;
            }

            string text = parts[0].Trim();
            string fontSizeStr = parts[1].Trim();

            // Parse font size; fallback to a default size if parsing fails.
            if (!float.TryParse(fontSizeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out float fontSize))
            {
                Console.WriteLine($"Invalid font size on line {rowIndex}, using default 12pt.");
                fontSize = 12f;
            }

            // Prepare output file name – ensure a valid file name per row.
            string safeText = string.Concat(Array.FindAll(text.ToCharArray(), c => !Path.GetInvalidFileNameChars().Contains(c)));
            string outputFile = $"qr_{rowIndex}_{safeText}.png";

            // Generate QR code with the specified text and font size.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set human‑readable text font family and size.
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Optional: set QR error correction level (default is LevelL).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the barcode image.
                generator.Save(outputFile);
            }

            Console.WriteLine($"Generated QR code for row {rowIndex}: \"{text}\" with font size {fontSize}pt -> {outputFile}");
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}