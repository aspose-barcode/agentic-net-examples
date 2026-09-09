// Title: Extract PDF417 Macro Structured-Append Sequence and Total Count
// Description: Demonstrates generating multi‑segment PDF417 macro barcodes and reading their structured‑append metadata (segment number and total segment count). Useful for assembling split data across several barcode symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 macro (structured‑append) operations. It showcases the use of BarcodeGenerator for creating MacroPdf417 symbols and BarCodeReader for extracting extended PDF417 information such as MacroPdf417FileID, SegmentID, and SegmentsCount. Developers working with large data payloads that need to be split across multiple barcodes can refer to this pattern for encoding and decoding macro PDF417 sequences.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, macro, structured-append, barcode-generation, barcode-recognition, aspnet, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading PDF417 macro barcodes to extract structured‑append sequence information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of PDF417 macro barcodes, reads them, and prints segment metadata.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417Macro_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        int totalSegments = 3;          // Number of macro segments to generate
        int macroFileId = 12345678;     // Identifier shared by all segments
        List<string> barcodeFiles = new List<string>();

        // Generate PDF417 macro barcodes with segment IDs
        for (int segmentId = 0; segmentId < totalSegments; segmentId++)
        {
            string filePath = Path.Combine(tempFolder, $"pdf417_{segmentId}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "Sample Text"))
            {
                // Configure barcode appearance and macro properties
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                generator.Parameters.Barcode.Pdf417.Columns = 4;
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = macroFileId;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = segmentId;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = totalSegments;

                // Save the generated barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Read each barcode and output structured-append information
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize reader for MacroPdf417 type
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.MacroPdf417))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  MacroFileID: {result.Extended.Pdf417.MacroPdf417FileID}");
                        Console.WriteLine($"  SegmentID (Sequence Number): {result.Extended.Pdf417.MacroPdf417SegmentID}");
                        Console.WriteLine($"  SegmentsCount (Total Count): {result.Extended.Pdf417.MacroPdf417SegmentsCount}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read barcode from {file}: {ex.Message}");
            }
        }

        // Cleanup: optional removal of temporary files
        // Directory.Delete(tempFolder, true);
    }
}