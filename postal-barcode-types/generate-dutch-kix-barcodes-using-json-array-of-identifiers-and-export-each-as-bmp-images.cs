// Title: Generate Dutch KIX barcodes from JSON identifiers and save as BMP files
// Description: Demonstrates how to parse a JSON array of identifiers, create Dutch KIX barcodes using Aspose.BarCode, and export each barcode as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DutchKIX. It shows typical steps such as parsing input data, configuring the generator, and saving images in a specific format—common tasks for developers needing to produce bulk barcode images for inventory, shipping, or labeling systems.
// Prompt: Generate Dutch KIX barcodes using a JSON array of identifiers and export each as BMP images.
// Tags: dutch kix, barcode generation, json parsing, bmp output, aspose.barcode, csharp

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to generate Dutch KIX barcodes from a JSON array and save them as BMP images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes based on a sample JSON array and writes them to the output folder.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used in this example).</param>
    static void Main(string[] args)
    {
        // Sample JSON array of identifiers; replace with args or file input as needed.
        string json = "[\"123456789012\", \"987654321098\", \"555555555555\"]";
        string outputFolder = "Barcodes";

        try
        {
            // Generate the barcodes and save them to the specified folder.
            GenerateDutchKixBarcodes(json, outputFolder);
            Console.WriteLine("Barcode generation completed.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses a JSON array of identifier strings, creates a Dutch KIX barcode for each, and saves the result as a BMP file.
    /// </summary>
    /// <param name="jsonArray">A JSON-formatted array containing barcode identifiers.</param>
    /// <param name="outputDirectory">The directory where BMP images will be written.</param>
    static void GenerateDutchKixBarcodes(string jsonArray, string outputDirectory)
    {
        if (string.IsNullOrWhiteSpace(jsonArray))
            throw new ArgumentException("JSON array is null or empty.");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Parse the JSON array of strings.
        using (JsonDocument doc = JsonDocument.Parse(jsonArray))
        {
            if (doc.RootElement.ValueKind != JsonValueKind.Array)
                throw new ArgumentException("Provided JSON is not an array.");

            // Iterate over each element in the JSON array.
            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                // Skip entries that are not strings.
                if (element.ValueKind != JsonValueKind.String)
                    continue;

                string identifier = element.GetString();
                if (string.IsNullOrWhiteSpace(identifier))
                    continue;

                // Build the full file path for the BMP image.
                string filePath = Path.Combine(outputDirectory, $"{identifier}.bmp");

                // Create and save the Dutch KIX barcode.
                using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, identifier))
                {
                    // Save directly as BMP using the appropriate format enum.
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }
            }
        }
    }
}