// Title: Generate Dutch KIX barcodes from JSON identifiers and save as BMP
// Description: This example reads a JSON array of identifier strings, creates Dutch KIX barcodes for each, and saves them as BMP image files in a temporary folder.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.DutchKIX. Typical scenarios include batch creation of barcode images from data sources such as JSON, XML, or databases, and exporting them to common image formats for printing or digital use. Developers working with barcode symbologies often need to configure dimensions, output paths, and handle input parsing, which this example illustrates.
// Prompt: Generate Dutch KIX barcodes using a JSON array of identifiers and export each as BMP images.
// Tags: dutchkix, barcode, generation, bmp, json, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates Dutch KIX barcodes from a JSON array and saves them as BMP files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample JSON array containing the identifiers to encode as barcodes
        string json = "[\"123456ASPOSE\",\"9876543210\",\"ABCDEF\"]";

        // Parse the JSON string into a string array; handle any parsing errors gracefully
        string[] identifiers;
        try
        {
            identifiers = JsonSerializer.Deserialize<string[]>(json);
            if (identifiers == null)
                throw new ArgumentException("JSON does not contain a valid array.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Create a unique temporary output folder for the generated BMP images
        string outputFolder = Path.Combine(Path.GetTempPath(), "DutchKIX_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each identifier, generate a barcode, and save it as a BMP file
        for (int i = 0; i < identifiers.Length; i++)
        {
            string codeText = identifiers[i];
            string filePath = Path.Combine(outputFolder, $"DutchKIX_{i}.bmp");

            // Initialize the barcode generator with Dutch KIX symbology and the current identifier
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
            {
                // Optional: configure barcode dimensions (pixel size and height)
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Save the generated barcode image as BMP to the specified file path
                generator.Save(filePath, BarCodeImageFormat.Bmp);
            }

            Console.WriteLine($"Generated barcode for '{codeText}' at: {filePath}");
        }

        Console.WriteLine("All barcodes have been generated.");
    }
}