// Title: Batch generate Code39 barcodes from CSV and save as SVG
// Description: Demonstrates reading a list of Code39 values from a CSV file and creating individual SVG barcode images for each entry.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code39 to produce vector graphics. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs, where developers need to read data sources and output scalable SVG files for web or print. The snippet shows file handling, CSV parsing, and safe filename generation, useful for batch processing scenarios.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode generation, csv, svg, batch processing, aspose.barcode, barcodegenerator

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Code39 barcodes from a CSV file and saving each as an SVG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads codes from a CSV, generates barcodes, and writes SVG files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the demo files
        string baseFolder = Path.Combine(Path.GetTempPath(), "BarcodesDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(baseFolder);

        // Prepare a CSV file containing sample Code39 values
        string csvPath = Path.Combine(baseFolder, "codes.csv");
        string[] sampleCodes = new string[] { "ABC123", "XYZ789", "12345" };
        File.WriteAllLines(csvPath, sampleCodes, Encoding.UTF8);

        // Create an output folder where SVG barcode images will be stored
        string outputFolder = Path.Combine(baseFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Read each line from the CSV and generate a corresponding barcode
        using (var reader = new StreamReader(csvPath, Encoding.UTF8))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                // Skip empty or whitespace‑only lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string codeText = line.Trim();

                // Generate a Code39 barcode for the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
                {
                    generator.SetCodeText(codeText, Encoding.UTF8);

                    // Build a safe file name by replacing invalid characters
                    string safeFileName = codeText.Replace(Path.GetInvalidFileNameChars(), "_");
                    string svgPath = Path.Combine(outputFolder, safeFileName + ".svg");

                    try
                    {
                        // Save the barcode as an SVG image
                        generator.Save(svgPath, BarCodeImageFormat.Svg);
                        Console.WriteLine($"Saved barcode: {svgPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error generating barcode for '{codeText}': {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}

/// <summary>
/// Extension methods for <see cref="System.String"/> used in this example.
/// </summary>
static class PathExtensions
{
    /// <summary>
    /// Replaces each occurrence of any character in <paramref name="chars"/> with the specified <paramref name="replacement"/> string.
    /// </summary>
    /// <param name="source">The original string.</param>
    /// <param name="chars">Array of characters to replace.</param>
    /// <param name="replacement">The string to insert in place of each character.</param>
    /// <returns>A new string with the specified characters replaced.</returns>
    public static string Replace(this string source, char[] chars, string replacement)
    {
        var result = new StringBuilder(source);
        foreach (char c in chars)
        {
            result.Replace(c.ToString(), replacement);
        }
        return result.ToString();
    }
}