// Title: Read a batch of TIFF images with Australia Post NTable interpretation
// Description: Demonstrates generating Australia Post barcodes in TIFF format and then reading them while applying the NTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator class for creating barcodes, the BarCodeReader class for decoding them, and the AustraliaPostSettings.CustomerInformationInterpretingType enumeration for controlling how customer information is interpreted. Typical use cases include bulk processing of postal barcodes, automated verification of Australia Post items, and integration into logistics workflows where NTable interpretation is required.
// Prompt: Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerInformationInterpretingType.NTable.
// Tags: barcode, australia post, tiff, batch, generation, recognition, ntable, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a batch of Australia Post barcodes in TIFF format
/// and reads them back using the NTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates barcode images, reads them with NTable settings,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the sample files
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample Australia Post barcode texts (FCC=62, NTable customer info = digits)
        var codeTexts = new List<string>
        {
            "62012345670123", // FCC 62 + DPID 01234567 + customer info 0123
            "62012345670234",
            "62012345670345"
        };

        var generatedFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images in TIFF format using NTable encoding
        // -----------------------------------------------------------------
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string filePath = Path.Combine(batchFolder, $"AustraliaPost_{i}.tif");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeTexts[i]))
            {
                // Set the customer information interpreting type to NTable for generation
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
                // Save the barcode as a TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            generatedFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Read each generated TIFF image applying NTable interpreting type
        // -----------------------------------------------------------------
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Configure the reader to interpret customer information using NTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                    // Iterate over all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle cases where the image cannot be loaded (e.g., corrupted file)
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues during processing
                Console.WriteLine($"Error processing file {file}: {ex.Message}");
            }
        }

        // Cleanup (optional). Uncomment the line below to delete the temporary folder after execution.
        // Directory.Delete(batchFolder, true);
    }
}