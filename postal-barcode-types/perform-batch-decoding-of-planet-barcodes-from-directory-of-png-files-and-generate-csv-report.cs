// Title: Batch decode Planet barcodes and generate CSV report
// Description: Demonstrates how to read multiple Planet barcode images from a folder, decode them, and export the results to a CSV file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating the use of BarCodeReader for decoding and BarcodeGenerator for creating sample barcodes. Typical use cases include bulk barcode validation, inventory scanning, and automated reporting where developers need to process many images and collect decoded data in a structured format.
// Prompt: Perform batch decoding of Planet barcodes from a directory of PNG files and generate a CSV report.
// Tags: planet, barcode, decoding, csv, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console application that batch‑processes Planet barcode images,
/// decodes their contents, and writes a CSV report summarising the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Scans a folder for PNG images containing
    /// Planet barcodes, decodes each image, and creates a CSV file with the
    /// filename and decoded text.
    /// </summary>
    static void Main()
    {
        // Define the folder that holds input PNG files and the output CSV file name.
        string inputFolder = "Barcodes";
        string outputCsv = "PlanetBarcodesReport.csv";

        // Ensure the input folder exists; create it if it does not.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a few sample Planet barcode images for demo purposes.
        if (Directory.GetFiles(inputFolder, "*.png").Length == 0)
        {
            GenerateSampleBarcodes(inputFolder);
        }

        // Initialise a StringBuilder to construct the CSV content.
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,CodeText");

        // Iterate over each PNG file in the input folder.
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.png"))
        {
            // Verify the file still exists (defensive check).
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Use BarCodeReader configured for the Planet symbology to decode the image.
            using (var reader = new BarCodeReader(filePath, DecodeType.Planet))
            {
                var results = reader.ReadBarCodes();

                // If no barcode was detected, write an empty entry for this file.
                if (results.Length == 0)
                {
                    csvBuilder.AppendLine($"{Path.GetFileName(filePath)},");
                    continue;
                }

                // Write each detected barcode to the CSV (Planet typically yields a single result).
                foreach (var result in results)
                {
                    string codeText = result.CodeText ?? string.Empty;

                    // Escape commas in the decoded text to preserve CSV structure.
                    if (codeText.Contains(","))
                    {
                        codeText = $"\"{codeText}\"";
                    }

                    csvBuilder.AppendLine($"{Path.GetFileName(filePath)},{codeText}");
                }
            }
        }

        // Attempt to write the accumulated CSV data to the output file.
        try
        {
            File.WriteAllText(outputCsv, csvBuilder.ToString());
            Console.WriteLine($"Report generated: {outputCsv}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write CSV report: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a set of sample Planet barcode images in the specified folder.
    /// This helper is used only when the input directory is initially empty.
    /// </summary>
    /// <param name="folder">The directory where sample PNG files will be saved.</param>
    private static void GenerateSampleBarcodes(string folder)
    {
        // Sample texts to encode into Planet barcodes.
        string[] sampleTexts = { "12345", "9876543210", "ABCDEF", "00112233", "9999999999" };

        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string text = sampleTexts[i];
            string fileName = Path.Combine(folder, $"Planet_{i + 1}.png");

            // Create a Planet barcode generator for the current sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, text))
            {
                // Optional appearance adjustments.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Save the generated barcode as a PNG image.
                generator.Save(fileName, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine($"Generated {sampleTexts.Length} sample Planet barcode images in '{folder}'.");
    }
}