// Title: Generate barcode images from CSV with padding and rotation
// Description: This example reads a CSV file containing barcode data, padding, and rotation values, then creates PNG images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode generation API for batch processing. Shows how to use BarcodeGenerator, set EncodeTypes, configure padding via Parameters.Barcode.Padding, apply rotation with Parameters.RotationAngle, and save images. Useful for developers automating barcode creation from data sources such as CSV files.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode symbology, generation, png, padding, rotation, csv, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads a CSV list of barcode specifications and generates PNG images with custom padding and rotation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes the CSV file and creates barcode images.
    /// </summary>
    static void Main()
    {
        // Define the path to the CSV file that holds barcode specifications.
        string csvPath = "barcodes.csv";

        // If the CSV file does not exist, create a sample file with example data.
        if (!File.Exists(csvPath))
        {
            string[] sampleLines =
            {
                // Format: CodeText,OutputFileName,RotationAngle,PaddingPoints
                "1234567890,code1.png,0,10",
                "ABCDEF,code2.png,90,15",
                "HelloWorld,code3.png,180,20"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at '{csvPath}'.");
        }

        // Read all lines from the CSV file, ignoring empty entries.
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip blank lines.

            // Expected columns: CodeText, OutputFileName, RotationAngle, PaddingPoints
            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Console.WriteLine($"Skipping malformed line: {line}");
                continue; // Not enough columns; move to the next line.
            }

            // Extract and trim individual values.
            string codeText = parts[0].Trim();
            string outputFile = parts[1].Trim();

            // Parse rotation angle; if invalid, report and skip.
            if (!float.TryParse(parts[2].Trim(), out float rotation))
            {
                Console.WriteLine($"Invalid rotation value on line: {line}");
                continue;
            }

            // Parse padding value; if invalid, report and skip.
            if (!float.TryParse(parts[3].Trim(), out float padding))
            {
                Console.WriteLine($"Invalid padding value on line: {line}");
                continue;
            }

            // Generate the barcode using the specified symbology (Code128) and text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply uniform padding (in points) to all sides of the barcode.
                generator.Parameters.Barcode.Padding.Left.Point = padding;
                generator.Parameters.Barcode.Padding.Top.Point = padding;
                generator.Parameters.Barcode.Padding.Right.Point = padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = padding;

                // Set the rotation angle (in degrees) for the barcode image.
                generator.Parameters.RotationAngle = rotation;

                // Save the generated barcode as a PNG file (default format).
                generator.Save(outputFile);
                Console.WriteLine($"Generated '{outputFile}' for code '{codeText}'.");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}