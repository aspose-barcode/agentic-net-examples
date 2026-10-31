// Title: Generate Barcode Images from CSV with Padding and Rotation
// Description: This example reads barcode specifications from a CSV file (or uses built‑in sample data) and creates PNG images applying custom padding and rotation.
// Category-Description: Demonstrates Aspose.BarCode generation workflow, including EncodeTypes resolution, BarcodeGenerator configuration, and image export. Ideal for developers needing batch barcode creation with layout adjustments such as padding and rotation. Covers key classes like EncodeTypes, BarcodeGenerator, and BarCodeImageFormat, typical for inventory, shipping, or labeling solutions.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode, symbology, generation, padding, rotation, csv, png, aspose.barcode, aspnet

using System;
using System.IO;
using System.Globalization;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading barcode definitions from a CSV file and generating PNG images
/// with custom padding and rotation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional CSV file path argument, processes each line,
    /// and writes barcode images to a temporary folder.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV source: command‑line argument or fallback to sample data.
        string csvPath = args.Length > 0 ? args[0] : string.Empty;
        string[] csvLines;

        if (!string.IsNullOrEmpty(csvPath) && File.Exists(csvPath))
        {
            // Load all lines from the provided CSV file.
            csvLines = File.ReadAllLines(csvPath);
        }
        else
        {
            // Use built‑in sample CSV data when no valid file is supplied.
            // Format: Symbology,CodeText,PadLeft,PadTop,PadRight,PadBottom,RotationAngle
            csvLines = new[]
            {
                "Code128,Sample123,10,10,10,10,45",
                "QR,Hello World,5,5,5,5,90",
                "DataMatrix,ABCDEF,15,0,15,0,-45"
            };
        }

        // Create a unique temporary output folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        int lineNumber = 0;
        // Process each CSV line individually.
        foreach (string rawLine in csvLines)
        {
            lineNumber++;
            string line = rawLine.Trim();

            // Skip empty lines or lines that start with a comment marker.
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;

            // Split the line into its constituent columns.
            string[] parts = line.Split(',');
            if (parts.Length < 7)
            {
                Console.WriteLine($"Line {lineNumber}: insufficient columns, skipping.");
                continue;
            }

            // Extract and trim individual values.
            string symbologyName = parts[0].Trim();
            string codeText = parts[1].Trim();

            // Parse numeric padding and rotation values using invariant culture.
            if (!float.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float padLeft) ||
                !float.TryParse(parts[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float padTop) ||
                !float.TryParse(parts[4].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float padRight) ||
                !float.TryParse(parts[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float padBottom) ||
                !float.TryParse(parts[6].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float rotationAngle))
            {
                Console.WriteLine($"Line {lineNumber}: invalid numeric values, skipping.");
                continue;
            }

            // Resolve the symbology enum value via reflection on EncodeTypes.
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Line {lineNumber}: unknown symbology '{symbologyName}', skipping.");
                continue;
            }

            BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
            if (encodeType == null)
            {
                Console.WriteLine($"Line {lineNumber}: failed to obtain encode type for '{symbologyName}', skipping.");
                continue;
            }

            // Build the output file path for the current barcode image.
            string outputPath = Path.Combine(outputFolder, $"barcode_{lineNumber}.png");

            // Generate the barcode with specified padding and rotation.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply custom padding (in points).
                generator.Parameters.Barcode.Padding.Left.Point = padLeft;
                generator.Parameters.Barcode.Padding.Top.Point = padTop;
                generator.Parameters.Barcode.Padding.Right.Point = padRight;
                generator.Parameters.Barcode.Padding.Bottom.Point = padBottom;

                // Apply rotation angle.
                generator.Parameters.RotationAngle = rotationAngle;

                // Save the barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Line {lineNumber}: barcode saved to {outputPath}");
        }

        Console.WriteLine("Processing completed.");
    }
}