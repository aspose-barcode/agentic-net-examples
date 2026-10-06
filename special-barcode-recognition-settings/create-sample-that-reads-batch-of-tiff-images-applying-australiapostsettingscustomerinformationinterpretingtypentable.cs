// Title: Read batch of TIFF images with Australia Post NTable interpreting
// Description: Demonstrates generating TIFF barcode images for Australia Post and reading them back using the NTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create Australia Post barcodes, save them as TIFF files, and then use BarCodeReader with AustraliaPostSettings.CustomerInformationInterpretingType set to NTable to decode the barcodes. Developers working with postal barcode standards often need to configure interpreting types for accurate data extraction, and this snippet illustrates the typical workflow.
// Prompt: Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerInformationInterpretingType.NTable.
// Tags: australia post, barcode generation, barcode recognition, tiff, ntable, customerinformationinterpretingtype, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a batch of Australia Post barcodes as TIFF images,
/// then reads them back applying the NTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates barcode images, reads them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated TIFF files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchTiff_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode data to encode
        var barcodeData = new List<string>
        {
            "620123456701234",
            "620123456702345",
            "620123456703456"
        };

        // Generate TIFF images for each barcode entry
        var tiffFiles = new List<string>();
        int index = 1;
        foreach (string data in barcodeData)
        {
            string filePath = Path.Combine(tempFolder, $"AustraliaPost_{index}.tiff");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, data))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                // Set encoding table to NTable for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
                // Save the barcode as a TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
            tiffFiles.Add(filePath);
            index++;
        }

        // Read each generated TIFF image applying the NTable interpreting type
        foreach (string file in tiffFiles)
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
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Skip files that cannot be loaded as images
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
        }

        // Cleanup temporary folder and generated files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}