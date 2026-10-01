// Title: Retrieve PDF417 Macro Fields from a Generated Barcode
// Description: Demonstrates generating a PDF417 barcode with macro fields and reading those macro values from the scanned image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a PDF417 barcode with macro information (file ID, segment ID, segment count) and BarCodeReader to decode the barcode and access extended PDF417 macro fields. Developers working with bulk data encoding, document tracking, or segmented barcode workflows can learn the key API classes (BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType) and typical usage patterns for generating and reading macro-enabled PDF417 symbols.
// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
// Tags: pdf417, macro, barcode, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a PDF417 barcode with macro fields,
/// then reads and displays those macro values using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, reads macro data, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417MacroDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "macro.png");

        // Generate a PDF417 barcode with macro fields
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "DemoData"))
        {
            // Set macro properties (file ID, segment ID, total segments)
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3;

            // Save the barcode image to the temporary location
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode and extract macro fields
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Pdf417))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text content
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Access extended PDF417 information containing macro fields
                var pdf417Ext = result.Extended.Pdf417;
                Console.WriteLine($"Macro File ID: {pdf417Ext.MacroPdf417FileID}");
                Console.WriteLine($"Macro Segment ID: {pdf417Ext.MacroPdf417SegmentID}");
                Console.WriteLine($"Macro Segments Count: {pdf417Ext.MacroPdf417SegmentsCount}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}