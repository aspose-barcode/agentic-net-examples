// Title: Generate barcode images from CSV with padding and rotation
// Description: Demonstrates reading a CSV file containing barcode data, rotation angles, and padding values, then creating PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as rotation, padding, and XDimension. It shows typical usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for batch processing of barcode data. Developers often need to automate barcode creation from data sources like CSV files, applying visual adjustments for layout requirements.
// Prompt: Create an app that reads a CSV list of barcode data and generates images with padding and rotation.
// Tags: barcode, csv, generation, padding, rotation, png, aspose.barcode, code128

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Reads a CSV file with barcode specifications and generates PNG images applying
/// rotation and individual padding values using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Handles CSV discovery/creation, parses each row,
    /// configures a BarcodeGenerator, and saves the resulting image.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV path: use argument if provided, otherwise create a sample CSV in a temp folder.
        string csvPath;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            csvPath = args[0];
        }
        else
        {
            // Create a temporary folder for the demo.
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeCsvDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // Sample CSV file path.
            csvPath = Path.Combine(tempFolder, "barcodes.csv");

            // Write sample CSV content.
            // Columns: CodeText, RotationAngle, PaddingLeft, PaddingTop, PaddingRight, PaddingBottom
            var sampleLines = new List<string>
            {
                "CodeText,RotationAngle,PaddingLeft,PaddingTop,PaddingRight,PaddingBottom",
                "1234567890,0,5,5,5,5",
                "HELLO-WORLD,90,10,10,10,10",
                "ASP.NET,180,15,5,15,5",
                "BARCODE,270,0,0,0,0"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Verify CSV existence.
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Prepare output folder for generated images.
        string outputFolder = Path.Combine(Path.GetDirectoryName(csvPath) ?? Directory.GetCurrentDirectory(), "GeneratedBarcodes");
        Directory.CreateDirectory(outputFolder);

        // Read all lines, skip header.
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV file does not contain data rows.");
            return;
        }

        // Process each data line.
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Invalid line format at row {i + 1}: {line}");
                continue;
            }

            string codeText = parts[0].Trim();

            // Parse rotation angle; default to 0 if missing/invalid.
            float rotationAngle = 0f;
            if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float parsedAngle))
                rotationAngle = parsedAngle;

            // Parse padding values; default to 0 if missing/invalid.
            float padLeft = 0f, padTop = 0f, padRight = 0f, padBottom = 0f;
            if (parts.Length > 2 && float.TryParse(parts[2].Trim(), out float pL)) padLeft = pL;
            if (parts.Length > 3 && float.TryParse(parts[3].Trim(), out float pT)) padTop = pT;
            if (parts.Length > 4 && float.TryParse(parts[4].Trim(), out float pR)) padRight = pR;
            if (parts.Length > 5 && float.TryParse(parts[5].Trim(), out float pB)) padBottom = pB;

            // Create barcode generator using Code128 as a generic example.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set rotation.
                generator.Parameters.RotationAngle = rotationAngle;

                // Set individual paddings.
                generator.Parameters.Barcode.Padding.Left.Point = padLeft;
                generator.Parameters.Barcode.Padding.Top.Point = padTop;
                generator.Parameters.Barcode.Padding.Right.Point = padRight;
                generator.Parameters.Barcode.Padding.Bottom.Point = padBottom;

                // Optional: set XDimension for visual size (module width).
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Build output file name, sanitizing invalid characters.
                string safeCodeText = codeText.Replace(Path.GetInvalidFileNameChars(), '_');
                string outputPath = Path.Combine(outputFolder, $"{safeCodeText}_{i}.png");

                // Save barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode for '{codeText}' -> {outputPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}

// Extension method to replace invalid filename characters.
/// <summary>
/// Provides a helper to replace an array of characters in a string with a specified replacement character.
/// </summary>
static class StringExtensions
{
    /// <summary>
    /// Returns a new string where each occurrence of any character in <paramref name="chars"/> is replaced with <paramref name="replacement"/>.
    /// </summary>
    /// <param name="str">The original string.</param>
    /// <param name="chars">Array of characters to replace.</param>
    /// <param name="replacement">The character to insert in place of each found character.</param>
    /// <returns>The sanitized string.</returns>
    public static string Replace(this string str, char[] chars, char replacement)
    {
        foreach (var c in chars)
        {
            str = str.Replace(c, replacement);
        }
        return str;
    }
}