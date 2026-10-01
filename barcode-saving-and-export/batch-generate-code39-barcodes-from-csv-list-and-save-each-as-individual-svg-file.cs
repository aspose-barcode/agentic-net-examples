// Title: Batch generation of Code39 barcodes from CSV to SVG files
// Description: Demonstrates how to read a list of Code39 values from a CSV file, generate a barcode for each entry, and save the results as individual SVG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Code39. Typical use cases include bulk creation of barcodes for inventory, shipping labels, or product catalogs, where developers need to automate reading data sources and exporting vector graphics. The key API classes shown are BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, commonly used for programmatic barcode creation and format selection.
// Prompt: Batch generate Code39 barcodes from a CSV list and save each as an individual SVG file.
// Tags: code39, barcode, generation, svg, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads Code39 values from a CSV file,
/// generates a barcode for each value, and saves each barcode as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for the demo files.
        string workDir = Path.Combine(Path.GetTempPath(), "Code39Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Prepare a sample CSV file containing the code texts to be encoded.
        string csvPath = Path.Combine(workDir, "codes.csv");
        string[] sampleCodes = new string[] { "ABC123", "XYZ789", "CODE39", "HELLO-WORLD", "12345" };
        File.WriteAllLines(csvPath, sampleCodes);

        // Create an output folder where the generated SVG files will be stored.
        string outputDir = Path.Combine(workDir, "Output");
        Directory.CreateDirectory(outputDir);

        // Verify that the CSV file exists before attempting to read it.
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found.");
            return;
        }

        // Read all lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        foreach (string rawLine in lines)
        {
            // Trim whitespace and skip empty lines.
            string codeText = rawLine.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue;

            // Generate a Code39 barcode for the current text and save it as an SVG file.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39))
            {
                // Assign the code text using UTF-8 encoding.
                generator.SetCodeText(codeText, Encoding.UTF8);

                // Build the full path for the output SVG file.
                string outputPath = Path.Combine(outputDir, $"{codeText}.svg");
                try
                {
                    // Save the barcode image in SVG format.
                    generator.Save(outputPath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved barcode for '{codeText}' to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Handle potential licensing or format restrictions.
                    Console.WriteLine($"Failed to save barcode for '{codeText}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}