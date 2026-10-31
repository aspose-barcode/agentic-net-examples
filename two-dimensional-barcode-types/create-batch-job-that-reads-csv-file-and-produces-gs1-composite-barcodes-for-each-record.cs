// Title: Batch Generation of GS1 Composite Barcodes from CSV
// Description: Demonstrates how to read a CSV file and generate a GS1 Composite barcode for each record, saving the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to create linear and 2‑D components. Typical use cases include bulk barcode creation for inventory, shipping, or product labeling where data is stored in CSV files. Developers often need to automate barcode production, configure visual properties, and handle file I/O efficiently.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, barcode, csv, batch, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads a CSV file and generates GS1 Composite barcodes for each line.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes the CSV, creates output folder, and generates PNG barcode images.
    /// </summary>
    static void Main()
    {
        // Determine CSV input path (temporary folder)
        string csvPath = Path.Combine(Path.GetTempPath(), "GS1CompositeBatch", "input.csv");

        // Ensure CSV exists; create a sample file if missing
        if (!File.Exists(csvPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(csvPath));
            string[] sampleLines = new string[]
            {
                "(01)01234567890128,(01)00123456789012",
                "(01)12345678901231,(01)00123456789012"
            };
            File.WriteAllLines(csvPath, sampleLines);
            Console.WriteLine($"Sample CSV created at: {csvPath}");
        }

        // Create a unique output folder for the generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        int index = 1;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split the line into linear and 2‑D components
            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping malformed line {index}: {line}");
                index++;
                continue;
            }

            string linearPart = parts[0].Trim();
            string twoDPart = parts[1].Trim();
            string codeText = $"{linearPart}|{twoDPart}";

            // Build the output file path for the current barcode
            string outputPath = Path.Combine(outputFolder, $"barcode_{index}.png");

            try
            {
                // Initialize the barcode generator for GS1 Composite symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
                {
                    // Configure GS1 Composite parameters
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                    generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                    // Optional visual settings
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {index}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for line {index}: {ex.Message}");
            }

            index++;
        }
    }
}