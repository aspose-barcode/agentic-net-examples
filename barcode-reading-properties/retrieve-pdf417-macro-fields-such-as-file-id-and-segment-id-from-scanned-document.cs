// Title: Retrieve Macro PDF417 barcode fields from a scanned image
// Description: Demonstrates how to generate a Macro PDF417 barcode, read it, and extract macro fields such as file ID, segment ID, and segment count.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating Macro PDF417 barcodes and BarCodeReader for decoding them. Developers working with document scanning, inventory tracking, or secure data transmission often need to embed and later retrieve macro information from PDF417 symbols. The key API classes used are BarcodeGenerator, BarCodeReader, and related parameter objects.
// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
// Tags: pdf417, macro, barcode, generation, recognition, extraction, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Macro PDF417 barcode (if not already present),
/// reads it, and prints macro fields such as FileID, SegmentID, and SegmentsCount.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode, decodes it,
    /// and displays macro information on the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder to store the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417MacroDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path to the barcode image (simulating a scanned document).
        string barcodePath = Path.Combine(tempFolder, "MacroPdf417.png");

        // --------------------------------------------------------------------
        // If the barcode image does not exist, generate a sample Macro PDF417 barcode.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
            {
                // Set visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.Pdf417.Columns = 4;

                // Set macro metadata fields.
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3;

                // Save the generated barcode as a PNG file.
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image exists before attempting to read it.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode and extract macro fields using BarCodeReader.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MacroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("--- Macro PDF417 Barcode ---");
                Console.WriteLine("CodeText: " + result.CodeText);
                Console.WriteLine("MacroPdf417FileID: " + result.Extended.Pdf417.MacroPdf417FileID);
                Console.WriteLine("MacroPdf417SegmentID: " + result.Extended.Pdf417.MacroPdf417SegmentID);
                Console.WriteLine("MacroPdf417SegmentsCount: " + result.Extended.Pdf417.MacroPdf417SegmentsCount);
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and folder (optional).
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}