// Title: Decode Barcode from Network Stream and Verify Type
// Description: Demonstrates loading a barcode image via HTTP, decoding it with Aspose.BarCode, and checking that the detected symbology matches an expected value.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeReader with a stream to recognize any supported symbology. Typical use cases include processing images received from web services or APIs, where developers need to decode barcodes on the fly and validate their type. The example showcases key classes such as BarCodeReader, DecodeType, and BarCodeResult, which are commonly used for batch scanning, real‑time decoding, and verification scenarios.
// Prompt: Load a barcode image from a network stream, decode it, and verify service type matches expected value.
// Tags: barcode, decoding, verification, network, http, aspose.barcode, barcodereader, decode type, qr

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program that downloads a barcode image, decodes it, and verifies the symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Downloads the image, decodes barcodes, and checks against expected type.
    /// </summary>
    static void Main()
    {
        // Define the barcode image URL and the expected symbology name
        string imageUrl = "https://example.com/sample-barcode.png";
        string expectedTypeName = "QR";

        // Download the image into a memory stream for processing
        MemoryStream imageStream;
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Retrieve the remote image as a stream
                using (Stream networkStream = httpClient.GetStreamAsync(imageUrl).GetAwaiter().GetResult())
                {
                    imageStream = new MemoryStream();
                    networkStream.CopyTo(imageStream);
                    // Reset position to the beginning before reading
                    imageStream.Position = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download image: {ex.Message}");
                return;
            }
        }

        // Initialize the barcode reader to recognize all supported types
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle case where no barcode is found
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Verify that the detected type matches the expected value
                if (string.Equals(result.CodeTypeName, expectedTypeName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Verification succeeded: barcode type matches expected value.");
                }
                else
                {
                    Console.WriteLine($"Verification failed: expected '{expectedTypeName}' but got '{result.CodeTypeName}'.");
                }
            }
        }

        // Release the memory stream resources
        imageStream.Dispose();
    }
}