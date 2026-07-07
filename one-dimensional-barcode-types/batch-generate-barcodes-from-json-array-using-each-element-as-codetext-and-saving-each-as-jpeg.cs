// Title: Batch Barcode Generation from JSON to JPEG
// Description: Demonstrates how to read a JSON array of strings, generate a Code128 barcode for each entry, and save the images as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes to create 1D barcodes. Typical use cases include batch processing of identifiers from data sources such as JSON, CSV, or databases, and exporting them as image files for printing or digital distribution. Developers often need to automate barcode creation in bulk, customize formats, and manage output directories.
// Prompt: Batch generate barcodes from a JSON array, using each element as CodeText and saving each as JPEG.
// Tags: barcode symbology, batch generation, jpeg, aspose.barcode, json

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of batch barcode generation from a JSON array and saves each barcode as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads a JSON array, generates Code128 barcodes, and writes JPEG files.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing the code texts for the barcodes.
        string json = "[\"12345\",\"ABCDEF\",\"Hello World\",\"9876543210\",\"SampleCode\"]";

        // Deserialize the JSON array into a list of strings.
        List<string> codeTexts = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();

        // Ensure the output directory exists.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a barcode for each code text and save it as a JPEG file.
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string code = codeTexts[i];
            string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.jpeg");

            // Use Code128 as a generic 1D barcode type.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                // Save the barcode image in JPEG format.
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }

        // Indicate successful completion.
        Console.WriteLine($"Generated {codeTexts.Count} barcode images in '{outputDir}' directory.");
    }
}