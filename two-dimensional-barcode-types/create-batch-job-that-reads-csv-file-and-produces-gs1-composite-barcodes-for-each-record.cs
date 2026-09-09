// Title: Batch generation of GS1 Composite barcodes from CSV
// Description: Demonstrates reading a CSV file and creating a GS1 Composite barcode image for each record, saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2‑D components, and export images. Developers often need to automate barcode creation for large data sets, integrating CSV input, component configuration, and image output in a single workflow.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, batch generation, png output, barcodegenerator, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that reads a CSV file and generates GS1 Composite barcodes for each record.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, writes a sample CSV, processes each line,
    /// generates a GS1 Composite barcode image, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch job
        string batchFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a sample CSV file (LinearComponent,TwoDComponent)
        string csvPath = Path.Combine(batchFolder, "input.csv");
        var sampleLines = new List<string>
        {
            "LinearComponent,TwoDComponent",
            "(01)12345678901231,(01)00123456789012",
            "(01)98765432109876,(01)00987654321098",
            "(01)55555555555555,(01)00555555555555"
        };
        File.WriteAllLines(csvPath, sampleLines);

        // Read all lines from the CSV file
        string[] csvLines = File.ReadAllLines(csvPath);
        for (int i = 1; i < csvLines.Length; i++) // skip header row
        {
            string line = csvLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // ignore empty lines

            // Split the line into linear and 2‑D components
            string[] parts = line.Split(',');
            if (parts.Length != 2)
                continue; // ignore malformed rows

            string linear = parts[0].Trim();
            string twoD = parts[1].Trim();

            // Combine linear and 2‑D parts with pipe separator as required by GS1 Composite
            string codeText = $"{linear}|{twoD}";

            // Define the output file path for the generated barcode image
            string outputPath = Path.Combine(batchFolder, $"barcode_{i}.png");

            // Generate GS1 Composite barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set basic visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure the linear and 2‑D components of the composite barcode
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

                // For CC_C (PDF417) set the column count to control barcode size
                generator.Parameters.Barcode.Pdf417.Columns = 30;

                // Save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode {i}: {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}