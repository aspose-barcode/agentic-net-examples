// Title: Decode a barcode from a network stream and verify its symbology
// Description: Demonstrates loading a barcode image via HTTP, decoding all supported symbologies, and checking that the detected type matches an expected value.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeReader with a Stream source. It covers typical use cases such as reading barcodes from remote files, iterating over multiple results, and performing validation logic. Developers working with barcode scanning, image processing, or integration with web services often need these patterns.
// Prompt: Load a barcode image from a network stream, decode it, and verify service type matches expected value.
// Tags: barcode, qr, decode, network, http, aspose.barcode, barcodereader, stream, validation

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a barcode image, decodes it, and verifies the detected symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Downloads the image, reads all barcodes, and checks for a matching type.
    /// </summary>
    static void Main()
    {
        // URL of the barcode image to process
        string imageUrl = "https://example.com/sample.png";

        // Expected barcode type name (e.g., "QR")
        string expectedTypeName = "QR";

        // HttpClient is used to fetch the image stream from the network
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Synchronously get the image stream (blocking call for simplicity)
                using (Stream stream = httpClient.GetStreamAsync(imageUrl).GetAwaiter().GetResult())
                {
                    // Initialize the barcode reader to decode all supported types from the stream
                    using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                    {
                        bool matchFound = false;

                        // Iterate through all detected barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");

                            // Compare the detected type with the expected value (case‑insensitive)
                            if (string.Equals(result.CodeTypeName, expectedTypeName, StringComparison.OrdinalIgnoreCase))
                            {
                                matchFound = true;
                            }
                        }

                        // Output verification result
                        if (matchFound)
                        {
                            Console.WriteLine("Service type matches expected value.");
                        }
                        else
                        {
                            Console.WriteLine("Service type does NOT match expected value.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during download or decoding
                Console.WriteLine($"Error processing barcode: {ex.Message}");
            }
        }
    }
}