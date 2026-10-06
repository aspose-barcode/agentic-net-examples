// Title: Extract PDF417 Structured-Append Sequence Number and Total Count
// Description: Demonstrates generating a multi‑segment Macro PDF417 barcode, saving each segment as an image, and reading back the structured‑append metadata (segment ID and total segment count).
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on MacroPdf417 (structured‑append) operations. It showcases the use of BarcodeGenerator for creating segmented PDF417 barcodes and BarCodeReader for extracting macro information. Developers working with large data payloads that require splitting across multiple PDF417 symbols will find this pattern useful for assembling and validating the complete data set.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, macro, structured-append, barcode, generation, recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a two‑segment Macro PDF417 barcode, saves each segment as a PNG file,
/// then reads the files to display the structured‑append sequence number and total count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, writes Macro PDF417 segments,
    /// reads the macro metadata, and cleans up the temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "Pdf417Macro_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Prepare storage for file paths and define macro parameters.
        string[] filePaths = new string[2];
        int totalSegments = 2;          // Number of macro segments to generate.
        int fileId = 123456;            // Identifier shared by all segments of the same macro barcode.

        // Generate each macro segment and save it as a PNG image.
        for (int i = 0; i < totalSegments; i++)
        {
            filePaths[i] = Path.Combine(tempDir, $"segment{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleData"))
            {
                // Set visual and macro-specific parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = fileId;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = i;
                generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = totalSegments;

                // Save the generated segment to disk.
                generator.Save(filePaths[i], BarCodeImageFormat.Png);
            }
        }

        // Read each saved image and output the macro metadata (segment ID and total count).
        Console.WriteLine("Reading Macro PDF417 metadata:");
        foreach (string path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (var reader = new BarCodeReader(path, DecodeType.MacroPdf417))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    int segmentId = result.Extended.Pdf417.MacroPdf417SegmentID;
                    int segmentsCount = result.Extended.Pdf417.MacroPdf417SegmentsCount;
                    Console.WriteLine($"File: {Path.GetFileName(path)}  SequenceNumber: {segmentId}  TotalCount: {segmentsCount}");
                }
            }
        }

        // Cleanup temporary files and directory.
        try
        {
            foreach (string file in filePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}