// Title: Batch generation of DataMatrix barcodes from a JSON array
// Description: Demonstrates reading a JSON array of strings, generating a DataMatrix barcode for each entry using Aspose.BarCode, and saving the images to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.DataMatrix. Typical use cases include bulk barcode creation from data sources such as JSON, CSV, or databases. Developers often need to automate barcode production for inventory, shipping, or tracking systems.
// Prompt: Create a batch routine that reads a JSON array and produces DataMatrix barcodes saved to a specified folder.
// Tags: datamatrix, barcode, generation, json, batch, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of DataMatrix barcodes from a JSON array and saving them to a folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses JSON, creates output directory, generates barcodes, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Sample JSON array of strings to encode
        string json = "[\"ABC\",\"123\",\"Hello World\"]";

        // Deserialize JSON into a list of strings
        List<string> items;
        try
        {
            items = JsonSerializer.Deserialize<List<string>>(json);
            if (items == null)
            {
                Console.WriteLine("JSON deserialization returned null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a unique temporary output folder for the generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "DataMatrixBatch_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to create output folder: {ex.Message}");
            return;
        }

        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate over each item and generate a DataMatrix barcode
        for (int i = 0; i < items.Count; i++)
        {
            string text = items[i] ?? string.Empty;
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.png");

            // Initialize the barcode generator with DataMatrix symbology and the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Optional: adjust module size for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                try
                {
                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Saved: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for \"{text}\": {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}