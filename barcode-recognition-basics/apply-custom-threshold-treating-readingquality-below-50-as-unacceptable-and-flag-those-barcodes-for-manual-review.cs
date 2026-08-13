// Title: Barcode Generation, Recognition, and Quality Evaluation
// Description: Generates Code128 barcodes, reads them, and evaluates reading quality, flagging low-quality scans for manual review.
// Category-Description: This example demonstrates core Aspose.BarCode operations: barcode generation (BarcodeGenerator) and recognition (BarCodeReader). It shows how to configure barcode parameters, save to a stream, decode, and assess the ReadingQuality metric. Developers often need to automate barcode validation pipelines and identify scans that fall below acceptable quality thresholds.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: code128, barcode, generation, recognition, quality, readingquality, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes, reading them from memory,
/// and evaluating their <c>ReadingQuality</c> to flag low‑quality results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them,
    /// and outputs quality assessment messages.
    /// </summary>
    static void Main()
    {
        // Sample barcode texts to process
        string[] codeTexts = { "12345", "ABCDE", "LOWQ" };

        // Iterate over each sample text
        foreach (string code in codeTexts)
        {
            // Create a barcode generator for Code128 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code))
            {
                // Optional: adjust the module (X) size for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Store the generated barcode image in a memory stream
                using (var ms = new MemoryStream())
                {
                    // Save the barcode as a PNG image into the stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Initialize a barcode reader for Code128 using the same stream
                    using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                    {
                        // Read all barcodes found in the image (should be one)
                        foreach (var result in reader.ReadBarCodes())
                        {
                            double quality = result.ReadingQuality;

                            // Apply custom quality threshold: flag if below 50
                            if (quality < 50.0)
                            {
                                Console.WriteLine($"[FLAGGED] Code '{code}' requires manual review. ReadingQuality: {quality}");
                            }
                            else
                            {
                                Console.WriteLine($"[OK] Code '{code}' recognized successfully. ReadingQuality: {quality}");
                            }
                        }
                    }
                }
            }
        }
    }
}