// Title: Generate Barcode Images from CSV with Padding and Rotation
// Description: Demonstrates reading a CSV file containing barcode parameters, creating barcode images with specified padding and rotation, and saving them to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameter classes to produce barcodes. Typical use cases include batch processing of barcode data, applying visual adjustments such as padding and rotation, and exporting images in common formats. Developers often need to read external data sources (e.g., CSV) and programmatically configure barcode appearance for integration into reports, labels, or web services.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode, symbology, generation, padding, rotation, csv, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Reads barcode specifications from a CSV file, generates corresponding barcode images
/// with custom padding and rotation, and saves them to a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Executes the CSV processing and barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file (Symbology,CodeText,PadLeft,PadTop,PadRight,PadBottom,Rotation)
        string csvPath = Path.Combine(outputFolder, "data.csv");
        string[] csvLines =
        {
            "Code128,ABC123,10,10,10,10,45",
            "QR,https://example.com,5,5,5,5,90",
            "DataMatrix,SampleText,15,0,15,0,-30"
        };
        File.WriteAllLines(csvPath, csvLines);

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into individual columns
            string[] parts = line.Split(',');
            if (parts.Length < 7)
            {
                Console.WriteLine($"Skipping line {index}: insufficient columns.");
                index++;
                continue;
            }

            // Extract barcode parameters
            string symbologyName = parts[0].Trim();
            string codeText = parts[1].Trim();

            // Parse numeric values for padding and rotation
            if (!float.TryParse(parts[2].Trim(), out float padLeft) ||
                !float.TryParse(parts[3].Trim(), out float padTop) ||
                !float.TryParse(parts[4].Trim(), out float padRight) ||
                !float.TryParse(parts[5].Trim(), out float padBottom) ||
                !float.TryParse(parts[6].Trim(), out float rotation))
            {
                Console.WriteLine($"Skipping line {index}: invalid numeric values.");
                index++;
                continue;
            }

            // Resolve symbology name to BaseEncodeType via reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Skipping line {index}: unknown symbology '{symbologyName}'.");
                index++;
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            try
            {
                // Initialize the barcode generator with the resolved symbology and code text
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Apply padding (specified in points)
                    generator.Parameters.Barcode.Padding.Left.Point = padLeft;
                    generator.Parameters.Barcode.Padding.Top.Point = padTop;
                    generator.Parameters.Barcode.Padding.Right.Point = padRight;
                    generator.Parameters.Barcode.Padding.Bottom.Point = padBottom;

                    // Apply rotation angle
                    generator.Parameters.RotationAngle = rotation;

                    // Save the generated barcode as a PNG image
                    string imagePath = Path.Combine(outputFolder, $"barcode_{index}.png");
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode {index}: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode on line {index}: {ex.Message}");
            }

            index++;
        }

        Console.WriteLine($"All barcode images saved to: {outputFolder}");
    }
}