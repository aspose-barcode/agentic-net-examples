// Title: Extract PDF417 Structured‑Append Sequence Information
// Description: Demonstrates how to read multi‑segment PDF417 barcodes and retrieve the structured‑append sequence number and total segment count.
// Prompt: Extract PDF417 structured‑append sequence number and total count from multi‑segment PDF417 codes.
// Tags: pdf417, structured-append, barcode, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that extracts structured‑append information from PDF417 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads an image file, detects PDF417 barcodes, and prints their structured‑append details.
    /// </summary>
    static void Main()
    {
        // Path to the image containing PDF417 barcode segments.
        const string imagePath = "pdf417_multi.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Open the image file as a read‑only stream.
        using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        // Initialise a barcode reader configured for PDF417 symbology.
        using (var reader = new BarCodeReader(stream, DecodeType.Pdf417))
        {
            bool anyFound = false;

            // Iterate through all recognized barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Structured Append information is stored in the PDF417 extended parameters.
                var pdf417Ext = result.Extended?.Pdf417;
                if (pdf417Ext != null)
                {
                    // MacroPdf417FileID corresponds to the file identifier.
                    // MacroPdf417SegmentID corresponds to the sequence number (starts from 0).
                    // MacroPdf417SegmentsCount corresponds to the total number of segments.
                    Console.WriteLine($"Structured Append File ID   : {pdf417Ext.MacroPdf417FileID}");
                    Console.WriteLine($"Structured Append Sequence : {pdf417Ext.MacroPdf417SegmentID}");
                    Console.WriteLine($"Structured Append Total    : {pdf417Ext.MacroPdf417SegmentsCount}");
                }
                else
                {
                    Console.WriteLine("No structured-append information available for this barcode.");
                }

                // Separator for readability between barcode entries.
                Console.WriteLine(new string('-', 40));
            }

            // Inform the user if no PDF417 barcodes were detected.
            if (!anyFound)
            {
                Console.WriteLine("No PDF417 barcodes were detected in the image.");
            }
        }
    }
}