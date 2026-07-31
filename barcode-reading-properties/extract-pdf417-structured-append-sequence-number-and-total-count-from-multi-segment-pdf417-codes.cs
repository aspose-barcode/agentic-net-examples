// Title: Extract PDF417 Structured‑Append Sequence Number and Total Count
// Description: Demonstrates how to generate multi‑segment PDF417 barcodes with Macro PDF417 properties and read back the sequence number and total segment count.
// Category-Description: This example belongs to the Aspose.BarCode PDF417 macro (structured‑append) operations collection. It shows usage of BarcodeGenerator for creating Macro PDF417 barcodes and BarCodeReader with DecodeType.MacroPdf417 to retrieve extended PDF417 metadata such as segment ID and segment count. Developers working with large data payloads split across multiple PDF417 symbols can use these APIs to assemble the original data correctly.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, structured-append, macro, barcode-generation, barcode-recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of Macro PDF417 barcode segments and then reads each segment
/// to extract the structured‑append (Macro PDF417) sequence number and total segment count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, saves them to disk,
    /// and reads back the Macro PDF417 metadata from each image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Define Macro PDF417 parameters (same file ID for all segments)
        // --------------------------------------------------------------------
        int totalSegments = 3;          // total number of segments to generate
        int fileId = 12345;             // identifier shared by all segments

        // --------------------------------------------------------------------
        // Generate sample PDF417 segments with Macro PDF417 properties
        // --------------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            string fileName = Path.Combine(folderPath, $"segment_{i}.png");

            // Create a barcode generator for PDF417 and assign segment‑specific data
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, $"Segment_{i + 1}"))
            {
                // Set Macro PDF417 (structured‑append) properties
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = fileId;               // common file identifier
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = i;               // sequence number (0‑based)
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = totalSegments; // total segment count

                // Save the generated barcode image to disk
                generator.Save(fileName);
            }
        }

        Console.WriteLine("Reading structured‑append information from generated barcodes:");

        // --------------------------------------------------------------------
        // Read each barcode image and extract Macro PDF417 metadata
        // --------------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            string fileName = Path.Combine(folderPath, $"segment_{i}.png");

            // Verify that the image file exists before attempting to read it
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File not found: {fileName}");
                continue;
            }

            // Use BarCodeReader with DecodeType.MacroPdf417 to access extended data
            using (var reader = new BarCodeReader(fileName, DecodeType.MacroPdf417))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Extended parameters contain Macro PDF417 metadata, if present
                    var pdf417Ext = result.Extended?.Pdf417;
                    if (pdf417Ext != null)
                    {
                        int segmentId = pdf417Ext.MacroPdf417SegmentID;       // sequence number of this segment
                        int segmentsCount = pdf417Ext.MacroPdf417SegmentsCount; // total number of segments

                        Console.WriteLine($"File: {Path.GetFileName(fileName)}");
                        Console.WriteLine($"  Segment ID (sequence number): {segmentId}");
                        Console.WriteLine($"  Total Segments: {segmentsCount}");
                    }
                    else
                    {
                        Console.WriteLine($"No Macro PDF417 metadata found in {Path.GetFileName(fileName)}");
                    }
                }
            }
        }
    }
}