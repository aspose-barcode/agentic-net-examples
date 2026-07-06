// Title: Decode Barcode from Network Stream and Verify Service Type
// Description: Downloads a barcode image via HTTP, decodes it using Aspose.BarCode, and checks that the decoded text matches an expected service identifier.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, demonstrating how to use BarCodeReader with DecodeType.AllSupportedTypes to read barcodes from streams. Typical scenarios include processing images received over a network, validating embedded data, and integrating barcode verification into web services. Developers often need to download image data, feed it to the reader, and compare the decoded value against business rules.
// Prompt: Load a barcode image from a network stream, decode it, and verify service type matches expected value.
// Tags: barcode, decoding, http, network, verification, aspose.barcode, csharp

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a barcode image from a network stream, decoding it,
/// and verifying that the decoded text matches an expected service type.
/// </summary>
class Program
{
    // Expected service type that should be encoded in the barcode
    private const string ExpectedServiceType = "MyService";

    /// <summary>
    /// Entry point of the example. Downloads the barcode image, decodes it,
    /// and validates the decoded text against <see cref="ExpectedServiceType"/>.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // URL of the barcode image (replace with a real URL when testing)
        const string imageUrl = "https://example.com/barcode.png";

        // Create an HttpClient to download the image
        using (var httpClient = new HttpClient())
        // Request the image data with response headers read first
        using (var response = await httpClient.GetAsync(imageUrl, HttpCompletionOption.ResponseHeadersRead))
        {
            // Ensure the request succeeded
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to download image. Status code: {response.StatusCode}");
                return;
            }

            // Obtain the network stream containing the image bytes
            using (var networkStream = await response.Content.ReadAsStreamAsync())
            // Copy the network stream into a memory stream for random access
            using (var memoryStream = new MemoryStream())
            {
                await networkStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0; // Reset position to the beginning for reading

                // Initialize the barcode reader with the image stream and detect all supported types
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    // Perform recognition and retrieve all detected barcodes
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were found, inform the user and exit
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected in the image.");
                        return;
                    }

                    // Process each detected barcode (normally there will be only one)
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");

                        // Verify that the decoded text matches the expected service type
                        if (string.Equals(result.CodeText, ExpectedServiceType, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Verification succeeded: service type matches expected value.");
                        }
                        else
                        {
                            Console.WriteLine($"Verification failed: expected '{ExpectedServiceType}' but got '{result.CodeText}'.");
                        }
                    }
                }
            }
        }
    }
}