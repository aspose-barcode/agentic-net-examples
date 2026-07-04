// Title: Extract barcode metadata from a generated image (simulated live feed)
// Description: Demonstrates generating a barcode, reading its metadata, and displaying results, simulating a live camera feed scenario.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: barcode symbology, metadata extraction, console output, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode metadata extraction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, reads its metadata, and prints details to the console.
    /// </summary>
    static void Main()
    {
        // The console runner cannot access a live camera feed.
        // Instead, we generate a sample barcode image and extract its metadata.

        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image in memory.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a reader that can decode all supported barcode types.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    int processed = 0; // Counter to limit processing to the first barcode.

                    // Iterate through all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Stop after processing the first detected barcode.
                        if (processed >= 1) break;

                        // Output basic barcode information.
                        Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                        // Output the location and size of the barcode region.
                        var region = result.Region.Rectangle;
                        Console.WriteLine($"Region - X:{region.X}, Y:{region.Y}, Width:{region.Width}, Height:{region.Height}");

                        // Output the rotation angle of the barcode region.
                        Console.WriteLine($"Angle: {result.Region.Angle}");

                        processed++; // Increment the processed counter.
                    }
                }
            }
        }
    }
}