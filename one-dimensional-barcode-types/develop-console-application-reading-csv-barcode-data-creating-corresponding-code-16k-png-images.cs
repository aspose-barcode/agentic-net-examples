// Title: Generate Code 16K Barcodes from CSV Data
// Description: This example reads barcode parameters from a CSV file and creates corresponding Code 16K PNG images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode generation for Code 16K symbology, covering CSV input handling, barcode parameter configuration, and image export. Useful for developers needing batch barcode creation, customizing aspect ratio and quiet zones, and saving PNG files. Part of a series on barcode generation with Aspose.BarCode API classes like BarcodeGenerator and EncodeTypes.
// Prompt: Develop console application reading CSV barcode data, creating corresponding Code 16K PNG images.
// Tags: code16k, barcode, csv, png, generation, aspose.barcode, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that reads barcode data from a CSV file and generates Code 16K PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary working folder, writes sample CSV data,
    /// reads each line, configures barcode parameters, and saves the generated images.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated files
        string workDir = Path.Combine(Path.GetTempPath(), "Code16KDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define the path for the sample CSV file
        string csvPath = Path.Combine(workDir, "data.csv");

        // Write sample CSV content: CodeText,AspectRatio,QuietZoneLeftCoef,QuietZoneRightCoef
        string[] sampleLines =
        {
            "ASP.NET,10,10,1",
            "Aspose.BarCode,15,12,2",
            "Code16KExample,20,15,3"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Verify that the CSV file exists before processing
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found.");
            return;
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int lineNumber = 0;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into individual fields
            string[] parts = line.Split(',');
            if (parts.Length < 1)
            {
                Console.WriteLine($"Line {lineNumber}: insufficient data.");
                continue;
            }

            // Extract mandatory barcode text
            string codeText = parts[0].Trim();

            // Set default values for optional parameters
            float aspectRatio = 10f;
            int quietLeft = 10;
            int quietRight = 1;

            // Parse optional AspectRatio
            if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float ar))
                aspectRatio = ar;

            // Parse optional QuietZoneLeftCoef
            if (parts.Length > 2 && int.TryParse(parts[2].Trim(), out int ql))
                quietLeft = ql;

            // Parse optional QuietZoneRightCoef
            if (parts.Length > 3 && int.TryParse(parts[3].Trim(), out int qr))
                quietRight = qr;

            // Enforce minimum quiet zone constraints
            if (quietLeft < 10) quietLeft = 10;
            if (quietRight < 1) quietRight = 1;

            // Determine output PNG file path for the current barcode
            string outputPath = Path.Combine(workDir, $"Barcode_{lineNumber}.png");

            try
            {
                // Initialize the barcode generator with Code16K symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Configure common barcode parameters
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeft;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRight;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {lineNumber}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode on line {lineNumber}: {ex.Message}");
            }
        }

        Console.WriteLine($"All barcode images saved to: {workDir}");
    }
}