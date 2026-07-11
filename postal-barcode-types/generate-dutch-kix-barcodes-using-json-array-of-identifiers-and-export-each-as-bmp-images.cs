// Title: Generate Dutch KIX Barcodes from JSON and Export as BMP
// Description: This example reads a JSON array of 8‑digit identifiers, creates Dutch KIX barcodes for each, and saves them as BMP images.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.DutchKIX. Typical use cases include batch creation of KIX barcodes for inventory or logistics, reading identifiers from JSON, configuring barcode parameters, and exporting to bitmap files. Developers often need to validate input, manage output folders, and handle serialization when automating barcode production.
// Prompt: Generate Dutch KIX barcodes using a JSON array of identifiers and export each as BMP images.
// Tags: dutch kix, barcode generation, bmp, aspose.barcode, json, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Reads a JSON file containing an array of identifiers, generates Dutch KIX barcodes,
/// and saves each barcode as a BMP image in the output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles JSON loading, identifier validation,
    /// barcode generation, and image export.
    /// </summary>
    static void Main()
    {
        // Path to the JSON file that holds the identifier array.
        const string jsonFile = "identifiers.json";

        // Load JSON content; fall back to a hard‑coded sample if the file is missing.
        string jsonContent;
        if (File.Exists(jsonFile))
        {
            jsonContent = File.ReadAllText(jsonFile);
        }
        else
        {
            // Sample identifiers for Dutch KIX (numeric, exactly 8 characters).
            jsonContent = "[\"12345678\", \"87654321\", \"11223344\"]";
        }

        // Deserialize the JSON array into a string[].
        string[] identifiers;
        try
        {
            identifiers = JsonSerializer.Deserialize<string[]>(jsonContent);
            if (identifiers == null || identifiers.Length == 0)
                throw new ArgumentException("No identifiers found in JSON.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse identifiers: {ex.Message}");
            return;
        }

        // Ensure the output directory exists (creates it if necessary).
        const string outputDir = "output";
        Directory.CreateDirectory(outputDir);

        // Iterate over each identifier, validate it, generate a barcode, and save as BMP.
        foreach (string id in identifiers)
        {
            // Validate identifier: must be numeric and exactly 8 characters for KIX.
            if (string.IsNullOrWhiteSpace(id) || id.Length != 8 || !long.TryParse(id, out _))
            {
                Console.WriteLine($"Skipping invalid identifier: '{id}'");
                continue;
            }

            // Build the full path for the output BMP file.
            string outputPath = Path.Combine(outputDir, $"{id}.bmp");

            // Create and configure the barcode generator for Dutch KIX.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DutchKIX, id))
            {
                // Optional: set X dimension (size of the smallest bar) to 2 points.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode image as BMP.
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Generated barcode for '{id}' -> {outputPath}");
        }
    }
}