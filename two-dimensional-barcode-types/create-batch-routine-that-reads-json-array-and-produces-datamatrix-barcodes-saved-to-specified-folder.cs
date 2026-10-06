// Title: Batch generation of DataMatrix barcodes from JSON input
// Description: Demonstrates reading a JSON array of strings and creating DataMatrix barcodes for each entry, saving them as PNG files in a designated folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DataMatrix. It illustrates typical batch processing scenarios where multiple barcodes are produced from external data sources such as JSON files. Developers often need to automate barcode creation for inventory, shipping, or tracking systems, and this snippet provides a concise pattern for reading input, configuring barcode parameters, and exporting images.
// Prompt: Create a batch routine that reads a JSON array and produces DataMatrix barcodes saved to a specified folder.
// Tags: datamatrix, barcode, batch, json, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that reads a JSON array of strings and generates
/// DataMatrix barcodes for each entry, saving the images to a specified output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles argument parsing, input preparation,
    /// barcode generation, and result reporting.
    /// </summary>
    /// <param name="args">
    /// Optional command‑line arguments:
    /// args[0] – path to the input JSON file (default: ./input.json);
    /// args[1] – path to the output folder (default: temporary folder).
    /// </param>
    static void Main(string[] args)
    {
        // Determine input JSON path and output folder (use defaults if not provided)
        string jsonPath = args.Length > 0 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), "input.json");
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Path.GetTempPath(), "DataMatrixBatch_" + Guid.NewGuid().ToString("N"));

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the JSON file is missing, create a sample file with example data
        if (!File.Exists(jsonPath))
        {
            var sampleData = new List<string> { "Sample1", "HelloWorld", "1234567890", "Aspose.BarCode", "DataMatrix" };
            string sampleJson = JsonSerializer.Serialize(sampleData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, sampleJson);
            Console.WriteLine($"Sample JSON created at: {jsonPath}");
        }

        // Read the JSON content and deserialize it into a list of strings
        List<string> codeTexts;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            codeTexts = JsonSerializer.Deserialize<List<string>>(jsonContent);
            if (codeTexts == null)
                throw new ArgumentException("JSON does not contain a valid array of strings.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read or parse JSON file: {ex.Message}");
            return;
        }

        // Limit the batch size to a safe maximum (e.g., 10 items) to avoid excessive processing
        int maxItems = Math.Min(codeTexts.Count, 10);
        for (int i = 0; i < maxItems; i++)
        {
            // Retrieve the text for the current barcode; use empty string if null
            string text = codeTexts[i] ?? string.Empty;
            string fileName = $"barcode_{i + 1}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Create a BarcodeGenerator for DataMatrix with the specified text
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
                {
                    // Optional: adjust the module (pixel) size for better readability
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode {i + 1}: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for item {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine($"Batch processing completed. Barcodes saved to: {outputFolder}");
    }
}