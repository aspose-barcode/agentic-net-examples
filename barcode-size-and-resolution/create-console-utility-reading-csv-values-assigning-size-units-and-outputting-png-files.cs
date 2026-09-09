// Title: Generate DataMatrix barcodes from CSV with size units and save as PNG
// Description: This example reads barcode data from a CSV file, sets the X‑dimension using either pixels or millimeters, and saves each barcode as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. Shows how to configure barcode parameters such as XDimension, choose unit types, and export images in PNG format. Useful for developers needing batch barcode creation from data sources like CSV files.
// Prompt: Create console utility reading CSV values, assigning size units, and outputting PNG files.
// Tags: datamatrix, barcode generation, png, csv, xdimension, console

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console utility that reads barcode specifications from a CSV file,
/// applies size units to the X‑dimension, and generates PNG images for each entry.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, writes sample CSV data,
    /// processes each line to generate a DataMatrix barcode with the specified size unit,
    /// and outputs the paths of the generated PNG files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample CSV content: CodeText,UnitType,UnitValue
        string csvPath = Path.Combine(outputFolder, "data.csv");
        string[] csvLines = new[]
        {
            "ABC123,Pixels,3",
            "XYZ789,Millimeters,2",
            "HELLO,Pixels,5"
        };
        File.WriteAllLines(csvPath, csvLines);

        // Read and process CSV
        List<string> createdFiles = new List<string>();
        foreach (string line in File.ReadAllLines(csvPath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split line into parts: code text, unit type, unit value
            string[] parts = line.Split(',');
            if (parts.Length != 3)
                continue; // skip malformed lines

            string codeText = parts[0].Trim();
            string unitType = parts[1].Trim().ToLowerInvariant();

            // Parse unit value; ignore non‑positive or invalid numbers
            if (!float.TryParse(parts[2].Trim(), out float unitValue) || unitValue <= 0f)
                continue; // skip invalid unit values

            string outputPath = Path.Combine(outputFolder, $"{codeText}.png");

            // Generate barcode with specified X‑dimension unit
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                if (unitType == "pixels")
                {
                    generator.Parameters.Barcode.XDimension.Pixels = unitValue;
                }
                else if (unitType == "millimeters")
                {
                    generator.Parameters.Barcode.XDimension.Millimeters = unitValue;
                }
                else
                {
                    // Unknown unit type, skip this entry
                    continue;
                }

                // Save barcode as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                createdFiles.Add(outputPath);
            }
        }

        // Output result summary
        Console.WriteLine("Generated barcode files:");
        foreach (string file in createdFiles)
        {
            Console.WriteLine(file);
        }
    }
}