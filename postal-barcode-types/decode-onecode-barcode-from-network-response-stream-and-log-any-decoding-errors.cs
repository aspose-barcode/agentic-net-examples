// Title: Decode OneCode barcode from a network response stream
// Description: Downloads an image containing a OneCode barcode, decodes it using Aspose.BarCode, and logs any decoding errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, demonstrating how to use BarCodeReader with DecodeType.OneCode to read barcodes from streams. Typical use cases include processing images received over HTTP, handling real‑time scanning scenarios, and integrating barcode decoding into web services. Developers often need to retrieve image data from a network source, instantiate a BarCodeReader for a specific symbology, and handle possible errors gracefully.
// Prompt: Decode a OneCode barcode from a network response stream and log any decoding errors.
// Tags: onecode, barcode, decode, network, stream, aspose.barcode, barcoderecognition

using System;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates downloading an image containing a OneCode barcode,
/// decoding it with Aspose.BarCode, and logging any errors that occur.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the download, decoding, and logging.
    /// </summary>
    static void Main()
    {
        // URL of the image that contains a OneCode barcode.
        const string imageUrl = "https://example.com/onecode.png";

        try
        {
            // Create an HttpClient to download the image.
            using (HttpClient httpClient = new HttpClient())
            {
                // Send a synchronous GET request and obtain the response.
                using (HttpResponseMessage response = httpClient.GetAsync(imageUrl).Result)
                {
                    // Throw if the HTTP status is not successful.
                    response.EnsureSuccessStatusCode();

                    // Get the response content as a stream.
                    using (System.IO.Stream imageStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        // Initialize BarCodeReader for the OneCode symbology using the image stream.
                        using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.OneCode))
                        {
                            // Read all barcodes found in the image.
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // If no barcodes were detected, inform the user.
                            if (results.Length == 0)
                            {
                                Console.WriteLine("No OneCode barcode detected in the image.");
                            }
                            else
                            {
                                // Iterate through each detected barcode and log its text.
                                foreach (BarCodeResult result in results)
                                {
                                    Console.WriteLine($"Detected OneCode barcode: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occurred during download or decoding.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}