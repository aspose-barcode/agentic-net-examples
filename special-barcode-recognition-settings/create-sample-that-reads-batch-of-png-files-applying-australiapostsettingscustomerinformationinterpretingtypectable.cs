// Title: Read a batch of PNG barcodes using AustraliaPost CTable interpreting type
// Description: Demonstrates generating multiple PNG images with Australia Post barcodes encoded using the CTable format, then reading them back with the appropriate interpreting settings.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator and BarCodeReader for bulk operations. It covers setting AustraliaPostSettings.CustomerInformationInterpretingType, handling PNG output, and typical use cases like validating large sets of postal barcodes. Developers often need to generate and decode many barcodes efficiently, and this snippet provides a concise reference.
// Prompt: Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Tags: barcode symbology, reading, png, barcodegenerator, barcodereader, australiapost

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a batch of Australia Post barcodes encoded with CTable,
/// saves them as PNG files, and reads them back using the appropriate interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, reads them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode texts (valid for CTable)
        List<string> codeTexts = new List<string>
        {
            "6201234567ASPO",
            "6201234567TEST",
            "6201234567CODE"
        };

        // Generate PNG files with AustraliaPost CTable encoding
        List<string> generatedFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, text + ".png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, text))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Apply CTable encoding for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Batch generation completed. Reading barcodes with CTable interpreting type...");

        // Read each generated PNG file applying CTable interpreting type
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Configure reader to interpret customer information using CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            foreach (string file in generatedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(batchFolder))
                Directory.Delete(batchFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}