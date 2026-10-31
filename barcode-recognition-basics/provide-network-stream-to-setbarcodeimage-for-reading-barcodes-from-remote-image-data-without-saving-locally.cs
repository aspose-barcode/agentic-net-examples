// Title: Read barcodes from a remote image via network stream
// Description: Demonstrates how to download a barcode image from a URL and decode it using Aspose.BarCode without saving the file locally.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader to process images supplied as streams. Typical use cases include reading barcodes from web services, cloud storage, or any remote source where persisting the image is unnecessary. Developers often need to work with HttpClient, streams, and the SetBarCodeImage method to integrate barcode scanning into network‑based workflows.
// Prompt: Provide a network stream to SetBarCodeImage for reading barcodes from remote image data without saving locally.
// Tags: barcode recognition, network stream, httpclient, setbarcodeimage, aspnet, aspose.barcode

using System;
using System.IO;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from a remote image using a network stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Downloads an image from a URL, feeds it to BarCodeReader via a stream, and prints detected barcodes.
    /// </summary>
    /// <param name="args">Optional first argument specifying the image URL.</param>
    static void Main(string[] args)
    {
        // Determine the image URL: use the first command‑line argument if provided, otherwise fall back to a default.
        string imageUrl = args.Length > 0 ? args[0] : "https://example.com/barcode.png";

        try
        {
            // Create an HttpClient to fetch the remote image.
            using (HttpClient client = new HttpClient())
            {
                // Synchronously request the image data.
                using (HttpResponseMessage response = client.GetAsync(imageUrl).Result)
                {
                    // Throw if the HTTP request was not successful.
                    response.EnsureSuccessStatusCode();

                    // Obtain the response content as a stream without writing to disk.
                    using (Stream imageStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        // Initialize the barcode reader.
                        using (BarCodeReader reader = new BarCodeReader())
                        {
                            // Supply the image stream to the reader.
                            reader.SetBarCodeImage(imageStream);

                            // Perform barcode detection.
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // Output the number of barcodes found.
                            Console.WriteLine($"Read {results.Length} barcode(s) from network stream:");

                            // Iterate through each result and display its type and decoded text.
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during the download or recognition process.
            Console.WriteLine($"Error processing barcode image from URL '{imageUrl}': {ex.Message}");
        }
    }
}