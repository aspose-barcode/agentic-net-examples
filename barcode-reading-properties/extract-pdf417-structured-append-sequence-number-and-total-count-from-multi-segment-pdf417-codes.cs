// Title: Extract PDF417 Structured‑Append Sequence Number and Total Count
// Description: Demonstrates how to generate multi‑segment PDF417 barcodes with macro (structured‑append) properties and then read each segment to obtain its sequence number and total segment count.
// Category-Description: This example belongs to the Aspose.BarCode PDF417 macro (structured‑append) operations collection. It shows usage of BarcodeGenerator for creating macro PDF417 symbols and BarCodeReader for decoding them, covering typical scenarios such as splitting large data across multiple barcodes and retrieving segment metadata. Developers working with PDF417 macro barcodes can use this pattern to assemble or disassemble multi‑segment codes.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, structured-append, macro, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading PDF417 macro (structured‑append) barcodes to extract segment information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates temporary PDF417 macro images, reads them, and prints each segment's index and total count.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417Macro_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data for two structured‑append segments
        string[] segmentTexts = { "Segment0", "Segment1" };
        int fileId = 1;               // Identifier for the whole macro PDF417
        int totalSegments = segmentTexts.Length;

        // Generate each PDF417 segment with macro properties
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(tempFolder, $"pdf417_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, segmentTexts[i]))
            {
                // Set macro (structured‑append) properties
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = fileId;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = i;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = totalSegments;

                // Save the barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read each generated barcode and extract sequence number and total count
        Console.WriteLine("Extracted PDF417 Structured‑Append Information:");
        for (int i = 0; i < totalSegments; i++)
        {
            string filePath = Path.Combine(tempFolder, $"pdf417_{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Use the PDF417 decode type
            BaseDecodeType decodeType = DecodeType.Pdf417;
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(filePath)}");
                    continue;
                }

                // Assuming one barcode per image
                var result = results[0];
                int segmentId = result.Extended.Pdf417.MacroPdf417SegmentID;
                int segmentsCount = result.Extended.Pdf417.MacroPdf417SegmentsCount;

                Console.WriteLine($"{Path.GetFileName(filePath)}: Segment {segmentId + 1} of {segmentsCount}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the temp folder will be removed by the OS eventually
        }
    }
}