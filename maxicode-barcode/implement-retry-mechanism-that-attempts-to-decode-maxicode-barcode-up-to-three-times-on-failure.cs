// Title: MaxiCode barcode generation and retry decode example
// Description: Demonstrates generating a MaxiCode barcode, saving it to a memory stream, and attempting to decode it up to three times with retry logic.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to use BarcodeGenerator, BarCodeReader, and related classes to create and read MaxiCode symbology. Developers often need to generate barcodes for packaging and then verify them by decoding, handling transient failures with retry loops. The snippet shows typical usage patterns for encoding, saving to streams, and robust decoding.
// Prompt: Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times on failure.
// Tags: maxicode, barcode generation, barcode recognition, retry, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and decoding it with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode barcode, saves it to a memory stream, and tries to decode it up to three times.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCode barcode generator with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position for reading
                ms.Position = 0;

                const int maxAttempts = 3;
                bool decoded = false;

                // Retry loop: attempt to decode up to maxAttempts times
                for (int attempt = 1; attempt <= maxAttempts && !decoded; attempt++)
                {
                    try
                    {
                        // Ensure the stream is positioned at the beginning before each decode attempt
                        ms.Position = 0;

                        // Decode the barcode from the memory stream
                        using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                        {
                            var results = reader.ReadBarCodes();

                            if (results != null && results.Length > 0)
                            {
                                // Successful decode – output details
                                var result = results[0];
                                Console.WriteLine($"Decoded on attempt {attempt}:");
                                Console.WriteLine($"  Code Type: {result.CodeType}");
                                Console.WriteLine($"  Code Text: {result.CodeText}");
                                decoded = true;
                            }
                            else
                            {
                                Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log exception and continue to next attempt
                        Console.WriteLine($"Attempt {attempt}: Exception - {ex.Message}");
                    }

                    // Optional: add a small delay here before the next attempt if needed
                }

                if (!decoded)
                {
                    Console.WriteLine("Failed to decode the MaxiCode barcode after 3 attempts.");
                }
            }
        }
    }
}