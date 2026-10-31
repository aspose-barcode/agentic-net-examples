// Title: Extract PDF417 Structured‑Append Sequence Number and Total Count
// Description: Demonstrates generating multi‑segment PDF417 macro barcodes and reading their structured‑append sequence number and total segment count.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on MacroPdf417 (structured‑append) operations. It showcases the use of BarcodeGenerator for creating PDF417 macro symbols and BarCodeReader for extracting extended PDF417 metadata such as segment ID and segment count. Developers working with large data payloads that need to be split across multiple PDF417 symbols will find this pattern useful for assembling and validating the complete data set.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, macro, structured-append, barcode, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a multi‑segment PDF417 macro barcode, reads each segment,
/// and displays the structured‑append sequence number and total segment count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images,
    /// extracts macro metadata, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417MacroDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define common macro metadata used for all segments.
        int fileId = 987654;               // Identifier shared across all macro segments.
        int totalSegments = 2;             // Total number of segments in the macro sequence.
        string codeText = "Sample PDF417 Macro Data";

        // Generate each macro segment and save it as a PNG image.
        for (int segment = 0; segment < totalSegments; segment++)
        {
            string filePath = Path.Combine(tempFolder, $"Pdf417Segment_{segment}.png");

            // Configure the barcode generator for MacroPdf417 and set macro properties.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, codeText))
            {
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = fileId;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = segment;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = totalSegments;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read the generated barcode and output its macro metadata.
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.MacroPdf417))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Segment ID (Sequence Number): {result.Extended.Pdf417.MacroPdf417SegmentID}");
                    Console.WriteLine($"  Total Segments (Count): {result.Extended.Pdf417.MacroPdf417SegmentsCount}");
                }
            }
        }

        // Optional cleanup: delete the temporary folder and its contents.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}