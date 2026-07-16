// Title: Batch DataMatrix Barcode Generation from JSON
// Description: Reads a JSON array of strings and creates a DataMatrix barcode image for each entry, saving them to a specified folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.DataMatrix to produce barcode images in bulk. Typical use cases include batch processing of product codes, inventory tags, or any list of identifiers that need to be encoded as DataMatrix symbols. Developers often need to read input data from files (e.g., JSON, CSV) and output PNG or other image formats, handling filename safety and folder management.
// Prompt: Create a batch routine that reads a JSON array and produces DataMatrix barcodes saved to a specified folder.
// Tags: datamatrix, barcode generation, batch processing, json, aspose.barcode, png, c#
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of DataMatrix barcodes from a JSON array using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads input arguments, processes a JSON file, and generates DataMatrix PNG images.
    /// </summary>
    /// <param name="args">Optional arguments: [0] = JSON file path, [1] = output folder.</param>
    static void Main(string[] args)
    {
        // Determine input JSON file and output folder from arguments or use defaults
        string jsonPath = args.Length > 0 ? args[0] : "codes.json";
        string outputFolder = args.Length > 1 ? args[1] : "DataMatrixBarcodes";

        // If the JSON file does not exist, create a small sample file
        if (!File.Exists(jsonPath))
        {
            var sampleCodes = new List<string> { "ABC123", "HelloWorld", "2023-07-12", "DataMatrix", "Sample5" };
            string sampleJson = JsonSerializer.Serialize(sampleCodes);
            File.WriteAllText(jsonPath, sampleJson);
            Console.WriteLine($"Sample JSON file created at '{jsonPath}'.");
        }

        // Read and deserialize the JSON array
        List<string> codeTexts;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            codeTexts = JsonSerializer.Deserialize<List<string>>(jsonContent);
            if (codeTexts == null)
                throw new InvalidOperationException("Deserialized JSON is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read or parse JSON file: {ex.Message}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each code text and generate a DataMatrix barcode
        int index = 0;
        foreach (string code in codeTexts)
        {
            // Skip empty entries
            if (string.IsNullOrWhiteSpace(code))
                continue;

            // Create a DataMatrix barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, code))
            {
                // Configure DataMatrix specific parameters
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
                generator.Parameters.Barcode.DataMatrix.AspectRatio = 1f; // square
                generator.Parameters.Barcode.XDimension.Point = 2f; // size of a module
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Optional: set colors (white background, black bars)
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Build a safe file name
                string safeName = GetSafeFileName(code);
                if (string.IsNullOrEmpty(safeName))
                {
                    safeName = $"barcode_{index}";
                }

                string outputPath = Path.Combine(outputFolder, $"{safeName}.png");

                // Save the barcode image as PNG
                generator.Save(outputPath);
                Console.WriteLine($"Saved DataMatrix barcode for '{code}' to '{outputPath}'.");
            }

            index++;
        }
    }

    // Helper to replace invalid filename characters and limit length
    private static string GetSafeFileName(string input)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, '_');
        }
        // Trim length to avoid overly long filenames
        return input.Length > 100 ? input.Substring(0, 100) : input;
    }
}