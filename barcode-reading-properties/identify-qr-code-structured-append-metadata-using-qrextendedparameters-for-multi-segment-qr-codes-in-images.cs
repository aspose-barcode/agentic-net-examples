// Title: Identify QR Code Structured Append Metadata
// Description: Demonstrates generating multi‑segment QR codes with structured‑append parameters and reading back the metadata using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode QR code operations collection. It shows how to use the BarcodeGenerator class to set QR structured‑append properties and the BarCodeReader class with QrExtendedParameters to retrieve segment information. Developers working with multi‑segment QR codes for data splitting, batch processing, or enhanced error correction can use these APIs to create and decode structured‑append QR symbols.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr code, structured-append, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates QR code segments with Structured Append parameters,
/// then reads each segment to display the associated metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates QR segments, saves them,
    /// and extracts Structured Append information using QrExtendedParameters.
    /// </summary>
    static void Main()
    {
        // Define folder for generated QR code images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrSegments");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Number of QR code segments (structured append) and base text for each segment
        int segmentCount = 2;
        string baseText = "Hello from segment ";

        // --------------------------------------------------------------------
        // Generate each QR segment with Structured Append parameters
        // --------------------------------------------------------------------
        for (int i = 0; i < segmentCount; i++)
        {
            // Build file path for the current segment image
            string filePath = Path.Combine(outputFolder, $"qr_segment_{i}.png");

            // Create a QR code generator for the segment text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, baseText + (i + 1)))
            {
                // Configure Structured Append settings
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = segmentCount;      // total number of segments
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i;        // zero‑based index of this segment
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = 0;               // optional parity byte (0 = none)

                // Save the generated QR image to disk
                generator.Save(filePath);
                Console.WriteLine($"Generated QR segment {i + 1} at: {filePath}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Reading QR segments and extracting Structured Append metadata...");

        // --------------------------------------------------------------------
        // Read each generated QR image and display Structured Append metadata
        // --------------------------------------------------------------------
        foreach (string file in Directory.GetFiles(outputFolder, "*.png"))
        {
            // Initialize a QR code reader for the current image file
            using (var reader = new BarCodeReader(file, DecodeType.QR))
            {
                // Iterate through all detected barcodes (should be one per image)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access QR‑specific extended parameters
                    var qrExt = result.Extended.QR;

                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                    Console.WriteLine($"  Structured Append Quantity: {qrExt.StructuredAppendModeBarCodesQuantity}");
                    Console.WriteLine($"  Structured Append Index   : {qrExt.StructuredAppendModeBarCodeIndex}");
                    Console.WriteLine($"  Structured Append Parity  : {qrExt.StructuredAppendModeParityData}");
                    Console.WriteLine();
                }
            }
        }

        // Optional cleanup: remove generated files and folder
        // foreach (var f in Directory.GetFiles(outputFolder)) File.Delete(f);
        // Directory.Delete(outputFolder);
    }
}