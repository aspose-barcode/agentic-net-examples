// Title: Read Barcode from Remote Image via Network Stream
// Description: Demonstrates downloading a barcode image from a URL and decoding it directly from the network stream without saving to disk.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to use the BarCodeReader class to extract barcode data from images obtained over HTTP. Typical use cases include processing barcodes from web services, cloud storage, or any remote source where persisting the image locally is undesirable. Developers often need to stream image data directly into the reader to improve performance and reduce I/O overhead.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode symbology, read, console, barcodereader, httpclient

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that downloads a barcode image from a remote URL and reads all supported barcode types directly from the network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs HTTP download, streams the image to BarCodeReader, and prints detected barcode information.
    /// </summary>
    static void Main()
    {
        // URL of the remote barcode image. Replace with a valid image URL.
        const string imageUrl = "https://example.com/barcode.png";

        // Create an HttpClient instance for downloading the image.
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Synchronously send GET request to the image URL.
                using (var response = httpClient.GetAsync(imageUrl).Result)
                {
                    // Throw if the HTTP response indicates failure.
                    response.EnsureSuccessStatusCode();

                    // Retrieve the response content as a stream.
                    using (var imageStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        // Initialize BarCodeReader with the image stream, decoding all supported barcode types.
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Iterate through each detected barcode and output its type and text.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                                Console.WriteLine($"Barcode Text: {result.CodeText}");
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Output any errors that occur during download or barcode recognition.
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}