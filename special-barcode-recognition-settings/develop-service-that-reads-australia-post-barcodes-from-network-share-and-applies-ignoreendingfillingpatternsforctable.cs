// Title: Read Australia Post barcodes with CTable settings from a network share
// Description: Demonstrates generating Australia Post barcodes using CTable encoding, then reading them with IgnoreEndingFillingPatternsForCTable enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Australia Post barcodes, and BarCodeReader for decoding them with specific CustomerInformationInterpretingType settings. Developers working with postal barcode standards often need to control encoding tables and ignore filler patterns; this snippet provides a concise reference for those scenarios.
// Prompt: Develop a service that reads Australia Post barcodes from a network share and applies IgnoreEndingFillingPatternsForCTable.
// Tags: australia post, barcode generation, barcode recognition, ctable, ignoreendingfillingpatterns, aspnet, aspnetcore, aspnetmvc, aspnetwebapi

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading Australia Post barcodes using CTable encoding
/// with the <c>IgnoreEndingFillingPatternsForCTable</c> option enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them with specific
    /// settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "AusPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode data (CTable encoding)
        var samples = new List<string>
        {
            "6201234567ABCD",   // FCC+DPID + 4 letters (valid for CTable)
            "6201234567EFGH",
            "6201234567IJKL"
        };

        var generatedFiles = new List<string>();

        // Generate barcode images for each sample string
        foreach (var codeText in samples)
        {
            string filePath = Path.Combine(tempFolder, codeText + ".png");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Use CTable encoding for Australia Post barcodes
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Reading Australia Post barcodes with IgnoreEndingFillingPatternsForCTable = true");

        // Read each generated barcode applying the required settings
        foreach (var file in generatedFiles)
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
                    // Configure reader to interpret CTable and ignore ending filling patterns
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // Cleanup temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}