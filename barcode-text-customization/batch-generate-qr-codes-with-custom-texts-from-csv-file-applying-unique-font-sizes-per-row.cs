// Title: Batch QR Code Generation from CSV with Custom Font Sizes
// Description: Demonstrates reading a CSV file containing text and font size values, then generating QR code images for each row using Aspose.BarCode, applying the specified font size to the code text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to create QR codes in bulk. It covers reading external data sources (CSV), configuring barcode parameters such as XDimension and CodeTextParameters, and saving images in PNG format. Developers often need to automate barcode creation for large datasets while customizing visual aspects like font size, making this pattern useful for inventory, marketing, or data‑encoding scenarios.
// Prompt: Batch generate QR codes with custom texts from a CSV file, applying unique font sizes per row.
// Tags: qr code, batch generation, csv, font size, aspose.barcode, barcode generation, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads a CSV file containing text and font size values,
/// then generates a QR code image for each entry using Aspose.BarCode with the specified font size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the batch QR code generation process.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string baseFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(baseFolder);
        string csvPath = Path.Combine(baseFolder, "data.csv");
        string outputFolder = Path.Combine(baseFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Prepare a small sample CSV file: each line contains Text,FontSize
        var sampleLines = new List<string>
        {
            "Hello World,12",
            "Aspose.BarCode,14",
            "QR Code Sample,10",
            "Batch Generation,16",
            "C# Example,11"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Verify that the CSV file exists before attempting to read it
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found.");
            return;
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
                continue; // Skip empty lines

            // Split the line into text and font size components
            string[] parts = line.Split(',');
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid CSV format at line {i + 1}: {line}");
                continue;
            }

            string codeText = parts[0];
            if (!float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float fontSize))
            {
                Console.WriteLine($"Invalid font size at line {i + 1}: {parts[1]}");
                continue;
            }

            // Generate QR code with the custom font size
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Optional: set the module (pixel) size of the QR code
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure the appearance of the code text displayed below the QR code
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = fontSize;

                // Save the generated QR code as a PNG image
                string outputPath = Path.Combine(outputFolder, $"qr_{i + 1}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated QR code {i + 1}: {outputPath}");
            }
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}