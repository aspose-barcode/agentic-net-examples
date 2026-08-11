// Title: Generate Swiss Post Parcel Barcodes and CSV Index
// Description: Creates a set of Swiss Post Parcel additional service code barcodes, saves them as PNG files, and builds a CSV file that maps each identifier to its image file.
// Category-Description: This example demonstrates the Aspose.BarCode generation API for Swiss Post Parcel barcodes. It shows how to configure a BarcodeGenerator, set barcode parameters such as X‑dimension, and export images. Typical use cases include batch creation of parcel service codes and maintaining an index for downstream processing. Developers working with barcode generation, bulk image output, and CSV reporting will find this pattern useful.
// Prompt: Generate a batch of Swiss Post Parcel additional service code barcodes and produce a CSV index linking identifiers.
// Tags: barcode, swisspostparcel, generation, png, csv, aspose.barcode, encode types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel barcodes and creation of a CSV index file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images and writes a CSV index.
    /// </summary>
    static void Main()
    {
        // Determine output directory for barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Define the CSV index file path within the output directory
        string csvPath = Path.Combine(outputDir, "index.csv");

        // Prepare CSV content: header line followed by data rows
        List<string> csvLines = new List<string>();
        csvLines.Add("Identifier,FileName");

        // Generate a small batch of barcodes (5 samples)
        for (int i = 1; i <= 5; i++)
        {
            // Build a unique identifier for each barcode
            string identifier = $"ID{i:D3}";
            string fileName = $"{identifier}.png";
            string filePath = Path.Combine(outputDir, fileName);
            string codeText = identifier; // Use the identifier as the barcode's codetext

            // Create and configure the barcode generator for Swiss Post Parcel symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
            {
                // Set module (X) size to 2 points
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Disable exception throwing for incorrect codetext (optional)
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode image as PNG
                generator.Save(filePath);
            }

            // Record the identifier and corresponding file name in the CSV data
            csvLines.Add($"{identifier},{fileName}");
            Console.WriteLine($"Generated barcode for {identifier} -> {fileName}");
        }

        // Write all CSV lines to the index file
        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            foreach (string line in csvLines)
            {
                writer.WriteLine(line);
            }
        }

        Console.WriteLine($"CSV index created at: {csvPath}");
    }
}