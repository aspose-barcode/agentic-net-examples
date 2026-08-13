// Title: Batch barcode generation from JSON array
// Description: Demonstrates how to deserialize a JSON array of strings and generate a separate Code128 barcode image for each entry, saving them as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating bulk barcode creation using the BarcodeGenerator class. It shows typical use cases such as processing data-driven lists, exporting barcodes for inventory or labeling, and handling file output. Developers working with batch barcode generation often need to parse input data (e.g., JSON, CSV) and produce image files in formats like JPEG, PNG, or BMP.
// Prompt: Batch generate barcodes from a JSON array, using each element as CodeText and saving each as JPEG.
// Tags: barcode generation, json, batch processing, code128, jpeg, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that reads a JSON array of strings, creates a Code128 barcode for each string,
/// and saves the barcodes as JPEG images in an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs JSON deserialization, barcode generation, and file saving.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing code texts
        string json = @"[ ""ABC123"", ""XYZ789"", ""HELLO"", ""WORLD"", ""CODE5"" ]";

        // Deserialize the JSON array into a string[]
        string[] codeTexts = JsonSerializer.Deserialize<string[]>(json);

        // Ensure the output folder exists (creates it if missing)
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Choose a barcode symbology (e.g., Code128)
        BaseEncodeType barcodeType = EncodeTypes.Code128;

        // Iterate over each code text and generate a corresponding JPEG barcode
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string text = codeTexts[i];
            string fileName = $"barcode_{i + 1}.jpg";
            string filePath = Path.Combine(outputFolder, fileName);

            // Create a BarcodeGenerator for the selected symbology
            using (var generator = new BarcodeGenerator(barcodeType))
            {
                // Assign the text to be encoded
                generator.CodeText = text;

                // Save the barcode directly as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }

        // Indicate completion (no waiting for user input)
        Console.WriteLine($"Generated {codeTexts.Length} barcode images in '{outputFolder}'.");
    }
}