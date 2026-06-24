using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates downloading an image from a URL and decoding any barcodes it contains using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Downloads a barcode image, reads all supported barcode types, and validates the detected type against an expected value.
    /// </summary>
    static void Main()
    {
        // Expected barcode service type (symbology name). Adjust as needed.
        string expectedServiceType = "QR";

        // URL of the barcode image. Replace with a real endpoint when available.
        string imageUrl = "https://example.com/barcode.png";

        // Create HttpClient for network access.
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Download the image as a stream (synchronously for simplicity).
                using (Stream networkStream = httpClient.GetStreamAsync(imageUrl).Result)
                {
                    // Initialize the barcode reader to recognize all supported barcode types.
                    using (var reader = new BarCodeReader(networkStream, DecodeType.AllSupportedTypes))
                    {
                        // Perform barcode recognition.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Check if any barcodes were detected.
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                        }
                        else
                        {
                            // Iterate through each detected barcode.
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Decoded Text: {result.CodeText}");

                                // Verify that the detected service type matches the expected value.
                                if (string.Equals(result.CodeTypeName, expectedServiceType, StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine("Service type matches the expected value.");
                                }
                                else
                                {
                                    Console.WriteLine($"Service type does NOT match the expected value (expected: {expectedServiceType}).");
                                }
                            }
                        }
                    }
                }
            }
            // Handle network-related errors.
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error while retrieving the image: {ex.Message}");
            }
            // Handle any other unexpected errors.
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}