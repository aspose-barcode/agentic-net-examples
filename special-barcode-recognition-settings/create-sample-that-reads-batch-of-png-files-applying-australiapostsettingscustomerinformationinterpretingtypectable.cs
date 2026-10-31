// Title: Read a batch of PNG barcodes using AustraliaPost CTable interpreting type
// Description: This example generates PNG images containing Australia Post barcodes encoded with the CTable customer information interpreting type, then reads them back applying the same interpreting type.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for Australia Post symbology. Uses BarcodeGenerator to create barcodes, BarCodeReader to decode them, and configures CustomerInformationInterpretingType.CTable. Ideal for developers needing batch processing of barcodes, custom encoding tables, and automated verification of generated images.
// Prompt: Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Tags: barcode, australia post, ctable, batch processing, png, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading a batch of Australia Post barcodes using the CTable interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates PNG files with Australia Post barcodes, then reads them applying CTable interpreting type.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode texts (valid for CTable)
        List<string> codeTexts = new List<string>
        {
            "6201234567ASPOSE",   // FCC 62 + customer info "ASPOSE"
            "6201234567CODE123",  // FCC 62 + customer info "CODE123"
            "6201234567TEST"      // FCC 62 + customer info "TEST"
        };

        // Generate PNG files with AustraliaPost CTable encoding
        List<string> generatedFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, text + ".png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, text))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Apply CTable encoding for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Batch generation completed. Reading barcodes with CTable interpreting type:");

        // Read each generated PNG applying CTable interpreting type
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
                    // Configure the reader to interpret customer information using CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Iterate through all detected barcodes in the image
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

        // Cleanup: optionally delete the temporary folder and its contents
        // Directory.Delete(batchFolder, true);
    }
}