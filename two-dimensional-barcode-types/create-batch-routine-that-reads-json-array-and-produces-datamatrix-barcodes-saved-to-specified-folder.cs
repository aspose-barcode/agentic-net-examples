// Title: Batch generation of DataMatrix barcodes from JSON array
// Description: Demonstrates reading a JSON array of strings and creating a DataMatrix barcode image for each entry, saving them to a specified folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DataMatrix, configure ECI encoding for Unicode support, and batch‑process multiple inputs. Developers working with bulk barcode creation, data export, or inventory labeling can adapt this pattern for automated workflows.
// Prompt: Create a batch routine that reads a JSON array and produces DataMatrix barcodes saved to a specified folder.
// Tags: datamatrix, barcode generation, json, batch processing, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates DataMatrix barcodes from a JSON array of strings and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads input arguments, prepares JSON data, and creates barcode images.
    /// </summary>
    /// <param name="args">Optional arguments: [0] path to JSON file, [1] output folder path.</param>
    static void Main(string[] args)
    {
        // Determine input JSON file path (use provided argument or fallback to a temp sample file)
        string jsonPath = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "sample.json");

        // Determine output folder for barcodes (use provided argument or create a unique temp folder)
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the JSON file does not exist, create a sample one with example data
        if (!File.Exists(jsonPath))
        {
            var sampleData = new List<string> { "Hello", "World", "DataMatrix 🚀", "12345", "Sample Text" };
            string sampleJson = JsonSerializer.Serialize(sampleData);
            File.WriteAllText(jsonPath, sampleJson);
            Console.WriteLine($"Sample JSON created at: {jsonPath}");
        }

        // Read and parse the JSON array into a list of strings
        List<string> items;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            items = JsonSerializer.Deserialize<List<string>>(jsonContent);
            if (items == null)
                throw new Exception("Deserialized list is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read or parse JSON file: {ex.Message}");
            return;
        }

        // Generate a DataMatrix barcode for each item in the list
        for (int i = 0; i < items.Count; i++)
        {
            string codeText = items[i] ?? string.Empty;
            string fileName = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            try
            {
                // Initialize the barcode generator with DataMatrix symbology and the item text
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    // Enable ECI mode with UTF-8 to support Unicode characters
                    generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
                    generator.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.UTF8;

                    // Save the generated barcode image as a PNG file
                    generator.Save(fileName);
                }

                Console.WriteLine($"Saved barcode {i + 1} to: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for item {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}