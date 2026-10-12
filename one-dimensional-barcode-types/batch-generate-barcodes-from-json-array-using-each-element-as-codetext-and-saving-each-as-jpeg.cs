// Title: Batch generate Code128 barcodes from JSON and save as JPEG
// Description: Demonstrates how to deserialize a JSON array of strings, generate a Code128 barcode for each entry, and save the images as JPEG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating bulk barcode creation using the BarcodeGenerator class. Typical use cases include processing lists of identifiers, product codes, or any textual data to produce printable barcode images. Developers often need to automate batch generation, customize output formats, and manage file storage, which this snippet showcases.
// Prompt: Batch generate barcodes from a JSON array, using each element as CodeText and saving each as JPEG.
// Tags: barcode symbology, batch generation, jpeg output, aspose.barcode, json, code128

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from a JSON array and saving them as JPEG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Deserializes JSON, creates barcodes, and writes JPEG files to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing the text for each barcode
        string json = "[\"12345\",\"ABCDEF\",\"HelloWorld\"]";

        // Deserialize the JSON into a list of strings
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

        // Create a unique temporary folder for the output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each code text and generate a barcode image
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string code = codeTexts[i];
            string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.jpeg");

            // Use BarcodeGenerator to create a Code128 barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                // Optional: customize generator parameters (resolution, anti-aliasing, etc.)
                // generator.Parameters.Resolution = 72f;
                // generator.Parameters.UseAntiAlias = false;

                // Save the generated barcode as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            Console.WriteLine($"Saved barcode {i + 1} to {filePath}");
        }

        Console.WriteLine($"All barcodes saved to folder: {outputFolder}");
    }
}