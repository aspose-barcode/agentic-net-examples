// Title: Read Australia Post barcodes from a network share with CTable filling pattern handling
// Description: Demonstrates how to generate Australia Post barcodes, store them in a temporary folder that simulates a network share, and read them using Aspose.BarCode with the IgnoreEndingFillingPatternsForCTable option enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create Australia Post symbols and BarCodeReader with DecodeType.AustraliaPost to decode them, configuring AustraliaPost settings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Developers working with postal barcode processing, batch scanning from shared locations, or custom decoding options will find this pattern useful.
// Prompt: Develop a service that reads Australia Post barcodes from a network share and applies IgnoreEndingFillingPatternsForCTable.
// Tags: australia post, barcode, generation, recognition, ctable, ignoreendingfillingpatterns, aspnet, aspnetcore, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading Australia Post barcodes with CTable filling pattern handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, stores them in a temporary folder, and reads them with specific decoding settings.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder to simulate a network share
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Australia Post barcodes and collect their file paths
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder);

        // Iterate over each generated barcode file
        foreach (string filePath in barcodeFiles)
        {
            // Verify the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the reader for Australia Post symbology
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
                {
                    // Configure decoding settings: use CTable interpretation and ignore ending filler patterns
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Read all barcodes found in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be processed as a barcode
                Console.WriteLine($"Failed to read '{filePath}': {ex.Message}");
            }
        }

        // Cleanup (optional). Uncomment to delete the temporary folder after execution.
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Generates sample Australia Post barcode images in the specified folder.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <returns>A list of file paths for the generated barcode images.</returns>
    private static List<string> GenerateSampleBarcodes(string folder)
    {
        var files = new List<string>();

        // Sample code texts (FCC + DPID + optional customer info)
        string[] codeTexts = new[]
        {
            "6201234567CTAB",   // CTable example
            "6201234567END",    // Ends with filler pattern
            "6201234567AB12"    // Mixed alphanumeric for CTable
        };

        // Create a barcode image for each sample text
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string codeText = codeTexts[i];
            string filePath = Path.Combine(folder, $"AustraliaPost_{i + 1}.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Use CTable encoding for the Australian Post barcode
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }

        return files;
    }
}