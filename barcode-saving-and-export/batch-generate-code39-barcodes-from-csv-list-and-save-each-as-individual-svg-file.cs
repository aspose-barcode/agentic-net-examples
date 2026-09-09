// Title: Batch generate Code39 barcodes from CSV and save as SVG files
// Description: Demonstrates reading a list of values from a CSV file, generating a Code39 barcode for each entry, and saving each barcode as an individual SVG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Code39. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs. Developers often need to read data sources (e.g., CSV, databases) and output barcodes in vector formats like SVG for scalable rendering.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode, batch, csv, svg, generation, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads barcode data from a CSV file,
/// generates Code39 barcodes using Aspose.BarCode, and saves each barcode as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary CSV, processes each line,
    /// generates a Code39 barcode, and writes the result to an SVG file.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the CSV file and output SVGs
        string baseFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        string csvPath = Path.Combine(baseFolder, "codes.csv");
        string outputFolder = Path.Combine(baseFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Prepare a sample CSV file (header + barcode values)
        string[] sampleLines = new[]
        {
            "Code",
            "12345",
            "ABC-123",
            "XYZ789",
            "CODE39TEST"
        };
        File.WriteAllLines(csvPath, sampleLines, Encoding.UTF8);

        // Read all lines from the CSV, using UTF-8 encoding
        string[] allLines = File.ReadAllLines(csvPath, Encoding.UTF8);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV does not contain any barcode data.");
            return;
        }

        // Iterate over each data line (skip header at index 0)
        for (int i = 1; i < allLines.Length; i++)
        {
            string codeText = allLines[i].Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip empty rows

            // Build a safe file name for the SVG output
            string safeFileName = $"{i}_{SanitizeFileName(codeText)}.svg";
            string outputPath = Path.Combine(outputFolder, safeFileName);

            try
            {
                // Initialize the barcode generator for Code39
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
                {
                    // Ensure the code text is encoded as UTF-8 (optional but explicit)
                    generator.SetCodeText(codeText, Encoding.UTF8);
                    // Save the generated barcode as an SVG image
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                }
                Console.WriteLine($"Generated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for '{codeText}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }

    /// <summary>
    /// Replaces characters that are invalid in file names with an underscore.
    /// </summary>
    /// <param name="name">Original file name string.</param>
    /// <returns>A sanitized file name safe for use on the file system.</returns>
    static string SanitizeFileName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder(name.Length);
        foreach (char c in name)
        {
            sb.Append(Array.IndexOf(invalid, c) >= 0 ? '_' : c);
        }
        return sb.ToString();
    }
}