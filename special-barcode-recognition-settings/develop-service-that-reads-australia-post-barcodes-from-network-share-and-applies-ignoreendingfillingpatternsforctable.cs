// Title: Read Australia Post barcodes with CTable settings and ignore ending filling patterns
// Description: Demonstrates generating sample Australia Post barcodes, then reading them while applying the IgnoreEndingFillingPatternsForCTable option to correctly interpret CTable customer information.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Australia Post symbols, BarCodeReader with DecodeType.AustraliaPost for decoding, and specific BarcodeSettings such as CustomerInformationInterpretingType and IgnoreEndingFillingPatternsForCTable. Developers commonly use these APIs to process postal barcodes in batch or service scenarios, handling customer data extraction and validation.
// Prompt: Develop a service that reads Australia Post barcodes from a network share and applies IgnoreEndingFillingPatternsForCTable.
// Tags: australia post, barcode reading, ctable, ignore ending filling patterns, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample Australia Post barcodes,
/// reads them using Aspose.BarCode with CTable settings, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, reads them with specific decoding options, and removes temporary data.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Australia Post barcodes and collect file paths
        List<string> barcodeFiles = GenerateSampleBarcodes(tempFolder);

        // Iterate over each generated file and decode it using the CTable settings
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                continue;
            }

            try
            {
                // Initialise the reader for Australia Post symbology
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
                {
                    // Configure decoding to interpret customer information as CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    // Instruct the reader to ignore ending filling patterns for CTable data
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Process all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle cases where the image cannot be loaded (e.g., corrupted file)
                Console.WriteLine($"Skipped unreadable file: {filePath} ({ex.Message})");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors during processing
                Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
            }
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., files in use)
        }
    }

    /// <summary>
    /// Generates a set of sample Australia Post barcode images in the specified folder.
    /// </summary>
    /// <param name="folderPath">The directory where barcode images will be saved.</param>
    /// <returns>A list of full file paths to the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folderPath)
    {
        var files = new List<string>();

        // Sample code texts (FCC + DPID + optional customer info) valid for CTable
        string[] codeTexts = new string[]
        {
            "6201234567ABCD",
            "6201234567XYZ12",
            "6201234567HELLO"
        };

        foreach (string codeText in codeTexts)
        {
            // Create a unique file name for each barcode image
            string fileName = $"AustraliaPost_{Guid.NewGuid().ToString("N")}.png";
            string filePath = Path.Combine(folderPath, fileName);

            // Generate the barcode image using the Australia Post symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Set the encoding table to CTable for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }

        return files;
    }
}