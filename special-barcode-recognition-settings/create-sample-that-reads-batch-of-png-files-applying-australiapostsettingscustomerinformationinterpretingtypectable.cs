// Title: Read batch of PNG barcodes with AustraliaPost CTable interpretation
// Description: Demonstrates generating and reading multiple PNG images using AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It shows how to configure the AustralianPostEncodingTable for generation and the CustomerInformationInterpretingType for recognition using BarcodeGenerator, BarCodeReader, and related settings. Developers often need to process batches of barcodes with specific encoding tables, making this pattern useful for bulk operations.
// Prompt: Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInformationInterpretingType.CTable.
// Tags: barcode symbology, australia post, ctable, batch processing, png, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates and reads a batch of PNG barcodes using Australia Post CTable interpretation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample Australia Post barcodes, saves them as PNG, then reads them applying CTable interpretation.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for sample barcode images
        string folder = Path.Combine(Path.GetTempPath(), "AustraliaPostBarcodes");
        Directory.CreateDirectory(folder);

        // Sample Australia Post code texts (must satisfy CTable rules)
        string[] sampleCodes = new string[]
        {
            "5912345678AB",
            "5912345678CD",
            "5912345678EF"
        };

        // Generate PNG files for the sample codes
        foreach (string code in sampleCodes)
        {
            string filePath = Path.Combine(folder, $"{code}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                // Apply CTable interpreting type for generation
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read all PNG files in the folder (limit to 5 files for safety)
        string[] pngFiles = Directory.GetFiles(folder, "*.png");
        int maxFiles = Math.Min(pngFiles.Length, 5);
        Console.WriteLine($"Reading up to {maxFiles} barcode images from '{folder}':");

        for (int i = 0; i < maxFiles; i++)
        {
            string file = pngFiles[i];
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AustraliaPost))
            {
                // Apply CTable interpreting type for recognition
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                // Optional: set a quality preset
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"  Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                }
            }
        }

        // Cleanup: optionally delete the temporary files
        // foreach (var file in pngFiles) File.Delete(file);
    }
}