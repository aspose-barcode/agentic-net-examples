// Title: Decode OneCode barcode from a network stream and handle errors
// Description: Demonstrates downloading a barcode image via HttpClient, feeding it to Aspose.BarCode's BarCodeReader, and logging any decoding errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader with a specific DecodeType (OneCode). It illustrates typical scenarios such as retrieving barcode images from remote services, processing them in memory, and handling potential decoding failures. Developers working with barcode scanning, network image retrieval, or error logging will find this pattern useful.
// Prompt: Decode a OneCode barcode from a network response stream and log any decoding errors.
// Tags: onecode, barcode, decoding, network, aspose.barcode, barcoderecognition, error-handling

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a OneCode barcode image, attempts to decode it,
/// and logs any errors encountered during the decoding process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // URL of the OneCode barcode image (replace with a real URL if available)
        string url = "https://example.com/onecode.png";

        // Create an HttpClient instance to download the image
        using (HttpClient httpClient = new HttpClient())
        {
            // Synchronously request the image data
            using (HttpResponseMessage response = httpClient.GetAsync(url).Result)
            {
                // Verify that the request succeeded
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to download image. Status code: {response.StatusCode}");
                    return;
                }

                // Obtain the response stream containing the image bytes
                using (Stream responseStream = response.Content.ReadAsStreamAsync().Result)
                {
                    // Copy the response stream into a MemoryStream for random access
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        memoryStream.Position = 0; // Reset position to the beginning

                        // Specify the decode type for OneCode (recognition may be unsupported)
                        BaseDecodeType decodeType = DecodeType.OneCode;

                        // Initialize the BarCodeReader with the image stream and decode type
                        using (BarCodeReader reader = new BarCodeReader(memoryStream, decodeType))
                        {
                            try
                            {
                                // Attempt to read all barcodes from the image
                                BarCodeResult[] results = reader.ReadBarCodes();

                                // Check if any barcodes were detected
                                if (results.Length == 0)
                                {
                                    Console.WriteLine("No barcode detected (expected — OneCode recognition unsupported).");
                                }
                                else
                                {
                                    // Output details for each detected barcode
                                    foreach (BarCodeResult result in results)
                                    {
                                        Console.WriteLine($"CodeText: {result.CodeText}");
                                        Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                                        Console.WriteLine($"Confidence: {result.Confidence}");
                                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Log any exceptions that occur during decoding
                                Console.WriteLine($"Error during barcode decoding: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
    }
}