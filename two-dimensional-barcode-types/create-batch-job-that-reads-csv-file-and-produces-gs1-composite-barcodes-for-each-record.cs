// Title: Batch generation of GS1 Composite barcodes from CSV
// Description: Demonstrates reading a CSV file and creating a GS1 Composite barcode image for each record, saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType classes to produce combined linear and 2D barcodes. Developers often need batch processing to automate barcode creation for inventory, shipping, or product labeling scenarios.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, batch processing, png output, barcode generation, aspose.barcode, encode types, twodcomponenttype

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of GS1 Composite barcodes from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Reads input CSV, generates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define input CSV file name and output directory for barcode images
        string inputCsv = "input.csv";
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file does not exist, create a sample file with a few records
        if (!File.Exists(inputCsv))
        {
            string[] sampleLines =
            {
                "01012345678901, (21)A12345678",
                "01098765432109, (21)B87654321",
                "01055555555555, (21)C55555555",
                "01011111111111, (21)D11111111",
                "01022222222222, (21)E22222222"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(inputCsv);
        int index = 1;

        // Process each non‑empty line
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Expect two columns separated by a comma: linear part and 2D part
            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                Console.WriteLine($"Skipping invalid line {index}: {line}");
                index++;
                continue;
            }

            // Trim whitespace from each part
            string linearPart = parts[0].Trim();
            string twoDPart = parts[1].Trim();

            // Combine linear and 2D components using '|' as required for GS1 Composite
            string codeText = $"{linearPart}|{twoDPart}";

            // Generate the GS1 Composite barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set the linear component to GS1‑Code128 and the 2D component to CC‑A
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Optional: adjust additional barcode settings
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Build the output file path and save the barcode as PNG
                string outputPath = Path.Combine(outputFolder, $"barcode_{index}.png");
                generator.Save(outputPath);
                Console.WriteLine($"Saved barcode {index} to {outputPath}");
            }

            index++;
        }

        // All barcodes have been generated
    }
}