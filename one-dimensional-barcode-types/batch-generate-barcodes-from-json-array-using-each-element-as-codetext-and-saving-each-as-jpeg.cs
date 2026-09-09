// Title: Batch barcode generation from JSON array
// Description: Demonstrates parsing a JSON array of strings and generating a Code128 barcode for each element, saving each barcode as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes to create barcodes in bulk. Typical use cases include creating product labels, inventory tags, or QR codes from data sources such as JSON, CSV, or databases. Developers often need to automate barcode creation, customize image parameters, and store results in a file system.
// Prompt: Batch generate barcodes from a JSON array, using each element as CodeText and saving each as JPEG.
// Tags: code128, barcode generation, json, jpeg, aspose.barcode, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that reads a JSON array of strings, generates a Code128 barcode for each string,
/// and saves the resulting images as JPEG files in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs JSON deserialization, barcode generation, and file output.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing the text for each barcode
        string json = "[\"ABC123\",\"XYZ789\",\"HELLO\",\"WORLD\",\"12345\"]";

        // Deserialize the JSON into a List<string>
        List<string> codeTexts;
        try
        {
            codeTexts = JsonSerializer.Deserialize<List<string>>(json);
            if (codeTexts == null)
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

        // Create a unique temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate over each code text, generate a barcode, and save it as a JPEG file
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string code = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.jpg");

            // Initialize the barcode generator with Code128 symbology and the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                // Optional: configure image resolution and anti-aliasing settings
                generator.Parameters.Resolution = 72f;
                generator.Parameters.UseAntiAlias = false;

                // Save the generated barcode image as JPEG
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Generated barcode {i + 1}: {filePath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}