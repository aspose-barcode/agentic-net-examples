// Title: Decode Barcode from Network Stream and Verify Type
// Description: Demonstrates downloading a barcode image via HTTP, decoding it with Aspose.BarCode, and checking that the detected symbology matches an expected value.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, showcasing how to use BarCodeReader with DecodeType.AllSupportedTypes to read barcodes from streams. Typical scenarios include processing images received over a network, validating barcode types in automated workflows, and integrating barcode verification into web services. Developers often need to download image data, invoke the reader, and compare the resulting CodeTypeName against business rules.
// Prompt: Load a barcode image from a network stream, decode it, and verify service type matches expected value.
// Tags: barcode, decode, network, http, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a barcode image, decodes it, and validates the detected symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // URL of the barcode image to download.
        const string imageUrl = "https://example.com/barcode.png";

        // Expected barcode type name (e.g., "Code128", "QR", etc.).
        const string expectedCodeType = "Code128";

        // Create an HttpClient to download the image via HTTP.
        using (var httpClient = new HttpClient())
        {
            // Send a GET request and wait synchronously for the response.
            using (var response = httpClient.GetAsync(imageUrl).Result)
            {
                // Verify the request succeeded.
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to download image. HTTP status: {response.StatusCode}");
                    return;
                }

                // Obtain the response content as a stream.
                using (var imageStream = response.Content.ReadAsStreamAsync().Result)
                {
                    // Initialize BarCodeReader with the image stream and enable all supported barcode types.
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the image.
                        var results = reader.ReadBarCodes();

                        // Check if any barcodes were detected.
                        if (results == null || results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                            return;
                        }

                        // Iterate through each detected barcode.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded text: {result.CodeText}");

                            // Compare the detected type with the expected value (case‑insensitive).
                            if (string.Equals(result.CodeTypeName, expectedCodeType, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Service type matches the expected value.");
                            }
                            else
                            {
                                Console.WriteLine($"Service type mismatch. Expected: {expectedCodeType}");
                            }
                        }
                    }
                }
            }
        }
    }
}