// Title: Generate barcodes from CSV with padding and rotation
// Description: Demonstrates reading a CSV file containing barcode text, rotation angles, and padding values, then creating PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and image saving options. Typical use cases include batch barcode creation for inventory, shipping labels, or marketing materials where each barcode may require custom rotation and padding. Developers often need to automate image output from data sources such as CSV or databases.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode generation, csv, padding, rotation, png, aspose.barcode, code128, image output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads barcode specifications from a CSV file and generates PNG images with custom padding and rotation.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Handles CSV preparation, barcode generation, and output folder management.
    /// </summary>
    static void Main()
    {
        // Determine the path for the CSV file (placed in the system temporary folder)
        string csvPath = Path.Combine(Path.GetTempPath(), "barcode_data.csv");

        // If the CSV does not exist, create a sample file with header-less data rows
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                // Format: CodeText,RotationAngle,PadLeft,PadTop,PadRight,PadBottom
                "ASPOSE,45,10,10,10,10",
                "12345678,-45,5,5,5,5",
                "HELLO,90,15,0,15,0"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at: {csvPath}");
        }

        // Prepare a unique output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 0;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into individual fields
            string[] parts = line.Split(',');

            // Validate that at least the code text and rotation are present
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping invalid line {index + 1}");
                continue;
            }

            // Extract barcode text
            string codeText = parts[0].Trim();

            // Parse rotation angle; default to 0 if parsing fails
            if (!float.TryParse(parts[1].Trim(), out float rotation))
            {
                Console.WriteLine($"Invalid rotation on line {index + 1}, using 0");
                rotation = 0f;
            }

            // Default padding values (in points)
            float padLeft = 5f, padTop = 5f, padRight = 5f, padBottom = 5f;

            // If padding values are provided, attempt to parse them
            if (parts.Length >= 6)
            {
                float.TryParse(parts[2].Trim(), out padLeft);
                float.TryParse(parts[3].Trim(), out padTop);
                float.TryParse(parts[4].Trim(), out padRight);
                float.TryParse(parts[5].Trim(), out padBottom);
            }

            // Generate the barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply rotation and padding settings
                generator.Parameters.RotationAngle = rotation;
                generator.Parameters.Barcode.Padding.Left.Point = padLeft;
                generator.Parameters.Barcode.Padding.Top.Point = padTop;
                generator.Parameters.Barcode.Padding.Right.Point = padRight;
                generator.Parameters.Barcode.Padding.Bottom.Point = padBottom;

                // Define the output file path
                string outPath = Path.Combine(outputFolder, $"barcode_{index + 1}.png");

                // Save the barcode image as PNG
                generator.Save(outPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode {index + 1} to {outPath}");
            }

            index++;
        }

        Console.WriteLine($"All barcodes generated in folder: {outputFolder}");
    }
}