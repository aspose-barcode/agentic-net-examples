// Title: Read batch of TIFF images with Australia Post NTable interpretation
// Description: Demonstrates generating Australia Post barcodes in TIFF format and reading them using the NTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create barcodes, save them as TIFF images, and then employ BarCodeReader with specific AustraliaPostSettings to decode the barcodes. Typical use cases include bulk processing of shipping labels or postal documents where the NTable interpreting type is required. Developers often need to handle batch image generation, custom encoding tables, and robust error handling during barcode extraction.
// Prompt: Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerInformationInterpretingType.NTable.
// Tags: barcode, australia post, tiff, batch, generation, recognition, ntable, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a set of Australia Post barcodes saved as TIFF files,
/// then reads them back using the NTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates barcode images, reads them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample TIFF images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BatchTiff_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Australia Post barcode TIFF images
        List<string> tiffFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            // Build file path and sample code text for each image
            string filePath = Path.Combine(tempFolder, $"AustraliaPost_{i}.tiff");
            string codeText = $"6201234567{i:D4}"; // sample code text

            // Create barcode generator with Australia Post symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Configure visual appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Set encoding table to NTable for customer information
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

                // Save the barcode as a TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }

            tiffFiles.Add(filePath);
        }

        // Read barcodes from the generated TIFF images with NTable interpreting type
        foreach (string file in tiffFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize reader for Australia Post barcode type
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AustraliaPost))
                {
                    // Apply NTable interpreting type for customer information
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle unreadable or unsupported image files gracefully
                Console.WriteLine($"Skipping unreadable file: {file}. Reason: {ex.Message}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            foreach (string file in tiffFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}