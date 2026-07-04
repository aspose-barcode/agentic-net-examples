// Title: Retrieve PDF417 Macro Fields from Scanned Image
// Description: Demonstrates how to read a PDF417 (or Macro PDF417) barcode from an image and extract macro fields such as file ID, segment ID, and segment count.
// Prompt: Retrieve PDF417 macro fields such as file ID and segment ID from a scanned document.
// Tags: pdf417, macro, barcode, extraction, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads a PDF417 (or Macro PDF417) barcode from an image
/// and prints its macro fields (file ID, segment ID, and segment count).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the scanned image containing a PDF417 (or Macro PDF417) barcode.
        string imagePath = "sample.pdf417.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for PDF417 symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Access Macro PDF417 extended parameters.
                var pdfExt = result.Extended.Pdf417;

                // Output macro fields.
                Console.WriteLine($"Macro PDF417 File ID: {pdfExt.MacroPdf417FileID}");
                Console.WriteLine($"Macro PDF417 Segment ID: {pdfExt.MacroPdf417SegmentID}");
                Console.WriteLine($"Macro PDF417 Segments Count: {pdfExt.MacroPdf417SegmentsCount}");
                Console.WriteLine();
            }
        }
    }
}