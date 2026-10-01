// Title: Identify QR Code Structured Append Metadata Using QrExtendedParameters
// Description: Demonstrates how to generate multi‑segment QR codes with Structured Append parameters, save them as images, and read back the Structured Append metadata using Aspose.BarCode's QrExtendedParameters.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the use of BarcodeGenerator for creating QR symbols with Structured Append settings and BarCodeReader for extracting QR extended parameters (QrExtendedParameters). Typical scenarios include splitting large payloads across several QR codes and reassembling them. Developers often need to work with these APIs to handle multi‑segment QR codes in imaging applications.
// Prompt: Identify QR Code structured‑append metadata using QrExtendedParameters for multi‑segment QR codes in images.
// Tags: qr code, structured append, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates QR code segments with Structured Append parameters,
/// saves them as PNG images, and then reads back the Structured Append metadata
/// using Aspose.BarCode's QR extended parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR code segments, reads them,
    /// displays Structured Append information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for generated QR code images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrStructuredAppend_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define sample data for two QR code segments
        // --------------------------------------------------------------------
        var segments = new List<string> { "First segment of data", "Second segment of data" };
        int totalSegments = segments.Count;
        byte parityByte = 0; // For demonstration; normally calculated automatically

        var generatedFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate QR codes with Structured Append parameters (generation side)
        // --------------------------------------------------------------------
        for (int i = 0; i < totalSegments; i++)
        {
            string codeText = segments[i];
            string filePath = Path.Combine(tempFolder, $"qr_segment_{i + 1}.png");

            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Configure Structured Append settings
                generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = totalSegments;
                generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = i + 1; // 1‑based index
                generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = parityByte;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Read each generated QR code and extract Structured Append metadata
        // --------------------------------------------------------------------
        Console.WriteLine("Reading QR code segments and extracting Structured Append metadata:");
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Verify that the detected code is a QR code
                    if (result.CodeType != DecodeType.QR)
                    {
                        Console.WriteLine($"Detected code is not QR: {result.CodeType}");
                        continue;
                    }

                    // Access QR extended parameters (reading side)
                    var qrExt = result.Extended.QR;
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Structured Append Total Segments : {qrExt.StructuredAppendModeBarCodesQuantity}");
                    Console.WriteLine($"  Segment Index (1‑based)          : {qrExt.StructuredAppendModeBarCodeIndex}");
                    Console.WriteLine($"  Parity Data                      : {qrExt.StructuredAppendModeParityData}");
                    Console.WriteLine($"  Decoded Text                     : {result.CodeText}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in generatedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}