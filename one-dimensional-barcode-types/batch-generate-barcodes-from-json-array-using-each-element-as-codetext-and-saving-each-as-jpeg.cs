// Title: Batch generate Code128 barcodes from JSON and save as JPEG
// Description: Demonstrates how to deserialize a JSON array of strings, use each string as the CodeText for a Code128 barcode, and save the generated images as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with the BarcodeGenerator class to create barcodes programmatically. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs. Developers often need to parse input data (e.g., JSON, CSV) and produce image files in formats such as PNG, JPEG, or BMP using Aspose.BarCode APIs.
// Prompt: Batch generate barcodes from a JSON array, using each element as CodeText and saving each as JPEG.
// Tags: code128, barcode generation, json, jpeg, aspose.barcode, batch, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a JSON array of strings, generates a Code128 barcode for each string,
/// and saves the resulting images as JPEG files in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing the code texts for the barcodes.
        string json = "[\"ABC123\",\"XYZ789\",\"HelloWorld\"]";

        // Deserialize the JSON into a list of strings.
        List<string> codeTexts;
        try
        {
            codeTexts = JsonSerializer.Deserialize<List<string>>(json);
            if (codeTexts == null)
                throw new ArgumentException("JSON deserialization returned null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a unique temporary output folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate over each code text and generate a corresponding barcode image.
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.jpeg");

            try
            {
                // Initialize the barcode generator for Code128 symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, ""))
                {
                    // Assign the code text with UTF-8 encoding.
                    generator.SetCodeText(text, Encoding.UTF8);

                    // Optional configuration: suppress exceptions for invalid code text,
                    // set a modest resolution, and disable anti-aliasing to keep file size low.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                    generator.Parameters.Resolution = 72f;
                    generator.Parameters.UseAntiAlias = false;

                    // Save the generated barcode as a JPEG image.
                    generator.Save(filePath, BarCodeImageFormat.Jpeg);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for \"{text}\": {ex.Message}");
            }
        }
    }
}