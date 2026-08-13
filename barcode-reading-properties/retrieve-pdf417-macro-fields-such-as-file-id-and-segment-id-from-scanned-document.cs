// Title: Retrieve Macro PDF417 fields from a barcode image
// Description: Demonstrates how to generate a Macro PDF417 barcode, save it, and then read macro fields such as file ID and segment ID from the scanned image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 macro symbology. It showcases the use of BarcodeGenerator for creating MacroPdf417 barcodes and BarCodeReader with DecodeType.MacroPdf417 for extracting extended macro parameters. Developers working with document scanning, batch processing, or secure data encoding often need to retrieve macro information to reconstruct multi‑part barcode data.
// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
// Tags: pdf417, macro, barcode generation, barcode recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Macro PDF417 barcode (if missing) and reads its macro fields
/// such as File ID, Segment ID, and Segments Count from the scanned image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles barcode creation, file validation, and macro field extraction.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image
        string imagePath = "macropdf417.png";

        // ------------------------------------------------------------
        // Create a sample Macro PDF417 barcode if the image does not exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
            {
                // Set macro-specific fields required for reconstruction
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 123;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3;

                // Save the generated barcode image to disk
                generator.Save(imagePath);
                Console.WriteLine($"Generated sample barcode at '{Path.GetFullPath(imagePath)}'.");
            }
        }

        // ------------------------------------------------------------
        // Verify that the barcode image exists before attempting to read it
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File '{imagePath}' not found.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode and extract macro information using BarCodeReader
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.MacroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Basic barcode details
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Access extended PDF417 macro parameters, if present
                var pdf417Ext = result.Extended?.Pdf417;
                if (pdf417Ext != null)
                {
                    Console.WriteLine($"Macro PDF417 File ID: {pdf417Ext.MacroPdf417FileID}");
                    Console.WriteLine($"Macro PDF417 Segment ID: {pdf417Ext.MacroPdf417SegmentID}");
                    Console.WriteLine($"Macro PDF417 Segments Count: {pdf417Ext.MacroPdf417SegmentsCount}");
                }
                else
                {
                    Console.WriteLine("No Macro PDF417 extended parameters found.");
                }
            }
        }
    }
}