// Title: Read batch of TIFF images with Australia Post NTable interpretation
// Description: Demonstrates how to generate Australia Post barcodes saved as TIFF files and then read them using the NTable customer information interpreting type.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the AustraliaPostSettings.CustomerInformationInterpretingType property to control how customer information is interpreted. Typical use cases include batch processing of shipping labels or postal barcodes where specific interpreting tables (e.g., NTable) are required. Developers often need to generate barcode images in various formats and then read them back for validation or data extraction.
// Prompt: Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerInformationInterpretingType.NTable.
// Tags: barcode, australia post, ntable, tiff, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates Australia Post barcodes as TIFF files,
/// then reads them back applying the NTable customer information interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates sample TIFF images if they do not exist,
    /// then iterates through each file, reading barcodes with NTable interpretation.
    /// </summary>
    static void Main()
    {
        // Define the folder that will contain the sample TIFF images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        if (!Directory.Exists(inputFolder))
        {
            // Create the folder when it does not exist
            Directory.CreateDirectory(inputFolder);
        }

        // Sample data to encode into Australia Post barcodes
        string[] sampleTexts = new[] { "1100000000", "4501234567", "5901234567" };

        // Generate TIFF images for each sample text if they are missing
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(inputFolder, $"sample{i + 1}.tif");
            if (!File.Exists(filePath))
            {
                GenerateAustraliaPostTiff(sampleTexts[i], filePath);
            }
        }

        // Retrieve all TIFF files from the input folder
        string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (tiffFiles.Length == 0)
        {
            Console.WriteLine("No TIFF files found in the input folder.");
            return;
        }

        // Process each TIFF file individually
        foreach (string file in tiffFiles)
        {
            Console.WriteLine($"Processing file: {Path.GetFileName(file)}");
            using (var reader = new BarCodeReader(file, DecodeType.AustraliaPost))
            {
                // Set the interpreting type to NTable for customer information
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                // Read all barcodes present in the image
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcodes detected.");
                }
                else
                {
                    // Output details of each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeType}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }

    // Generates an Australia Post barcode image saved as a TIFF file
    static void GenerateAustraliaPostTiff(string codeText, string filePath)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the generator to use the NTable interpreting type
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Create the barcode image in memory
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to the specified file path in TIFF format
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    bitmap.Save(stream, ImageFormat.Tiff);
                }
            }
        }
    }
}