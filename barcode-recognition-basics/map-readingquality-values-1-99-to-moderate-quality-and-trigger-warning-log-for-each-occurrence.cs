// Title: Mapping ReadingQuality to Moderate Quality with Warning Log
// Description: Demonstrates generating Code128 barcodes, reading them, and mapping ReadingQuality values 1‑99 to moderate quality, logging a warning for each occurrence.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ReadingQuality property to assess scan confidence. Developers often need to evaluate reading quality to trigger alerts or adjust processing logic in inventory, logistics, or document automation scenarios.
// Prompt: Map ReadingQuality values 1‑99 to moderate quality and trigger a warning log for each occurrence.
// Tags: code128, generation, recognition, readingquality, png, aspose.barcode, barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample Code128 barcodes, reads them back, and logs a warning when the
/// <see cref="BarCodeResult.ReadingQuality"/> falls within the moderate range (1‑99).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over sample texts, creates barcode images,
    /// decodes them, and evaluates the reading quality.
    /// </summary>
    static void Main()
    {
        // Define sample barcode texts to encode.
        string[] texts = { "12345", "ABCDEF", "9876543210" };

        // Process each text individually.
        foreach (string text in texts)
        {
            // Create an in‑memory stream to hold the generated barcode image.
            using (var ms = new MemoryStream())
            {
                // Generate a Code128 barcode and save it as PNG into the stream.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset the stream position so it can be read from the beginning.
                ms.Position = 0;

                // Initialize a barcode reader that supports all available symbologies.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Iterate over all detected barcodes in the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        double readingQuality = result.ReadingQuality;

                        // Output the decoded text and its reading quality.
                        Console.WriteLine($"Barcode Text: {result.CodeText}");
                        Console.WriteLine($"Reading Quality: {readingQuality}");

                        // Map values 1‑99 to moderate quality and log a warning.
                        if (readingQuality >= 1 && readingQuality <= 99)
                        {
                            Console.WriteLine($"Warning: ReadingQuality {readingQuality} is considered moderate.");
                        }

                        Console.WriteLine(); // Blank line for readability between results.
                    }
                }
            }
        }
    }
}