// Title: Batch generation of DataMatrix barcodes from a JSON array
// Description: Demonstrates reading a JSON file that contains an array of strings and creating a DataMatrix barcode image for each string, saving the images to a specified or temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on batch processing of barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce DataMatrix symbols. Typical scenarios include bulk creation of inventory tags, product labeling, or QR‑style data encoding where many barcodes must be generated programmatically from structured data sources such as JSON.
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
/// Provides a console application that reads a JSON array of strings and generates a DataMatrix barcode
/// image for each entry, storing the results in a user‑specified or temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Parses command‑line arguments, reads the JSON input,
    /// creates the output folder, and generates DataMatrix barcodes for each string in the array.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// <list type="bullet">
    ///   <item><description>args[0] – Path to the JSON file (default: "data.json").</description></item>
    ///   <item><description>args[1] – Output folder path (default: a temporary folder).</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // Resolve JSON input path and output folder from command‑line arguments
        string jsonPath = args.Length > 0 ? args[0] : "data.json";
        string outputFolder = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetTempPath(), "DataMatrixBatch_" + Guid.NewGuid().ToString("N"));

        // Verify that the JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read the entire JSON file content
        string jsonContent;
        try
        {
            jsonContent = File.ReadAllText(jsonPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            return;
        }

        // Deserialize the JSON array into a list of strings
        List<string> codeTexts;
        try
        {
            codeTexts = JsonSerializer.Deserialize<List<string>>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON array: {ex.Message}");
            return;
        }

        // Ensure the deserialization produced a valid list
        if (codeTexts == null)
        {
            Console.WriteLine("JSON does not contain a valid array of strings.");
            return;
        }

        // Create the output directory (if it does not already exist)
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create output folder: {ex.Message}");
            return;
        }

        // Iterate over each text value and generate a corresponding DataMatrix barcode
        int index = 1;
        foreach (string rawText in codeTexts)
        {
            // Guard against null entries in the JSON array
            string text = rawText ?? string.Empty;
            string fileName = $"DataMatrix_{index}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Configure and generate the barcode image
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Set the module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved barcode {index} to {filePath}");
            index++;
        }

        // Inform the user where all generated barcodes are located
        Console.WriteLine($"All barcodes have been saved to: {outputFolder}");
    }
}